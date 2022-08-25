using UnityEngine;
using System.Collections.Generic;

namespace RosSharp.RosBridgeClient
{
    public class LidarPCLPublish : UnityPublisher<MessageTypes.Sensor.PointCloud>
    {
        public GameObject Lidar3DObject;
        double[,] LidarData;
        float time = 0;
        MessageTypes.Sensor.PointCloud Data;

        protected override void Start()
        {
            base.Start();
            Data = new MessageTypes.Sensor.PointCloud();
        }

        private void FixedUpdate()
        {
            time += Time.deltaTime;
            if(time > 0.1f)
            {
                UpdateMessage();
                time = 0;
            }
        }

        private void UpdateMessage()
        {
            LidarData = Lidar3DObject.GetComponent<Lidar3D>().lidarData;
            MessageTypes.Geometry.Point32[] data = new MessageTypes.Geometry.Point32[3025];
            List<MessageTypes.Geometry.Point32> pclData = new List<MessageTypes.Geometry.Point32>();
            for(int i = 0; i < 3025; i++)
            {
                double d = LidarData[i / 121, i % 121];
                double a = (i / 121) - 12;
                double b = (i % 121) - 60;
                double x = d * Mathf.Cos((float)b * Mathf.PI / 180.0f) * Mathf.Cos((float)a * Mathf.PI / 180.0f);
                double y = -d * Mathf.Sin((float)b * Mathf.PI / 180.0f);
                double z = -d * Mathf.Cos((float)b * Mathf.PI / 180.0f) * Mathf.Sin((float)a * Mathf.PI / 180.0f);
                if (x != 0 || y != 0 || z != 0)
                {
                    pclData.Add(new MessageTypes.Geometry.Point32((float)x, (float)y, (float)z));
                }
            }
            Data.points = pclData.ToArray();
            Data.header.frame_id = "map";
            Publish(Data);

        }
    }
}
