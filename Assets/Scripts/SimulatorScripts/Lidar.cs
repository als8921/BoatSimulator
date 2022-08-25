using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lidar : MonoBehaviour
{
    public static float distance = 50;

    public GameObject[] lidarObject = new GameObject[360];

    public RaycastHit hit;

    public float theta = 0;

    public double[] lidarData = new double[360];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < lidarData.Length; i++)
        {
            lidarData[i] = 0;
        }

        for (int i = 0; i < lidarObject.Length; i++)
        { 
            lidarObject[i] = transform.Find("LidarTheta (" + i + ")").gameObject;
            lidarObject[i].transform.Rotate(0, i, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < lidarObject.Length; i++)
        {
            
            lidarData[i] = lidarObject[i].GetComponent<LidarSensor>().distanceData;
            
        }
            

        
    }
}
