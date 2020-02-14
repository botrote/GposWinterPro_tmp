using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private GameObject boundary;
    private int randomFactor;
    public GameObject grassTilemap;
    public GameObject lavaTilemap;

    void Awake()
    {
        boundary = GameObject.Find("Boundary");
        randomFactor = Random.Range(-32, 32);
        boundary.transform.position = new Vector3(randomFactor, 0, 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 GetCenterPos()
    {
        return new Vector2(randomFactor, 0);
    }

    public int GetMaximumX()
    {
        return (int)GetCenterPos().x + 40;
    }

    public int GetMinimumX()
    {
        return (int)GetCenterPos().x - 40;
    }

    public int GetMaximumY()
    {
        return (int)GetCenterPos().y + 20;
    }

    public int GetMinimumY()
    {
        return (int)GetCenterPos().y - 20;
    }

    public int GetCameraMaximumX()
    {
        return GetMaximumX() - 10;
    }

    public int GetCameraMinimumX()
    {
        return GetMinimumX() + 10;
    }

    public int GetCameraMaximumY()
    {
        return GetMaximumY() - 1;
    }

    public int GetCameraMinimumY()
    {
        return GetMinimumY() + 1;
    }

    public Vector2[] GetPortalPos()
    {
        Vector2[] portalPoses = new Vector2[8];
        portalPoses[0] = GetCenterPos() + new Vector2(-35, +20);
        portalPoses[1] = GetCenterPos() + new Vector2(+0, +20);
        portalPoses[2] = GetCenterPos() + new Vector2(+35, +20);
        portalPoses[3] = GetCenterPos() + new Vector2(-35, +0);
        portalPoses[4] = GetCenterPos() + new Vector2(+35, +0);
        portalPoses[5] = GetCenterPos() + new Vector2(-35, -20);
        portalPoses[6] = GetCenterPos() + new Vector2(+0, -20);
        portalPoses[7] = GetCenterPos() + new Vector2(+35, -20);
        return portalPoses;
    }

    public bool IsOutOfBoundary(Vector2 testPos)
    {
        return (testPos.x > GetMaximumX() || testPos.y > GetMaximumY() || testPos.x < GetMinimumX() || testPos.y < GetMinimumY());
    }
}
