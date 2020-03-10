﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
*   MasterSummoner 
*
*
*/
public class Debug_MasterSummoner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject productA;
    public GameObject productS;
    public GameObject productD;
    public GameObject productF;
    private Camera camera;
    void Start()
    {
       camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) && productA != null)
        {
            GameObject createdProduct = Instantiate(productA);
            createdProduct.transform.position = camera.ScreenToWorldPoint(Input.mousePosition);
            createdProduct.transform.position += new Vector3 (0, 0, 10);
        }
        else if(Input.GetKeyDown(KeyCode.S) && productS != null)
        {
            GameObject createdProduct = Instantiate(productS);
            createdProduct.transform.position = camera.ScreenToWorldPoint(Input.mousePosition);
            createdProduct.transform.position += new Vector3 (0, 0, 10);
        }
        else if(Input.GetKeyDown(KeyCode.D) && productD != null)
        {
            GameObject createdProduct = Instantiate(productD);
            createdProduct.transform.position = camera.ScreenToWorldPoint(Input.mousePosition);
            createdProduct.transform.position += new Vector3 (0, 0, 10);
        }
        else if(Input.GetKeyDown(KeyCode.F) && productF != null)
        {
            GameObject createdProduct = Instantiate(productF);
            createdProduct.transform.position = camera.ScreenToWorldPoint(Input.mousePosition);
            createdProduct.transform.position += new Vector3 (0, 0, 10);
        }
    }
}
