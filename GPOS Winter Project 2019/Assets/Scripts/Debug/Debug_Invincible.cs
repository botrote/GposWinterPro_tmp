using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_Invincible : MonoBehaviour
{
    // Start is called before the first frame update
    Player player;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        player.Heal(9999);
    }
}
