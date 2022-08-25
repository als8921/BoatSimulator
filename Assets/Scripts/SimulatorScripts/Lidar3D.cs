using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lidar3D : MonoBehaviour
{
    public static float distance = 50;

    public GameObject[,] lidarObject = new GameObject[25,121];

    public RaycastHit hit;

    public float theta = 0;

    public double[,] lidarData = new double[25, 121];
    // Start is called before the first frame update
    void Start()
    {
        var temp = this.GetComponentsInChildren<Transform>();
        for (int i = 1; i <= 3025; i++)
        {
            lidarObject[(i - 1) / 121, (i - 1) % 121] = temp[i].gameObject;
            lidarObject[(i - 1) / 121, (i - 1) % 121].transform.Rotate((i - 1) / 121 - 12, (i - 1) % 121 - 60, 0);
            //Debug.Log(((i - 1) / 121 - 12) + ", " + ((i - 1) % 121 - 60));
        }
    }

    void Update()
    { 
        for (int i = 1; i <= 3025; i++)
        {
            lidarData[(i - 1) / 121, (i - 1) % 121] = lidarObject[(i - 1) / 121, (i - 1) % 121].GetComponent<LidarSensor>().distanceData;
        }
    }
}
