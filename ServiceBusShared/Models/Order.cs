using System;


namespace ServiceBusShared.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }

        public string OrderName { get; set; }
    }
}
