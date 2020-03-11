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

    private GameObject ProduceEffect(Vector3 pos, Vector3 rotation, int ID, bool isLoop, int layer)
    {
        GameObject effect;
        effect = GameObject.Instantiate(effectContainer, pos, Quaternion.Euler(rotation));
        effect.GetComponent<EffectContainer>().Init(ID, isLoop);
        effect.GetComponent<SpriteRenderer>().sortingOrder = layer;
        return effect;
    }

    private GameObject ProduceEffect(Vector3 pos, Vector3 rotation, int ID, bool isLoop, Color color, int layer)
    {
        GameObject effect;
        effect = GameObject.Instantiate(effectContainer, pos, Quaternion.Euler(rotation));
        effect.GetComponent<EffectContainer>().Init(ID, isLoop, color);
        effect.GetComponent<SpriteRenderer>().sortingOrder = layer;
        return effect;
    }

    private GameObject ProduceEffect(GameObject parent, Vector3 localPos, Vector3 rotation, int ID, bool isLoop, Color color, int layer)
    {
        GameObject effect;
        effect = GameObject.Instantiate(effectContainer, new Vector3(0,0,0), Quaternion.Euler(rotation));
        effect.GetComponent<EffectContainer>().Init(ID, isLoop, color);
        effect.GetComponent<SpriteRenderer>().sortingOrder = layer;
        effect.transform.parent = parent.transform;
        effect.transform.localPosition = localPos;
        return effect;
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

    public IEnumerator BuildExplosiveEffect(Vector3 pos)
    {
        ProduceEffect(pos, new Vector3(0,0,0), 0, false, 1);
        yield return null;
    }

    public IEnumerator BuildDustEffect(Vector3 pos)
    {
        yield return new WaitForEndOfFrame();
        ProduceEffect((pos + new Vector3(0f, -1.00f, 0f)), new Vector3(0,0,0), 4, false, 2);
        //ProduceEffect(effects, (pos + new Vector3(-0.6f, -0.85f, 0f)), new Vector3(0,0,0), 5, false, 2);
        //effects[1].GetComponent<SpriteRenderer>().flipX = true;
        yield return null;
    }

    public IEnumerator BuildFriendlySpawn(GameObject parent)
    {
        yield return new WaitForEndOfFrame();
        ProduceEffect(parent, new Vector3(0f, -0.1f, 0f), new Vector3(0,0,0), 53, false, Color.black, 2).GetComponent<Animator>().speed = 1.2f;
    }

    public IEnumerator BuildGrassEnemySpawn(GameObject parent)
    {
        yield return new WaitForEndOfFrame();
        ProduceEffect(parent, new Vector3(0f, -0.0f, 0f), new Vector3(0,0,0), 8, false, Color.white, 2);
    }

    public IEnumerator BuildLavaEnemySpawn(GameObject parent)
    {
        yield return new WaitForEndOfFrame();
        ProduceEffect(parent, new Vector3(0f, -0.0f, 0f), new Vector3(0,0,0), 8, false, new Color(0.83f, 0.23f, 0.23f), 2);
    }
}
