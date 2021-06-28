using System;
using System.Collections.Generic;

namespace SharedCode {
    public static class RabbitMqSetup {
        public const string cartCreation = "create_cart_for_new_customer.queue";
        public record BaseMessage(Guid Id, DateTime CreatedDate) {
            public BaseMessage() : this(Guid.NewGuid(), DateTime.Now) { }
        }
        public record Message(string CustomerId="") : BaseMessage;
    }
}
