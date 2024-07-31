using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OrderManagement.Core.Services.DatabaseTransactions.Contracts;
using OrderManagement.Data.DatabaseContext;
using OrderManagement.Data.Enumerators.Config;
using OrderManagement.Data.Models;
using OrderManagement.Data.Models.Extensions;
using OrderManagement.Data.Models.Response.Orders;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OrderManagement.Core.Services.DatabaseTransactions.Impementation;

public class DatabaseTransactionService : IDatabaseTransactionService
{
    private readonly OrderManagementDbContext _dbContext;
    private readonly OrderOptions _options;
    private readonly string _filePath;

    public DatabaseTransactionService(OrderManagementDbContext dbContext,
                                    IOptions<OrderOptions> options)
    {
        _options = options.Value;
        _filePath = Path.Combine(_options?.FilePath, "Orders.json");
        _dbContext = dbContext;
    }

    public async Task<bool> CreateOrder(Data.Entities.OrderHeader order)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var newOrder = await _dbContext.Orders.FirstOrDefaultAsync(o => o.UserId == order.UserId
                                                   && o.OrderStatus == Data.Enumerators.OrderStatus.New);
            if (newOrder is not null)
                return false;

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            await _dbContext.BulkInsertOrUpdateAsync(order.OrderLine);

            SalesOrders salesOrders = await LoadOrderFromFile();
            if (salesOrders.OrderHeaders.Count > 0 && salesOrders.OrderHeaders.Exists(o => o.OrderNumber == order.OrderNumber))
            {
                await transaction.RollbackAsync();
                return false;
            }
            await transaction.CommitAsync();
            salesOrders.OrderHeaders.Add(order!?.ToOrderHeader());
            await SaveOrderToFile(salesOrders);
            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateOrder(Data.Entities.OrderHeader order)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            _dbContext.Update(order);
            await _dbContext.SaveChangesAsync();
            await _dbContext.BulkInsertOrUpdateOrDeleteAsync(order.OrderLine);

            SalesOrders salesOrders = await LoadOrderFromFile();

            if (!salesOrders.OrderHeaders.Exists(o => o.OrderNumber == order.OrderNumber))
            {
                await transaction.RollbackAsync();
                return false;
            }
            await transaction.CommitAsync();
            var existingOrder = salesOrders.OrderHeaders.FirstOrDefault(o => o.OrderNumber == order.OrderNumber);

            if (existingOrder != null)
            {
                existingOrder.OrderType = order.OrderType;
                existingOrder.CreateDate = order.CreateDate;

                var updatedOrderLines = order.OrderLine.Select(ol => new OrderLine
                {
                    LineNumber = ol.LineNumber,
                    ProductCode = ol.ProductCode,
                    ProductType = ol.ProductType,
                    CostPrice = ol.CostPrice,
                    SalesPrice = ol.SalesPrice,
                    Quantity = ol.Quantity
                }).ToList();

                existingOrder.OrderLine = updatedOrderLines;
            }
            else
            {
                salesOrders.OrderHeaders.Add(order.ToOrderHeader());
            }
            await SaveOrderToFile(salesOrders);
            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteOrder(Data.Entities.OrderHeader order)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            _dbContext.Remove(order);
            await _dbContext.SaveChangesAsync();

            SalesOrders salesOrders = await LoadOrderFromFile();

            if (!salesOrders.OrderHeaders.Exists(o => o.OrderNumber == order.OrderNumber))
            {
                await transaction.RollbackAsync();
                return false;
            }
            await transaction.CommitAsync();
            var existingOrder = salesOrders.OrderHeaders.FirstOrDefault(o => o.OrderNumber == order.OrderNumber);
            if (existingOrder is not null)
            {
                salesOrders.OrderHeaders.Remove(existingOrder);
                await SaveOrderToFile(salesOrders);
            }
            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }

    #region PrivateMethods

    private async Task SaveOrderToFile(SalesOrders salesOrders)
    {
        if (salesOrders is null)
            return;

        var updatedJson = JsonSerializer.Serialize(salesOrders, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_filePath, updatedJson);
    }

    private async Task<SalesOrders> LoadOrderFromFile()
    {
        SalesOrders? salesOrders = null;
        if (File.Exists(_filePath))
        {
            var existingJson = await File.ReadAllTextAsync(_filePath);
            salesOrders = JsonSerializer.Deserialize<SalesOrders>(existingJson) ?? new SalesOrders();
        }
        return salesOrders;
    }

    #endregion PrivateMethods
}