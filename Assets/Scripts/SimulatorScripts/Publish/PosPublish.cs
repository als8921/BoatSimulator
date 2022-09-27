using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class PosPublish : UnityPublisher<MessageTypes.Std.Float64MultiArray>
    {
        private MessageTypes.Std.Float64MultiArray Data;
        public GameObject Boat;
        protected override void Start()
        {
            base.Start();
            Data = new MessageTypes.Std.Float64MultiArray();
        }

        private void FixedUpdate()
        {
            Data.data = new double[] { Boat.transform.position.x, Boat.transform.position.z };
            Publish(Data);
        }
    }
}
