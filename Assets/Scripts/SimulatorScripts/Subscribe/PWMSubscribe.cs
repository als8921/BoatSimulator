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
        [SerializeField]
        double[] PWM;

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

        protected override void ReceiveMessage(MessageTypes.Std.Float64MultiArray message)
        {
            PWM = message.data;
            isMessageReceived = true;
        }

        private void ProcessMessage()
        {
            Debug.Log(PWM[0] + ", " + PWM[1]);
            BoatMove.PWM = PWM;
        }
    }
}

