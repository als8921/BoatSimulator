using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class Publisher : UnityPublisher<MessageTypes.Std.Float64MultiArray>
    {
        private MessageTypes.Std.Float64MultiArray message;
        public GameObject EventSystem;
        protected override void Start()
        {
            base.Start();
            message = new MessageTypes.Std.Float64MultiArray();
        }

        private void FixedUpdate()
        {
            UpdateMessage();
        }

        private void UpdateMessage()
        {
            if (EventSystem.GetComponent<UIMainScript>().GPSCoorList.Length > 0)
            {
                message.data = EventSystem.GetComponent<UIMainScript>().GPSCoorList;
                Publish(message);
            }
        }
    }
}
