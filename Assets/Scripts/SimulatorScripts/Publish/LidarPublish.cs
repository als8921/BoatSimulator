using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class LidarPublish : UnityPublisher<MessageTypes.Std.Float64MultiArray>
    {
        private MessageTypes.Std.Float64MultiArray Data;
        public GameObject LidarObject;
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
            
             Data.data = LidarObject.GetComponent<Lidar>().lidarData;
             Publish(Data);
        }
    }
}
