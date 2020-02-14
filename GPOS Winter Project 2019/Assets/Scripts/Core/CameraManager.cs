using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    protected Player player;
    protected Camera cam;
    public Vector3 oriPos;
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        cam = this.GetComponent<Camera>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = (Vector2)cam.WorldToScreenPoint(player.position) - new Vector2(cam.pixelWidth / 2 , cam.pixelHeight/2);
        //Debug.Log("in pixels : " + offset);
        //Debug.Log("Screentoworldpoint : " + (cam.ScreenToWorldPoint(offset + new Vector2(cam.pixelWidth / 2, cam.pixelHeight / 2)) -gameObject.transform.position));
        Vector2 CamoffsetinWorld = (cam.ScreenToWorldPoint(offset + new Vector2(cam.pixelWidth / 2, cam.pixelHeight / 2)) - gameObject.transform.position);
        float moveX = Mathf.Max(0, offset.x - cam.pixelWidth / 6) + Mathf.Min(0, offset.x + cam.pixelWidth / 6);
        float moveY = Mathf.Max(0, offset.y - cam.pixelHeight / 6) + Mathf.Min(0, offset.y + cam.pixelHeight / 6);
        gameObject.transform.Translate(cam.ScreenToWorldPoint(new Vector2(moveX, moveY) + new Vector2(cam.pixelWidth / 2, cam.pixelHeight / 2)) - gameObject.transform.position);
        oriPos = gameObject.transform.position;
        if(oriPos.y < -20)
        {
            oriPos = new Vector3(oriPos.x, -20, oriPos.z);
            gameObject.transform.position = oriPos;
        }
        else if (oriPos.y > +20)
        {
            oriPos = new Vector3(oriPos.x, +20, oriPos.z);
            gameObject.transform.position = oriPos;
        }
        if(oriPos.x < -64)
        {
            oriPos = new Vector3(-64, oriPos.y, oriPos.z);
            gameObject.transform.position = oriPos;
        }
        else if(oriPos.x > -2)
        {
            oriPos = new Vector3(-2, oriPos.y, oriPos.z);
            gameObject.transform.position = oriPos;
        }
    }
}
