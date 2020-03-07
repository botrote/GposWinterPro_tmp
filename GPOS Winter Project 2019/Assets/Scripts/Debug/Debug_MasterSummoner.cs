using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_MasterSummoner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject product;
    private Camera camera;
    void Start()
    {
       camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            GameObject createdProduct = Instantiate(product);
            createdProduct.transform.position = camera.ScreenToWorldPoint(Input.mousePosition);
            createdProduct.transform.position += new Vector3 (0, 0, 10);
        }
    }
}
