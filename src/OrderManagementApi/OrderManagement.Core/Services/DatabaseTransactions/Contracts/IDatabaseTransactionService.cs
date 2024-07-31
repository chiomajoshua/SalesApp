namespace OrderManagement.Core.Services.DatabaseTransactions.Contracts;

public interface IDatabaseTransactionService
{
    Task<bool> CreateOrder(Data.Entities.OrderHeader order);

    Task<bool> UpdateOrder(Data.Entities.OrderHeader order);

    Task<bool> DeleteOrder(Data.Entities.OrderHeader order);
}