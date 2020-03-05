using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_InfiniteMoney : MonoBehaviour
{
    // Start is called before the first frame update
    Player player;
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.getExp() < int.MaxValue - 1000)
            player.addExp(1000);
    }
}
