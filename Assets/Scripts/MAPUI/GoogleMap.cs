using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GoogleMap : MonoBehaviour
{
    public RawImage testImage;
    public InputField GPSInput;
    public string APIkey;
    string URL;

    public double Lat;
    public double Lon;


    IEnumerator LoadImage(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();
        testImage.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        GPSParse();
    }
    public void GetMapBtnClicked()
    {
        URLmake();
        StartCoroutine(LoadImage(URL));
    }

    public void GPSParse()
    {
        if(GPSInput.text != null)
        {
            string[] GPSData;
            GPSData = GPSInput.text.Split(',');
            Lat = double.Parse(GPSData[0]);
            Lon = double.Parse(GPSData[1]);
            GetComponent<UIMainScript>().LAT = Lat;
            GetComponent<UIMainScript>().LON = Lon;
            GetComponent<UIMainScript>().centerLat = Lat;
            GetComponent<UIMainScript>().centerLon = Lon;
            GetComponent<UIMainScript>().testLatText.text = Lat.ToString();
            GetComponent<UIMainScript>().testLonText.text = Lon.ToString();
        }
    }
    public void URLmake()
    {
        URL = "https://maps.googleapis.com/maps/api/staticmap?"
        + "center=" + GPSInput.text
        + "&zoom=19"
        + "&maptype=satellite"
        + "&size=640x640&scale=2"
        + "&key=" + APIkey;
    }
}
