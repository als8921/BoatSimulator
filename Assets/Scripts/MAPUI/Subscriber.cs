using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace RosSharp.RosBridgeClient
{
    public class Subscriber : UnitySubscriber<MessageTypes.Std.Float64MultiArray>
    {
        public GameObject text;

        public GameObject Point;
        public double posX;
        public double posY;
        public double theta;

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
            posX = message.data[0];
            posY = message.data[1];
            //theta = message.data[2];
            //posX = GetPosition(message).Ros2Unity();
            //posY = GetRotation(message).Ros2Unity();
            isMessageReceived = true;
        }

        private void ProcessMessage()
        {
            //PublishedTransform.position = new Vector3((float)posX, (float)posY);
            print("PosX : "+ Math.Round((float)posX, 7) + ", PosY : " + Math.Round((float)posY, 7));
            Point.GetComponent<UIMainScript>().LAT = Math.Round((float)posX,7);
            Point.GetComponent<UIMainScript>().LON = Math.Round((float)posY,7);
            text.GetComponent<Text>().text = Math.Round((float)posX, 7).ToString() + "  " + Math.Round((float)posY, 7).ToString();
        }
    }
}

