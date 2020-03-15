using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("Player") == null)
            SceneLoader.LoadDeadScene();
    }

    /// <summary>
    /// n초동안 게임 전체를 정지시킴
    /// </summary>
    void StopTime(float n)
    {
        StartCoroutine(StopTimeCoroutine(n));
    }

    IEnumerator StopTimeCoroutine(float n)
    {
        if(n <= 0)
            yield break;   

        Time.timeScale = 0;
        float originTime = Time.realtimeSinceStartup;
        float deltaTime = 0;
        float seconds = 0;
        while(seconds < n)
        {
            seconds += deltaTime;
            deltaTime = Time.realtimeSinceStartup - originTime;
            originTime = Time.realtimeSinceStartup;
            yield return null;
        }
        Time.timeScale = 1;
    }

}
