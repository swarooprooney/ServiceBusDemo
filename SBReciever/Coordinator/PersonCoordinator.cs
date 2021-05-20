﻿using SBReceiver.GenericReceiver;
using SBShared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SBReceiver.Coordinator
{

    public class PersonCoordinator : IPersonCoordinator
    {
        private readonly IReceiver<PersonModel> _receiver;

        public PersonCoordinator(IReceiver<PersonModel> receiver)
        {
            _receiver = receiver;
        }

        public async Task CloseQueueAsync()
        {
            _receiver.RaiseMessageReadyEvent -= _receiver_RaiseMessageReadyEvent;
           await _receiver.CloseQueueAsync();
        }

        public async Task GetMessageFromQueueAsync()
        {
            _receiver.RaiseMessageReadyEvent += _receiver_RaiseMessageReadyEvent;
            await _receiver.ReadMessageAsync("personqueue");
        }

        private void _receiver_RaiseMessageReadyEvent(object sender, PersonModel personModel)
        {
            Console.WriteLine($"The name of the person is {personModel.FirstName} {personModel.LastName}");
        }
    }
}