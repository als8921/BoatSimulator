using UnityEngine;
using UnityEngine.UI;

namespace RosSharp.RosBridgeClient
{
    public class CameraPublish : UnityPublisher<MessageTypes.Sensor.CompressedImage>
    {
        MessageTypes.Sensor.CompressedImage Data;
        RenderTexture renderTexture;
        Texture2D mainCameraTexture;
        public Camera sensorCamera;
        public Image image;
        float time = 0;


        int frame_width, frame_height;

        Rect frame;

        public float delay = 0.5f;
        protected override void Start()
        {
            base.Start();
            Data = new MessageTypes.Sensor.CompressedImage();

            renderTexture = new RenderTexture(sensorCamera.pixelWidth, sensorCamera.pixelHeight, 0, UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm);
            renderTexture.Create();
            frame_width = renderTexture.width;
            frame_height = renderTexture.height;
            frame = new Rect(0, 0, frame_width, frame_height);
            mainCameraTexture = new Texture2D(frame_width, frame_height, TextureFormat.RGBA32, false);
           

        }

        private void FixedUpdate()
        {
            UpdateMessage();
        }

        private void UpdateMessage()
        {
            time += Time.deltaTime;
            if(time > delay)
            {
                time = 0;
                sensorCamera.targetTexture = renderTexture;
                RenderTexture currentRT = RenderTexture.active;
                RenderTexture.active = renderTexture;
                sensorCamera.Render();

                mainCameraTexture.ReadPixels(frame, 0, 0);
                mainCameraTexture.Apply();

                sensorCamera.targetTexture = currentRT;
                sensorCamera.targetTexture = null;

                Data.data = mainCameraTexture.GetRawTextureData();
                Publish(Data);
                //var camTexture = new Texture2D(renderTexture.width, renderTexture.height);
                //camTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
                //camTexture.Apply();
                //RenderTexture.active = currentRT;
                //Rect rect = new Rect(0, 0, camTexture.width, camTexture.height);
                //image.sprite = Sprite.Create(camTexture, rect, new Vector2(0.5f, 0.5f));

                //// Encode the texture as a PNG, and send to ROS
                //byte[] imageBytes = camTexture.GetRawTextureData();
                //Data.data = imageBytes;
                //Publish(Data);



            }
        }
    }
}
