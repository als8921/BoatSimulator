using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace RosSharp.RosBridgeClient
{
    public class IMUSubscribe : UnitySubscriber<MessageTypes.Std.Float32>
    {
        public GameObject MapObject;
        public double psi;

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
            psi = message.data;
            isMessageReceived = true;
        }

        private void ProcessMessage()
        {
            MapObject.GetComponent<UIMainScript>().theta = (float)psi;
        }
    }
}

