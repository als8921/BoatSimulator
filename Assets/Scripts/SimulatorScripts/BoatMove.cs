using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatMove : MonoBehaviour
{
    public static double[] PWM = { 1500, 1500 };
    public float speed = 10;
    public float turnSpeed = 10;
    public Text timeText;

    bool isEnd = false;
    float time = 0;
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
        timeText.text = time.ToString() + "s";
        if (transform.position.x < 1)
            time = 0;
        else if (transform.position.x < 90)
        {
            time += Time.deltaTime;
        }
        if (transform.position.x > 90)
        {
            isEnd = true;
            Debug.Log("기록 : " + time);
        }

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
            isEnd = false;
            this.transform.position = new Vector3(0, 0, 3);
            this.transform.rotation = Quaternion.Euler(0, 90, 0);
            Boat.velocity = Vector3.zero;
        }

        float pl = 1500 - (float)PWM[0];
        float pr = (float)PWM[1] - 1500;

        this.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * speed * (pl + pr), ForceMode.Acceleration);
        this.GetComponent<Rigidbody>().AddTorque(0, turnSpeed * (pl - pr), 0, ForceMode.Acceleration);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!isEnd)
        {
            time += 15;
            Debug.Log("Crashed!!");
        }
    }
}
