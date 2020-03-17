using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_MassDamage : MonoBehaviour
{
    public bool ally;
    public bool enemy;
    public int damageValue;
    private GameObject[] units;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(ally)
                MassAllyDamage();
            if(enemy)
                MassEnemyDamage();
            StartCoroutine(GameObject.Find("Main Camera").GetComponent<CameraManager>().Shake(1f, 0.5f));
        }
    }

    void MassAllyDamage()
    {
        units = GameObject.FindGameObjectsWithTag("Friendly");
        for(int i = 0; i < units.Length; i++)
        {
            if(units[i].GetComponent<Player>() == null)
                units[i].GetComponent<Unit>().Damage(damageValue);
        }
    }

    void MassEnemyDamage()
    {
        units = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < units.Length; i++)
        {
            units[i].GetComponent<Unit>().Damage(damageValue);
        }
    }
}
