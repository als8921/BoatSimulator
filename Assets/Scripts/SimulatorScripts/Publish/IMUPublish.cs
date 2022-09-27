using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class IMUPublish : UnityPublisher<MessageTypes.Std.Float32>
    {
        private MessageTypes.Std.Float32 Data;
        public GameObject Boat;
        protected override void Start()
        {
            base.Start();
            Data = new MessageTypes.Std.Float32();
        }

        private void FixedUpdate()
        {
            Data.data = (Boat.transform.rotation.eulerAngles.y > 180) ? Boat.transform.rotation.eulerAngles.y - 360 : Boat.transform.rotation.eulerAngles.y;
            Publish(Data);
        }
    }
}
