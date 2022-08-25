using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMove : MonoBehaviour
{
    public float speed = 10;
    public float turnSpeed = 10;

    public float maxSpeed = 10;
    public float maxAngSpeed = 5;

    float pL = 0, pR = 0;

    Rigidbody Boat;
    // Start is called before the first frame update
    void Start()
    {
        Boat = GetComponent<Rigidbody>();
        Boat.maxAngularVelocity = 5;
        Boat.maxDepenetrationVelocity = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Boat.AddRelativeForce(Vector3.forward * speed, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Boat.AddRelativeForce(-Vector3.forward * speed, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Boat.transform.Rotate(new Vector3(0, -turnSpeed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            Boat.transform.Rotate(new Vector3(0, turnSpeed * Time.deltaTime, 0));
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.transform.position = new Vector3(0, 0, 0);
            this.transform.rotation = Quaternion.identity;
            Boat.velocity = Vector3.zero;
        }
       
    }
}
