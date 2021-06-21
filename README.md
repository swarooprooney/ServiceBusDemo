# ServiceBusDemo

There are 4 projects in the solution
1. ServiceBusHelper - This is the library which will help you to write and read messages from queue/topic in service bus on Azure.
2. SBSender - This is POC project which basically uses the ServiceBusHelper to publish a message into queue/topic.
3. ServiceBusShared - This is a shared project between sender and consumer. Mainly holds models which are published and consumed.
4. SBReciever - This is POC project which basically uses ServiceBusHelper to consume messages from queue/topic.

# I will go ahead and explain how to use ServiceBusHelper in your project

If you app uses dependency injection then use the methods RegisterSender,RegisterRecieverForQueue or RegisterRecieverForTopic to register the services.
        Use RegisterSender if you want to publish a message to queue or topic
        Use RegisterRecieverForQueue/RegisterRecieverForTopic depending upon where you want to consume the message from.
once the dependency has been registered,for sending messages to queue/topic you can inject it like below.

        private readonly ISenderService _queueService;
        public ContactController(ISenderService queueService)
        {
            _queueService = queueService;
        }
and then use below code to send the message to queue.
        
        await _queueService.SendMessage(data, "emailqueue", ServiceBusType.Queue);

To read from queue/topic
you will have to create two event listners and hook it up to events on QueueReceiver/TopicReceiver. These two event listners are to invoked when message is read from the queue and when there is an exception respectively. 

Refer the SBReciever for a POC of how to consume messages from the queue
        
If you are not using Dependency Injection (Why are you not using?), take a look at the public interface IQueueReceiver,ITopicReceiver and ISenderService and use them to consume/publish messages
