using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainScript : MonoBehaviour
{
    public RectTransform Point;
    public RectTransform Compass;
    public double centerLat;
    public double centerLon;

    //constant in daejeon
    const double latGap = 0.001377;
    const double lonGap = 0.001717;

    public double mapSize = 800;

    public double pivotX = 50;
    public double pivotY = 50;

    public double LAT = 0;
    public double LON = 0;

    public float theta = 0;

    public double resultX;
    public double resultY;

    //Tracer Variable
    public GameObject TraceMarker;
    public Transform Tracer;
    public Text TraceBtnText;
    public bool isTraceMode = false;
    public float delay = 0.5f;
    float time = 0;

    //Point GeoLocationTest
    public InputField testLatText;
    public InputField testLonText;
    public InputField GapText;

    //Create PlayingField
    public InputField[] FieldGPS;
    public GameObject FieldObject;

    //CreateCoorList
    public GameObject CoorListPopUp;
    public GameObject GPSListObject;
    public Transform GPSListContentTransform;

    public double[] GPSCoorList;
  
    double ClickedLat;
    double ClickedLon;

    //Create Marker
    public GameObject Marker;
    public Transform MarkerTransform;
    float pointX, pointY;

    //CheckRosConnect
    public GameObject RosConnect;
    public GameObject ConnectCheckUI;
    public Text RosConnectText;
    public InputField IPAddress;

    //ChangeMode
    public GameObject[] ModeList;
    int ModeIndex = 0;

    //testPsi_D
    public float psi_d = -10000;
    public InputField psi_dInputfield;


    void Start()
    {
        ModeList[0].SetActive(true);
        GetComponent<GoogleMap>().GPSInput.text = "36.365004, 127.3445"; 
        GapText.text = "0.000001";
        testLatText.text = centerLat.ToString();
        testLonText.text = centerLon.ToString();
    }

    void Update()
    {
        if(RosConnect.GetComponent<RosSharp.RosBridgeClient.RosConnector>().isRosConnected)
            ConnectCheckUI.GetComponent<Image>().color = new Color((float)156/255, (float)229 /255, (float)107 /255);
        else
            ConnectCheckUI.GetComponent<Image>().color = new Color((float)229 /255, (float)107 /255, (float)113 /255);

        LAT = Math.Round(LAT, 6);
        LON = Math.Round(LON, 6);

        #region PointLocation
        if (LAT != 0 && LON != 0)
        {
            resultY = (LAT - centerLat + latGap / 2) / latGap * mapSize + pivotY;
            resultX = (LON - centerLon + lonGap / 2) / lonGap * mapSize + pivotX;
            Point.transform.position = new Vector3((float)resultX, (float)resultY);
        }
        Point.transform.eulerAngles = new Vector3(0, 0, -theta);
        Compass.transform.eulerAngles = new Vector3(0, 0, -theta);
        #endregion

        #region TracerCode
        time += Time.deltaTime;
        if (isTraceMode) TraceBtnText.text = "END";
        if (!isTraceMode) TraceBtnText.text = "START";

        if (isTraceMode && time > delay)
        {
            Instantiate(TraceMarker, new Vector3((float)resultX, (float)resultY),Quaternion.Euler(0, 0, -theta), Tracer.transform);
            time = 0;
        }

        //Point GeoLocationTest
        try
        {
            LAT = double.Parse(testLatText.text);
            LON = double.Parse(testLonText.text);
        }
        catch(Exception)
        {
            print("?????? ???? ??????????");
        }
        #endregion

        #region CreateGPSCoorList
        if (Input.GetMouseButtonDown(1))
        {
            if (Input.mousePosition.x >= 50 && Input.mousePosition.x <= 850 && Input.mousePosition.y >= 50 && Input.mousePosition.y <= 850)
            {
                pointX = Input.mousePosition.x;
                pointY = Input.mousePosition.y;
                ClickedLat = (Input.mousePosition.y - pivotY) * latGap / mapSize - (latGap / 2) + centerLat;
                ClickedLon = (Input.mousePosition.x - pivotX) * lonGap / mapSize - (lonGap / 2) + centerLon;

                ClickedLat = Math.Round(ClickedLat, 6);
                ClickedLon = Math.Round(ClickedLon, 6);

                CoorListPopUp.GetComponent<RectTransform>().position = new Vector3(Input.mousePosition.x + 125, Input.mousePosition.y + 50, 0);
                CoorListPopUp.SetActive(true);
            }
            else CoorListPopUp.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            CoorListPopUp.SetActive(false);
        #endregion
    }
    #region Trace
    public void TraceBtn()
    {
        if (isTraceMode) isTraceMode = false;
        else isTraceMode = true;
    }
    public void TraceDeleteBtn()
    {
        Transform[] TracerChild= Tracer.GetComponentsInChildren<Transform>();
        for(int i = 1; i < TracerChild.Length; i++)
        {
            Destroy(TracerChild[i].gameObject);
        }
    }
    #endregion
    #region GeoLocationTest
    public void LatPlusBtn()
    {
        LAT = double.Parse(testLatText.text) + double.Parse(GapText.text);
        testLatText.text = LAT.ToString();
    }
    public void LatMinusBtn()
    {
        LAT = double.Parse(testLatText.text) - double.Parse(GapText.text);
        testLatText.text = LAT.ToString();
    }
    public void LonPlusBtn()
    {
        LON = double.Parse(testLonText.text) + double.Parse(GapText.text);
        testLonText.text = LON.ToString();
    }
    public void LonMinusBtn()
    {
        LON = double.Parse(testLonText.text) - double.Parse(GapText.text);
        testLonText.text = LON.ToString();
    }
    #endregion


    public void CreatePlayingFieldBtn()
    {
        double[,] FieldData = new double[3, 2];
        for (int i = 0; i < FieldGPS.Length; i++)
        {
            string[] temp = FieldGPS[i].text.Split(',');
            resultY = (double.Parse(temp[0]) - centerLat + latGap / 2) / latGap * mapSize + pivotY;
            resultX = (double.Parse(temp[1]) - centerLon + lonGap / 2) / lonGap * mapSize + pivotX;

            FieldData[i, 0] = resultX;
            FieldData[i, 1] = resultY;
            print("asdasd" + resultX +" "+resultY);
        }
        double width = Math.Sqrt(Math.Pow(FieldData[0, 0] - FieldData[1, 0], 2) + Math.Pow(FieldData[0, 1] - FieldData[1, 1], 2));
        double height = Math.Sqrt(Math.Pow(FieldData[2, 0] - FieldData[1, 0], 2)+ Math.Pow(FieldData[1, 1] - FieldData[2, 1], 2));

        
        double FieldTheta = (FieldData[0, 0] == FieldData[1, 0])? 0 : Math.Atan(Math.Abs(FieldData[0, 1] - FieldData[1, 1]) / Math.Abs(FieldData[0, 0] - FieldData[1, 0])) * 180 / Math.PI;
        print(width);
        print(height);
        print(FieldTheta);

        FieldObject.SetActive(true);
        FieldObject.GetComponent<RectTransform>().sizeDelta = new Vector2((float)width, (float)height);
        FieldObject.transform.position = new Vector3((float)(FieldData[0, 0] + FieldData[2, 0]) / 2, (float)(FieldData[0, 1] + FieldData[2, 1]) / 2);
        print((float)(FieldData[0, 0] + FieldData[2, 0]) / 2 + ", " + (float)(FieldData[0, 1] + FieldData[2, 1]) / 2);
        FieldObject.transform.eulerAngles = new Vector3(0, 0, (float)FieldTheta);
    }
    public void DeletePlayingFieldBtn()
    {
        FieldObject.SetActive(false);
    }
    public void CreateCoorListBtn()
    {
        GameObject Object = Instantiate(GPSListObject,GPSListContentTransform);
        Object.GetComponentInChildren<Text>().text = ClickedLat.ToString() + ", " + ClickedLon.ToString();
        Instantiate(Marker, new Vector3(pointX, pointY), Quaternion.identity, MarkerTransform.transform);

        CoorListPopUp.SetActive(false);
    }


    public void MarkerDeleteBtn()
    {
        Transform[] MarkerChild = MarkerTransform.GetComponentsInChildren<Transform>();
        for (int i = 1; i < MarkerChild.Length; i++)
        {
            Destroy(MarkerChild[i].gameObject);
        }
    }
    public void PublishListBtn()
    {
        GPSCoorList = new double[2 * GPSListContentTransform.childCount];

        int count = 0;
        for (int i = 0; i < GPSListContentTransform.childCount; i++)
        {
            GPSCoorList[count++] = double.Parse(GPSListContentTransform.GetChild(i).GetChild(0).GetComponent<Text>().text.Split(',')[0]);
            GPSCoorList[count++] = double.Parse(GPSListContentTransform.GetChild(i).GetChild(0).GetComponent<Text>().text.Split(',')[1]);
        }
        for (int i = 0; i < GPSListContentTransform.childCount; i++)
        {
            print(GPSCoorList[2*i]+", "+GPSCoorList[2*i+1]);
        }
    }
    public void ConnectBtn()
    {
        if (RosConnectText.text == "CONNECT")
        {
            RosConnect.GetComponent<RosSharp.RosBridgeClient.RosConnector>().RosBridgeServerUrl = "ws://" + IPAddress.text + ":9090";
            RosConnect.SetActive(true);
            RosConnectText.text = "DISCONNECT";
        }
        else if (RosConnectText.text == "DISCONNECT")
        {
            RosConnect.SetActive(false);
            RosConnectText.text = "CONNECT";
        }
    }
    public void PreBtn()
    {
        if (ModeIndex == 0) ModeIndex = 3;
        else ModeIndex--;

        for (int i = 0; i < 4; i++)
        {
            ModeList[i].SetActive(false);
        }
        ModeList[ModeIndex].SetActive(true);
    }
    public void NextBtn()
    {
        if (ModeIndex == 3) ModeIndex = 0;
        else ModeIndex++;

        for (int i = 0; i < 4; i++)
        {
            ModeList[i].SetActive(false);
        }
        ModeList[ModeIndex].SetActive(true);
    }
    public void psi_dPublishBtn()
    {
        float temp = float.Parse(psi_dInputfield.text) + theta;
        if (temp > 180) temp -= 360;
        else if(temp < -180) temp += 360;
        psi_d = temp;
    }
}
