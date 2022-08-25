using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LidarSensor : MonoBehaviour
{
    float Distance = Lidar.distance;
    public RaycastHit hit;

    public float distanceData = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * Distance, Color.red);

        if (Physics.Raycast(transform.position, transform.forward, out hit, Distance))
        {
            distanceData = hit.distance;
        }
        else
            distanceData = 0;
    }
}
