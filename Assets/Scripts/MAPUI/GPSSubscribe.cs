using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace RosSharp.RosBridgeClient
{
    public class GPSSubscribe : UnitySubscriber<MessageTypes.Sensor.NavSatFix>
    {
        public GameObject MapObject;
        public double lat, lon;

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

        protected override void ReceiveMessage(MessageTypes.Sensor.NavSatFix message)
        {
            lat = message.latitude;
            lon = message.longitude;
            isMessageReceived = true;
        }

        private void ProcessMessage()
        {
            MapObject.GetComponent<UIMainScript>().LAT = lat;
            MapObject.GetComponent<UIMainScript>().LON = lon;
        }
    }
}

