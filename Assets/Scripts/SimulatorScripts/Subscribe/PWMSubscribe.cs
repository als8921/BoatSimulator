using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace RosSharp.RosBridgeClient
{
    public class PWMSubscribe : UnitySubscriber<MessageTypes.Std.Float64MultiArray>
    {
        public GameObject Boat;
        double[] PWM;

        public bool isMessageReceived;

        protected override void Start()
        {
            base.Start();
            Boat.GetComponent<PWMController>().LPWM = 1500;
            Boat.GetComponent<PWMController>().RPWM = 1500;
        }

        private void Update()
        {
            if (isMessageReceived)
                ProcessMessage();
        }

        protected override void ReceiveMessage(MessageTypes.Std.Float64MultiArray message)
        {
            PWM = message.data;
            isMessageReceived = true;
        }

        private void ProcessMessage()
        {
            Boat.GetComponent<PWMController>().LPWM = (float)PWM[0];
            Boat.GetComponent<PWMController>().RPWM = (float)PWM[1];
        }
    }
}

