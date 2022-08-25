using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace RosSharp.RosBridgeClient
{
    public class Psi_dSubscribe : UnitySubscriber<MessageTypes.Std.Float32>
    {
        public GameObject Boat;
        public float PSI_D;

        public bool isMessageReceived;

        protected override void Start()
        {
            base.Start();
        }

        private void Update()
        {
            if (isMessageReceived)
                ProcessMessage();
        }

        protected override void ReceiveMessage(MessageTypes.Std.Float32 message)
        {
            PSI_D = message.data;
            isMessageReceived = true;
        }

        private void ProcessMessage()
        {
            Boat.GetComponent<PWMController>().psi_d = PSI_D;
        }
    }
}

