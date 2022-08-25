using UnityEngine;
namespace RosSharp.RosBridgeClient
{
    public class testPsi_dPublish : UnityPublisher<MessageTypes.Std.Float32>
    {
        private MessageTypes.Std.Float32 message;
        public GameObject EventSystem;
        protected override void Start()
        {
            base.Start();
            message = new MessageTypes.Std.Float32();
        }

        private void FixedUpdate()
        {
            UpdateMessage();
        }

        private void UpdateMessage()
        {
            message.data = EventSystem.GetComponent<UIMainScript>().psi_d;
            Publish(message);
        }
    }
}
