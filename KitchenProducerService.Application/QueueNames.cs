namespace KitchenProducerService.Application
{
    public static class QueueNames
    {
        // Cozinha aceitou o pedido, iniciou o preparo
        public const string OrderAccepted = "kitchen.order.accepted";

        // Cozinha recusou o pedido
        public const string OrderRejected = "kitchen.order.rejected";

        // Pedido está pronto para entrega
        public const string OrderReady = "kitchen.order.ready";
    }
}
