using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    protected Player player;
    protected Camera cam;
    public Vector3 oriPos;
    private MapManager mapManager;
    // Start is called before the first frame update
    private void Awake()
    {
        cam = this.GetComponent<Camera>();
        mapManager = GameObject.Find("MapManager").GetComponent<MapManager>();
    }

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        gameObject.transform.position = player.transform.position - new Vector3(0, 0, 10);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 offset = (Vector2)cam.WorldToScreenPoint(player.position) - new Vector2(cam.pixelWidth / 2 , cam.pixelHeight/2);
        //Debug.Log("in pixels : " + offset);
        //Debug.Log("Screentoworldpoint : " + (cam.ScreenToWorldPoint(offset + new Vector2(cam.pixelWidth / 2, cam.pixelHeight / 2)) -gameObject.transform.position));
        Vector2 CamoffsetinWorld = (cam.ScreenToWorldPoint(offset + new Vector2(cam.pixelWidth / 2, cam.pixelHeight / 2)) - gameObject.transform.position);
        float moveX = Mathf.Max(0, offset.x - cam.pixelWidth / 6) + Mathf.Min(0, offset.x + cam.pixelWidth / 6);
        float moveY = Mathf.Max(0, offset.y - cam.pixelHeight / 6) + Mathf.Min(0, offset.y + cam.pixelHeight / 6);
        gameObject.transform.Translate(cam.ScreenToWorldPoint(new Vector2(moveX, moveY) + new Vector2(cam.pixelWidth / 2, cam.pixelHeight / 2)) - gameObject.transform.position);
        oriPos = gameObject.transform.position;
        
        if(oriPos.y < (mapManager.GetCameraMinimumY()))
        {
            oriPos = new Vector3(oriPos.x, (mapManager.GetCameraMinimumY()), oriPos.z);
            gameObject.transform.position = oriPos;
        }
        else if (oriPos.y > mapManager.GetCameraMaximumY())
        {
            oriPos = new Vector3(oriPos.x, mapManager.GetCameraMaximumY(), oriPos.z);
            gameObject.transform.position = oriPos;
        }
        if(oriPos.x < (mapManager.GetCameraMinimumX()))
        {
            oriPos = new Vector3((mapManager.GetCameraMinimumX()), oriPos.y, oriPos.z);
            gameObject.transform.position = oriPos;
        }
        else if(oriPos.x > mapManager.GetCameraMaximumX())
        {
            oriPos = new Vector3(mapManager.GetCameraMaximumX(), oriPos.y, oriPos.z);
            gameObject.transform.position = oriPos;
        }
        
    }

    public IEnumerator Shake(float _amount,float _duration)
    {
        Vector3 originPos = cam.gameObject.transform.localPosition;
        float timer = 0;
        while(timer <= _duration)
        {
            transform.localPosition = (Vector3)Random.insideUnitCircle * _amount + originPos;
 
            timer += Time.deltaTime;
            yield return null;
        }
        cam.gameObject.transform.localPosition = originPos;
    } 
}
