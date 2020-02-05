using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    protected Player player;
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Transform>().position = (Vector3)player.position + new Vector3(0,0,-10);
    }
}
