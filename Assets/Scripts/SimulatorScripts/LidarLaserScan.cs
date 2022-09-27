using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class LidarLaserScan : UnityPublisher<MessageTypes.Sensor.LaserScan>
    {
        private MessageTypes.Sensor.LaserScan Data;
        public GameObject LidarObject;
        protected override void Start()
        {
            base.Start();
            Data = new MessageTypes.Sensor.LaserScan();
            Data.angle_min = 0;
            Data.angle_max = 359 * Mathf.PI / 180.0f;
            Data.angle_increment = 1 * Mathf.PI / 180.0f;
            Data.time_increment = 0.02f;
            Data.range_min = 1;
            Data.range_max = 200;
        }

        private void FixedUpdate()
        {
            double[] data = LidarObject.GetComponent<Lidar>().lidarData;
            for (int i = 0; i < 360; i++)
                Data.ranges[i] = (float)data[i];
            Publish(Data);
        }
    }
}
