using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScreenManager : MonoBehaviour
{
    public Camera cam;
    public RectTransform rect;
    public GameObject Boundary;
    // Start is called before the first frame update
    void Awake()
    {
        Vector2 size = (cam.ScreenToWorldPoint(new Vector2(cam.scaledPixelWidth, cam.scaledPixelHeight)) - cam.ScreenToWorldPoint(new Vector2(0,0))) * 5 / 2;
        rect = gameObject.GetComponent<RectTransform>();
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
        rect.localPosition = new Vector2(0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentCamPos = (Vector2)(cam.transform.position - Boundary.transform.position);
        rect.localPosition = currentCamPos * 5 / 2;
    }
}
