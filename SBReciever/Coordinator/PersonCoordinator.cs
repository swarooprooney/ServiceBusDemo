using SBHelper;
using SBHelper.Receiver;
using SBHelper.Receiver.ExternalServices;
using SBShared.Models;
using System;
using System.Threading.Tasks;

namespace SBReceiver.Coordinator
{

    public class PersonCoordinator : IPersonCoordinator
    {
        private readonly IQueueReceiver<PersonModel> _receiver;

        public PersonCoordinator(IQueueReceiver<PersonModel> receiver)
        {
            _receiver = receiver;
        }

        public async Task CloseQueueAsync()
        {
            _receiver.RaiseMessageReadyEvent -= PersonCoordinator_raiseMessageRecieved;
            _receiver.RaiseExceptionEvent -= PersonCoordinator_raiseExceptionOccured;
            await _receiver.CloseQueueAsync();
        }

        public async Task GetMessageFromQueueAsync()
        {
            _receiver.RaiseMessageReadyEvent += PersonCoordinator_raiseMessageRecieved;
            _receiver.RaiseExceptionEvent += PersonCoordinator_raiseExceptionOccured;
            await _receiver.ReadMessageAsync();
        }


        private void PersonCoordinator_raiseExceptionOccured(object sender, ExceptionModel e)
        {
            throw e.Exception;
        }

        private void PersonCoordinator_raiseMessageRecieved(object sender, PersonModel personModel)
        {
            Console.WriteLine($"The name of the person is {personModel.FirstName} {personModel.LastName}");
        }
    }
}
