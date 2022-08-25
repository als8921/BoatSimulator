using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PWMController : MonoBehaviour
{

    public bool AutoMode = false;
    public GameObject WayPoints;

    public Slider LSlider;
    public Slider RSlider;

    public float EndX = 900;
    public float EndY = 200;

    public float SurgeConstant = 1;
    public float RotateConstant = 1;

    public float psi_d = 0;

    float dt = 0.02f;
    public float x, y;
    public float psi;

    float psi_error;
    float psi_error_dot;
    float psi_error_past = 0;

    public float P_gain = 0.1f;
    public float D_gain = 0.1f;

    public float tau_N;
    public float tau_X = 300;

    public float RPWM, LPWM;

    public List<float[]> WP;
    public int k = 0;

    public float Delta = 50;
    public float end_range = 10;

    bool isEnd = false;

    Rigidbody boat;

    void Start()
    {
        boat = this.GetComponent<Rigidbody>();  
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            boat.transform.position = new Vector3(0, 0, 0);
            boat.transform.rotation = Quaternion.identity;
            k = 0;
            RPWM = 1500;
            LPWM = 1500;
        }

        x = boat.transform.position.x;
        y = boat.transform.position.z;

        psi = (boat.transform.rotation.eulerAngles.y > 180) ? boat.transform.rotation.eulerAngles.y - 360 : boat.transform.rotation.eulerAngles.y;


        WP = new List<float[]>();
        foreach (Transform child in WayPoints.GetComponentsInChildren<Transform>())
        {
            WP.Add(new float[2] { child.position.x, child.position.z });
        }

        #region TEST

        //if (!AutoMode)
        //{
        //    if (!isEnd)
        //    {
        //        if (Mathf.Pow(WP[k + 1][0] - x, 2) + Mathf.Pow(WP[k + 1][1] - y, 2) < end_range * end_range)
        //        {
        //            Debug.Log(k + 1 + "번째 WayPoint Pass!!");
        //            k++;
        //        }

        //        if (k == WP.Count - 1)
        //        {
        //            Debug.Log("CLEAR!!");
        //            k = 0;
        //            //isEnd = true;
        //        }
        //        else
        //        {
        //            float pi_p = Mathf.Atan2(WP[k + 1][0] - WP[k][0], WP[k + 1][1] - WP[k][1]);
        //            float y_e = (x - WP[k][0]) * Mathf.Cos(pi_p) - (y - WP[k][1]) * Mathf.Sin(pi_p);
        //            pi_p = pi_p * 180 / Mathf.PI;
        //            psi_d = pi_p - Mathf.Atan(y_e / Delta) * 180 / Mathf.PI;
        //        }
        //    }
        //}


        //if (psi > 180) psi -= 360;
        //if (psi_d > 180) psi_d -= 360;


        //psi_error = psi_d - psi;
        //if (psi_error > 180) psi_error -= 360;
        //else if (psi_error < -180) psi_error += 360;


        //psi_error_dot = (psi_error - psi_error_past) / dt;
        //psi_error_past = psi_error;

        //tau_N = P_gain * psi_error + D_gain * psi_error_dot;

        //if (tau_N > 2 * tau_X) tau_N = 2 * tau_X;
        //else if (tau_N < -2 * tau_X) tau_N = -2 * tau_X;

        //if (Mathf.Pow(EndX - x, 2) + Mathf.Pow(EndY - y, 2) < Mathf.Pow(end_range, 2))
        //{
        //    boat.transform.position = new Vector3(EndX, 0, EndY);
        //    boat.transform.rotation = Quaternion.identity;
        //    Debug.Log("Finish");
        //}

        //RPWM = tau_X - tau_N/2 + 1000;
        //LPWM = tau_X + tau_N/2 + 1000;

        #endregion


        float PL = 1500 - LPWM;
        float PR = RPWM - 1500;

        LSlider.value = PL;
        RSlider.value = PR;
        tau_X = 100 + PL + PR;
        tau_N = PL - PR;
        if (RPWM == 1500 && LPWM == 1500)
            tau_X = 0;
        boat.AddRelativeForce(Vector3.forward * SurgeConstant * tau_X, ForceMode.Acceleration);
        boat.AddTorque(0, RotateConstant * tau_N, 0, ForceMode.Acceleration);

    }
}
