using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minion : NPC
{
    public abstract ushort Notch
    {
        get;
    }

    
    // Start is called before the first frame update
    void Start()
    {
        //플레이어 델리게이트 추가
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        //플레이어 델리게이트 해제
    }
}
