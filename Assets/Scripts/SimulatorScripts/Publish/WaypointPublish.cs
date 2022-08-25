using UnityEngine;
using System.Collections.Generic;

namespace RosSharp.RosBridgeClient
{
    public class WaypointPublish : UnityPublisher<MessageTypes.Std.Float64MultiArray>
    {
        private MessageTypes.Std.Float64MultiArray Data;
        public GameObject Boat;
        List<float[]> WayPoint;

        protected override void Start()
        {
            base.Start();
            Data = new MessageTypes.Std.Float64MultiArray();
        }

        private void FixedUpdate()
        {
            UpdateMessage();
        }

        private void UpdateMessage()
        {
            WayPoint = Boat.GetComponent<PWMController>().WP;
            List<double> temp = new List<double>();
            if (WayPoint != null)
                foreach(float[] wp in WayPoint)
                {
                    temp.Add(wp[0]);
                    temp.Add(wp[1]);
                }
            Data.data = temp.ToArray();
            Publish(Data);
        }
    }
}
