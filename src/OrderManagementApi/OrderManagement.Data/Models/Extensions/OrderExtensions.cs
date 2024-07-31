using OrderManagement.Data.Enumerators.Config;

namespace OrderManagement.Data.Models.Extensions;

public static class OrderExtensions
{
    public static List<Response.Orders.OrderHeader> ToOrderHeaders(this List<Entities.OrderHeader> orderHeaders)
    {
        if (orderHeaders.Count < 1)
            return [];

        var result = new List<Response.Orders.OrderHeader>();
        result.AddRange(orderHeaders.Select(orderHeader => new Response.Orders.OrderHeader()
        {
            OrderNumber = orderHeader.OrderNumber,
            OrderType = orderHeader.OrderType,
            OrderStatus = orderHeader.OrderStatus,
            CustomerName = orderHeader.User.ToString(),
            CreateDate = orderHeader.CreateDate,
            OrderLine = orderHeader.OrderLine.ToOrderLines()
        }));
        return result;
    }

    public static Response.Orders.OrderHeader? ToOrderHeader(this Entities.OrderHeader orderHeader)
    {
        if (orderHeader is null)
            return null;

        return new Response.Orders.OrderHeader
        {
            OrderNumber = orderHeader.OrderNumber,
            OrderType = orderHeader.OrderType,
            OrderStatus = orderHeader.OrderStatus,
            CustomerName = orderHeader.User.ToString(),
            CreateDate = orderHeader.CreateDate,
            OrderLine = orderHeader.OrderLine.ToOrderLines()
        };
    }

    public static Entities.OrderHeader ToOrderHeader(this Request.Orders.CreateOrderHeader createOrderHeader, Guid userId)
    {
        var orderHeaderId = Guid.NewGuid();
        return new Entities.OrderHeader
        {
            Id = orderHeaderId,
            OrderNumber = createOrderHeader.OrderNumber,
            OrderType = createOrderHeader.OrderType,
            CreateDate = createOrderHeader.CreateDate,
            OrderStatus = Enumerators.OrderStatus.New,
            UserId = userId,
            OrderLine = createOrderHeader.OrderLine.ToOrderLines(orderHeaderId)
        };
    }

    public static Entities.OrderHeader ToOrderHeader(this Request.Orders.UpdateOrderHeader updateOrderHeader, Entities.OrderHeader orderHeader)
    {
        orderHeader.OrderType = updateOrderHeader.OrderType;
        orderHeader.CreateDate = updateOrderHeader.CreateDate;
        orderHeader.OrderLine = updateOrderHeader.OrderLine.ToOrderLines(orderHeader.OrderLine, orderHeader.Id);
        return orderHeader;
    }

    private static List<Response.Orders.OrderLine> ToOrderLines(this ICollection<Entities.OrderLine> orderLines)
    {
        if (orderLines.Count < 1)
            return [];

        var result = new List<Response.Orders.OrderLine>();
        result.AddRange(orderLines.Select(orderLine => new Response.Orders.OrderLine()
        {
            LineNumber = orderLine.LineNumber,
            ProductCode = orderLine.ProductCode,
            ProductType = orderLine.ProductType,
            CostPrice = orderLine.CostPrice,
            SalesPrice = orderLine.SalesPrice,
            Quantity = orderLine.Quantity
        }));
        return result;
    }

    private static List<Entities.OrderLine> ToOrderLines(this List<Request.Orders.CreateOrderLine> createOrderLines, Guid orderHeaderId)
    {
        if (createOrderLines.Count < 1)
            return [];

        var result = new List<Entities.OrderLine>();
        result.AddRange(createOrderLines.Select(createOrderLine => new Entities.OrderLine
        {
            LineNumber = createOrderLine.LineNumber,
            ProductCode = createOrderLine.ProductCode,
            ProductType = createOrderLine.ProductType,
            CostPrice = createOrderLine.CostPrice,
            SalesPrice = createOrderLine.SalesPrice,
            Quantity = createOrderLine.Quantity,
            OrderId = orderHeaderId,
            Id = Guid.NewGuid()
        }));
        return result;
    }

    private static ICollection<Entities.OrderLine> ToOrderLines(this List<Request.Orders.UpdateOrderLine> updateOrderLines, ICollection<Entities.OrderLine> orderLines, Guid orderHeaderId)
    {
        if (updateOrderLines == null || updateOrderLines.Count < 1)
            return orderLines;

        var result = new List<Entities.OrderLine>();

        var updateOrderLinesDict = updateOrderLines.ToDictionary(u => u.ProductCode);
        var existingOrderLinesDict = orderLines.ToDictionary(o => o.ProductCode);

        foreach (var updateOrderLine in updateOrderLines)
        {
            if (existingOrderLinesDict.TryGetValue(updateOrderLine.ProductCode, out var existingOrderLine))
            {
                existingOrderLine.ProductCode = updateOrderLine.ProductCode;
                existingOrderLine.ProductType = updateOrderLine.ProductType;
                existingOrderLine.CostPrice = updateOrderLine.CostPrice;
                existingOrderLine.SalesPrice = updateOrderLine.SalesPrice;
                existingOrderLine.Quantity = updateOrderLine.Quantity;
                result.Add(existingOrderLine);
            }
            else
            {
                result.Add(new Entities.OrderLine
                {
                    LineNumber = updateOrderLine.LineNumber,
                    ProductCode = updateOrderLine.ProductCode,
                    ProductType = updateOrderLine.ProductType,
                    CostPrice = updateOrderLine.CostPrice,
                    SalesPrice = updateOrderLine.SalesPrice,
                    Quantity = updateOrderLine.Quantity,
                    OrderId = orderHeaderId
                });
            }
        }

        foreach (var existingOrderLine in orderLines)
        {
            if (!updateOrderLinesDict.ContainsKey(existingOrderLine.ProductCode))
            {
                orderLines.Remove(existingOrderLine);
            }
        }

        return result;
    }
}