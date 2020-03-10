using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private GameObject effectBuilder;
    private List<GameObject> effects;
    // Start is called before the first frame update
    void Awake()
    {
        effectBuilder = Resources.Load("Prefabs/EffectBuilder") as GameObject;
        effects = new List<GameObject>();
        StartCoroutine(EffectMake());
    }

    private IEnumerator EffectMake()
    {
        Color shellColor;
        Color vortexColor;
        Color lightBlue;
        Color red = Color.red;
        Color origin = Color.white;
        ColorUtility.TryParseHtmlString("#15f9fb", out lightBlue);

        yield return new WaitForEndOfFrame();

        bool isLava = GameObject.Find("MapManager").GetComponent<MapManager>().isLava;

        if(isLava)
        {
            shellColor = red;
            vortexColor = red;
        }
        else
        {
            shellColor = origin;
            vortexColor = lightBlue;
        }

        effects.Add(GameObject.Instantiate(effectBuilder, gameObject.transform));
        effects[0].GetComponent<EffectManager>().Init(35, true, shellColor);

        effects.Add(GameObject.Instantiate(effectBuilder, gameObject.transform));
        effects[1].GetComponent<EffectManager>().Init(56, true, vortexColor);
        effects[1].GetComponent<SpriteRenderer>().sortingOrder = -1;

        for(int i = 0; i < 34; i ++)
        {
            yield return null;
        }

        effects.Add(GameObject.Instantiate(effectBuilder, gameObject.transform));
        effects[2].GetComponent<EffectManager>().Init(56, true, vortexColor);
        effects[2].GetComponent<SpriteRenderer>().sortingOrder = -1;

        for(int i = 0; i < 34; i ++)
        {
            yield return null;
        }

        effects.Add(GameObject.Instantiate(effectBuilder, gameObject.transform));
        effects[3].GetComponent<EffectManager>().Init(56, true, vortexColor);
        effects[3].GetComponent<SpriteRenderer>().sortingOrder = -1;

        yield return null;
    }
}
