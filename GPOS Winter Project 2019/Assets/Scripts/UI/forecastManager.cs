using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class forecastManager : MonoBehaviour
{
    Camera cam;
    public GameObject[] forecastDials;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        GameObject.Find("Manager").GetComponent<WaveManager>().SpawnForecast += this.show;
        //forecastDials[0].SetActive(true);
        //forecastDials[0].GetComponent<forecastDial>().StartCountDown("Archer", 10);
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < forecastDials.Length; i++)
        {
            if (forecastDials[i].activeInHierarchy)
            {
                Vector2 PortalPos = (Vector2)cam.WorldToScreenPoint(GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[i]);
                PortalPos = PortalPos - new Vector2(cam.pixelWidth, cam.pixelHeight) / 2;
                Debug.Log("#" + i + ", " + PortalPos);
                if (PortalPos.y > cam.pixelHeight / 2 - 60 || PortalPos.y < -cam.pixelHeight / 2 + 60) PortalPos = PortalPos / Mathf.Abs(PortalPos.y) * (cam.pixelHeight / 2 - 60);
                if (PortalPos.x > cam.pixelWidth / 2 - 60 || PortalPos.x < -cam.pixelWidth / 2 + 60) PortalPos = PortalPos / Mathf.Abs(PortalPos.x) * (cam.pixelWidth / 2 - 60);
                forecastDials[i].transform.position = PortalPos + new Vector2(cam.pixelWidth, cam.pixelHeight) / 2;
            }
        }
    }

    protected void show(int portalnum, string name)
    {
        Debug.Log("forecast: " + portalnum + ", " + name);
        forecastDials[portalnum].SetActive(true);
        forecastDials[portalnum].GetComponent<forecastDial>().StartCountDown(name,10);
    }
}
