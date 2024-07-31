using Moq;
using OrderManagement.Core.Infrastructure.RepositoryServices.Contracts;
using OrderManagement.Core.Services.Orders.Implemenation;
using OrderManagement.Data.Entities;
using System.Linq.Expressions;

namespace OrderManagement.Test.CoreTests;

public class OrderServiceTests
{
    private readonly Mock<IRepositoryService<OrderHeader>> _repositoryMock;
    private readonly OrderService _orderService;

    public OrderServiceTests()
    {
        _repositoryMock = new Mock<IRepositoryService<OrderHeader>>();
        _orderService = new OrderService(_repositoryMock.Object);
    }

    [Fact]
    public async Task GetOrderHeaderByIdAsync_OrderHeaderNotFound_ReturnsNull()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        _repositoryMock
            .Setup(x => x.GetSingleAsync(It.IsAny<Expression<Func<OrderHeader, bool>>>(), It.IsAny<bool>(), It.IsAny<Func<IQueryable<OrderHeader>, IQueryable<OrderHeader>>>()))
            .ReturnsAsync((OrderHeader)null); // Return null to simulate order header not found

        // Act
        var result = await _orderService.GetOrderById(orderId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetOrderHeaderByOrderNumberAsync_WithValidOrderNumber_ReturnsOrderHeader()
    {
        // Arrange
        var orderNumber = "262FGSA";
        var expectedOrderHeader = new OrderHeader
        {
            Id = Guid.NewGuid(),
            OrderNumber = orderNumber,
            CreateDate = DateTime.Now,
            OrderStatus = Data.Enumerators.OrderStatus.Processing,
            OrderType = Data.Enumerators.OrderType.Normal
        };

        _repositoryMock
              .Setup(x => x.GetSingleAsync(It.IsAny<Expression<Func<OrderHeader, bool>>>(), It.IsAny<bool>(), It.IsAny<Func<IQueryable<OrderHeader>, IQueryable<OrderHeader>>>()))
              .ReturnsAsync(expectedOrderHeader);

        // Act
        var result = await _orderService.GetOrderByOrderNumber(orderNumber);

        // Assert
        Assert.Equal(expectedOrderHeader, result);
    }
}