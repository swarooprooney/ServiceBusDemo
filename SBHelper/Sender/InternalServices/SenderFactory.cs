﻿using SBHelper.Sender.ExternalServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace SBHelper.Sender.InternalServices
{
    public class SenderFactory
    {
        private readonly string _connectionString;

        public SenderFactory(string connectionString)
        {
           _connectionString = connectionString;
        }
        internal ISender GetConcreteSender(ServiceBusType type, string name)
        {
            ISender sender;
            switch (type)
            {

                case ServiceBusType.Queue:
                    sender = new QueueService(_connectionString,name);
                    break;
                case ServiceBusType.Topic:
                    sender = new TopicService(_connectionString,name);
                    break;
                default:
                    sender = null;
                    break;
            }
            return sender;
        }
    }
}