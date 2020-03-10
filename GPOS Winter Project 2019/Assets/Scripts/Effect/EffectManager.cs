using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public GameObject effectContainer;
    private Color lightBlue;
    // Start is called before the first frame update
    void Awake()
    {
        ColorUtility.TryParseHtmlString("#15f9fb", out lightBlue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject ProduceEffect(List<GameObject> effects, Vector3 pos, Vector3 rotation, int ID, bool isLoop, Color color, int layer)
    {
        effects.Add(GameObject.Instantiate(effectContainer, pos, Quaternion.Euler(rotation)));
        effects[effects.Count - 1].GetComponent<EffectContainer>().Init(ID, isLoop, color);
        effects[effects.Count - 1].GetComponent<SpriteRenderer>().sortingOrder = layer;
        return effects[effects.Count - 1];
    }

    private GameObject ProduceEffect(List<GameObject> effects, Vector3 pos, Vector3 rotation, int ID, bool isLoop, int layer)
    {
        effects.Add(GameObject.Instantiate(effectContainer, pos, Quaternion.Euler(rotation)));
        effects[effects.Count - 1].GetComponent<EffectContainer>().Init(ID, isLoop);
        effects[effects.Count - 1].GetComponent<SpriteRenderer>().sortingOrder = layer;
        return effects[effects.Count - 1];
    }

    private IEnumerator WaitFrames(int n)
    {
        for(int i = 0; i < n; i++)
            yield return null;
    }

    public IEnumerator BuildBluePortalEffect(Vector3 pos)
    {
        List<GameObject> effects = new List<GameObject>();
        Transform parent = GameObject.Find("MapManager").transform.Find("Portals");
        yield return new WaitForEndOfFrame();
        ProduceEffect(effects, pos, new Vector3(0,0,0), 35, true, -1).transform.parent = parent;
        ProduceEffect(effects, pos, new Vector3(0,0,0), 56, true, Color.cyan, -2).transform.parent = parent;
        yield return StartCoroutine(WaitFrames(34));
        ProduceEffect(effects, pos, new Vector3(0,0,0), 56, true, Color.cyan, -2).transform.parent = parent;
        yield return StartCoroutine(WaitFrames(34));
        ProduceEffect(effects, pos, new Vector3(0,0,0), 56, true, Color.cyan, -2).transform.parent = parent;
        yield return null;
    }

    public IEnumerator BuildRedPortalEffect(Vector3 pos)
    {
        List<GameObject> effects = new List<GameObject>();
        Transform parent = GameObject.Find("MapManager").transform.Find("Portals");
        yield return new WaitForEndOfFrame();
        ProduceEffect(effects, pos, new Vector3(0,0,0), 35, true, Color.red, -1).transform.parent = parent;
        ProduceEffect(effects, pos, new Vector3(0,0,0), 56, true, Color.red, -2).transform.parent = parent;
        yield return StartCoroutine(WaitFrames(34));
        ProduceEffect(effects, pos, new Vector3(0,0,0), 56, true, Color.red, -2).transform.parent = parent;
        yield return StartCoroutine(WaitFrames(34));
        ProduceEffect(effects, pos, new Vector3(0,0,0), 56, true, Color.red, -2).transform.parent = parent;
        yield return null;
    }
}
