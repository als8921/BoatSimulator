using UnityEngine;
using UnityEngine.UI;

namespace RosSharp.RosBridgeClient
{
    public class Psi_dPublish : UnityPublisher<MessageTypes.Std.Float32>
    {
        private MessageTypes.Std.Float32 Data;
        public GameObject Boat;
        public Slider Handle;
        public Text HandleText;
        protected override void Start()
        {
            base.Start();
            Data = new MessageTypes.Std.Float32();
        }

        private void FixedUpdate()
        {
            UpdateMessage();
        }

        private void UpdateMessage()
        {
            //Data.data = Handle.value;
            //HandleText.text = Handle.value.ToString();
            Data.data = Boat.GetComponent<PWMController>().psi_d;
            Publish(Data);
        }
    }
}
