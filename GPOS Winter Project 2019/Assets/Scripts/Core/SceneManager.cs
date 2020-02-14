using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{ 
    public static void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
    public static void LoadBattleScene()
    {
        SceneManager.LoadScene("BattleScene");
    }

    public static void LoadDeadScene()
    {
        SceneManager.LoadScene("DeadScene");
    }

    public static void LoadClearScene()
    {
        SceneManager.LoadScene("ClearScene");
    }

}
