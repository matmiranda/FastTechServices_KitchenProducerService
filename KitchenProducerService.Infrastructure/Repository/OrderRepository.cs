using Dapper;
using KitchenProducerService.Application.Dto;
using KitchenProducerService.Domain.Entities;
using KitchenProducerService.Infrastructure.Database;

namespace KitchenProducerService.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DapperContext _context;
        public OrderRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PendingOrderDto>> GetPendingOrdersAsync()
        {
            using var connection = _context.CreateConnection();
            var sql = @"
        SELECT 
            o.id AS OrderId,
            o.customer_id AS CustomerId,
            o.delivery_method AS DeliveryMethod,
            o.status AS Status,
            o.total AS OrderTotal,
            o.created_at AS OrderCreated,
            oi.id AS ItemId,
            oi.menu_item_id AS MenuItemId,
            oi.quantity AS Quantity,
            oi.price_at_order AS PriceAtOrder,
            oi.total_item AS TotalItem,
            oi.created_at AS ItemCreated
        FROM order_db.orders o
        JOIN order_db.order_items oi ON o.id = oi.order_id
        WHERE o.status = 'PENDENTE'
        ORDER BY o.created_at ASC;
    ";

            var orderDictionary = new Dictionary<long, PendingOrderDto>();

            var result = await connection.QueryAsync<PendingOrderDto, OrderItemDto, PendingOrderDto>(
                sql,
                (order, item) =>
                {
                    if (!orderDictionary.TryGetValue(order.OrderId, out var pendingOrder))
                    {
                        pendingOrder = order;
                        pendingOrder.Items = new List<OrderItemDto>();
                        orderDictionary.Add(order.OrderId, pendingOrder);
                    }
                    pendingOrder.Items.Add(item);
                    return pendingOrder;
                },
                splitOn: "ItemId"
            );

            return orderDictionary.Values;
        }

        public async Task<bool> OrderExistsAsync(ulong orderId)
        {
            using var connection = _context.CreateConnection();
            var sql = @"SELECT COUNT(1) FROM order_db.orders WHERE id = @Id;";

            var count = await connection.ExecuteScalarAsync<int>(sql, new { Id = orderId });
            return count > 0;
        }


    }
}
