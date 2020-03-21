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

    public static void LookAtAndMove(GameObject source, Vector3 dest, float distance)
    {
        Vector3 sourcePos = source.transform.position;
        Vector3 lineVector = new Vector3(dest.x - sourcePos.x, dest.y - sourcePos.y, 0);
        float degree = Mathf.Atan2(lineVector.x, lineVector.y) * Mathf.Rad2Deg;
        source.transform.localRotation = Quaternion.Euler(0, 0, (-1)*degree);
        source.transform.position += lineVector.normalized * (distance);
    }

    public IEnumerator BuildBluePortalEffect(Vector3 pos)
    {
        List<GameObject> effects = new List<GameObject>();
        Transform parent = GameObject.Find("MapManager").transform.Find("Portals");
        yield return new WaitForEndOfFrame();
        ProduceEffect(effects, pos, new Vector3(0,0,0), 35, true, Color.white, -1).transform.parent = parent;
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
        ProduceEffect(pos, new Vector3(0,0,0), 0, false, Color.white, 1);
        yield return null;
    }

    public IEnumerator BuildBurnEffect(Vector3 pos)
    {
        ProduceEffect(pos, new Vector3(0,0,0), 1, false, Color.white, 1);
        yield return null;
    }

    public IEnumerator BuildDustEffect(Vector3 pos)
    {
        ProduceEffect((pos + new Vector3(0f, -1.00f, 0f)), new Vector3(0,0,0), 4, false, Color.white, 2);
        yield return null;
    }

    public IEnumerator BuildSingleDust(GameObject parent, GameObject target)
    {
        GameObject effect;
        bool flipX = (parent.transform.position.x < target.transform.position.x);
        effect = ProduceEffect(parent, new Vector3(0f, 0.00f, 0f), new Vector3(0,0,0), 5, false, Color.white, 2);
        effect.GetComponent<SpriteRenderer>().flipX = flipX;
        if(flipX)
            effect.transform.localPosition -= new Vector3(0.4f, 0f, 0f);
        else
            effect.transform.localPosition += new Vector3(0.4f, 0f, 0f);
        yield return null;
    }

    public IEnumerator BuildFriendlySpawn(GameObject parent)
    {
        yield return new WaitForEndOfFrame();
        ProduceEffect(parent, new Vector3(0f, -0.1f, 0f), new Vector3(0,0,0), 53, false, Color.black, 2).GetComponent<Animator>().speed = 1.2f;
    }

    public IEnumerator BuildZombieSpawn(GameObject richKing, GameObject zombie)
    {
        yield return new WaitForEndOfFrame();
        List<GameObject> effects = new List<GameObject>();
        effects.Add(ProduceEffect(richKing, new Vector3(0f, -0.20f, 0f), new Vector3(0,0,0), 57, false, Color.green, -5));
        effects[0].transform.localScale = new Vector3(0.5f, 0.5f, 0f);
        effects.Add(ProduceEffect(zombie, new Vector3(0f, +0.1f, 0f), new Vector3(0,0,0), 44, false, Color.white, 2));
        effects[1].GetComponent<Animator>().speed = 1.2f;
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

    public IEnumerator BuildDeathProjectile(GameObject parent)
    {
        //yield return new WaitForEndOfFrame();
        GameObject effect = ProduceEffect(parent, new Vector3(0f, -0.0f, 0f), new Vector3(0,0,0), 30, true, Color.black, 2);
        effect.transform.rotation = parent.transform.rotation;
        effect.GetComponent<SpriteRenderer>().flipY = true;
        effect.GetComponent<Animator>().speed = 1.5f;
        yield return null;
    }

    public IEnumerator BuildDeathExplosion(Vector3 pos)
    {
        ProduceEffect(pos, new Vector3(0,0,0), 18, false, Color.black, 1);
        yield return null;
    }

    public IEnumerator BuildHolyProjectile(GameObject parent)
    {
        //yield return new WaitForEndOfFrame();
        GameObject effect = ProduceEffect(parent, new Vector3(0f, -0.0f, 0f), new Vector3(0,0,0), 27, true, Color.yellow, 2);
        effect.transform.rotation = parent.transform.rotation;
        effect.GetComponent<SpriteRenderer>().flipY = true;
        effect.GetComponent<Animator>().speed = 1.5f;
        yield return null;
    }

    public IEnumerator BuildHolyExplosion(Vector3 pos)
    {
        ProduceEffect(pos, new Vector3(0,0,0), 22, false, Color.white, 1);
        yield return null;
    }

    public IEnumerator BuildFlameProjectile(GameObject parent)
    {
        //yield return new WaitForEndOfFrame();
        GameObject effect = ProduceEffect(parent, new Vector3(0f, -0.0f, 0f), new Vector3(0,0,0), 29, true, Color.white, 2);
        effect.transform.rotation = parent.transform.rotation;
        effect.GetComponent<SpriteRenderer>().flipY = true;
        effect.GetComponent<Animator>().speed = 2.0f;
        yield return null;
    }

    public IEnumerator BuildFlameCast(GameObject parent)
    {
        //yield return new WaitForEndOfFrame();
        GameObject effect;
        effect = ProduceEffect(parent, new Vector3(0,-0.2f,0), new Vector3(0,0,0), 32, false, Color.white, +1);
        effect.GetComponent<Animator>().speed = 1.5f;
        effect.transform.localScale += new Vector3(0.5f, 0f, 0f);
        yield return null;
    }

    public IEnumerator BuildFriendlySlash(GameObject parent, Vector3 target)
    {
        //yield return new WaitForEndOfFrame();
        GameObject effect =  ProduceEffect(parent, new Vector3(0,0,0), new Vector3(0,0,30), 62, false, Color.red, 2);
        LookAtAndMove(effect, target, 0.3f); 
        yield return null;
    }

    public IEnumerator BuildEnemySlash(GameObject parent, Vector3 target)
    {
        //yield return new WaitForEndOfFrame();
        GameObject effect =  ProduceEffect(parent, new Vector3(0,0,0), new Vector3(0,0,30), 62, false, Color.white, 2);
        LookAtAndMove(effect, target, 0.3f);
        yield return null; 
    }

    public IEnumerator BuildEnemySting(GameObject parent, Vector3 target)
    {
        GameObject effect =  ProduceEffect(parent, new Vector3(0,0,0), new Vector3(0,0,30), 63, true, Color.white, 2);
        LookAtAndMove(effect, target, 0.3f);
        effect.transform.localScale = new Vector3(1.0f, 0.5f, 1f);
        for(int i = 0; i < 20; i++)
        {
            if(i == 3)
                effect.transform.localScale = new Vector3(0.9f, 0.8f, 1f);
            if(i == 7)
                effect.transform.localScale = new Vector3(0.7f, 1.5f, 1f);
            yield return null;
        }
        effect.transform.localScale = new Vector3(0.5f, 2.25f, 1f); 
        LookAtAndMove(effect, target, 0.9f);
        yield return StartCoroutine(WaitFrames(10));
        Destroy(effect);
        yield return null;
    }

    public IEnumerator BuildFriendlyWildSlash(GameObject parent, Vector3 target)
    {
        List<GameObject> effects = new List<GameObject>();
        effects.Add(ProduceEffect(parent, new Vector3(0,0,0), new Vector3(0,0,30), 62, false, Color.red, 2));
        LookAtAndMove(effects[0], target, 0.3f);
        effects[0].transform.Rotate(new Vector3(0,0,30));
        yield return StartCoroutine(WaitFrames(3));
        effects.Add(ProduceEffect(parent, new Vector3(0,0,0), new Vector3(0,0,30), 62, false, Color.red, 2));
        LookAtAndMove(effects[1], target, 0.3f);
        effects[1].transform.Rotate(new Vector3(0,0,-30));
        yield return null;
    }

    public IEnumerator BuildEnemyWildSlash(GameObject parent, Vector3 target)
    {
        List<GameObject> effects = new List<GameObject>();

        effects.Add(ProduceEffect(parent, new Vector3(0,0,0), new Vector3(0,0,30), 62, false, Color.white, 2));
        LookAtAndMove(effects[0], target, 0.6f);
        effects[0].transform.Rotate(new Vector3(0,0,25));
        effects[0].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        yield return StartCoroutine(WaitFrames(3));

        effects.Add(ProduceEffect(parent, new Vector3(0,0,0), new Vector3(0,0,30), 62, false, Color.white, 2));
        LookAtAndMove(effects[1], target, 0.6f);
        effects[1].transform.Rotate(new Vector3(0,0,0));
        effects[1].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        yield return StartCoroutine(WaitFrames(3));

        effects.Add(ProduceEffect(parent, new Vector3(0,0,0), new Vector3(0,0,30), 62, false, Color.white, 2));
        LookAtAndMove(effects[2], target, 0.6f);
        effects[2].transform.Rotate(new Vector3(0,0,-25));
        effects[2].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        yield return null;
    }

    public IEnumerator BuildFriendlyScissor(GameObject parent, Vector3 target)
    {
        List<GameObject> effects = new List<GameObject>();
        effects.Add(ProduceEffect(parent, new Vector3(0,0,0), new Vector3(0,0,30), 62, false, Color.red, 2));
        LookAtAndMove(effects[0], target, 0.35f);
        effects[0].transform.Rotate(new Vector3(0,0,-40));
        effects[0].transform.localScale = new Vector3(1, 0.4f, 1);
        yield return StartCoroutine(WaitFrames(15));
        effects.Add(ProduceEffect(parent, new Vector3(0,0,0), new Vector3(0,0,30), 62, false, Color.red, 2));
        LookAtAndMove(effects[1], target, 0.35f);
        effects[1].transform.Rotate(new Vector3(0,0,40));
        effects[1].transform.localScale = new Vector3(1, 0.4f, 1);
        effects[1].GetComponent<SpriteRenderer>().flipX = true;
        yield return null;
    }

    public IEnumerator BuildFriendlyClaw(GameObject parent, GameObject target)
    {
        List<GameObject> effects = new List<GameObject>();
        Vector3 moveVector = (target.transform.position - parent.transform.position);
        SpriteRenderer sRenderer;
        bool flipY = !(moveVector.y > 0);

        effects.Add(ProduceEffect(target, new Vector3(0,0,0), new Vector3(0,0,0), 62, false, Color.red, 2));
        effects[0].transform.Rotate(new Vector3(0,0,-45));
        effects[0].transform.localScale = new Vector3(1, 0.4f, 1);
        effects[0].transform.localPosition += new Vector3(0.15f, 0.15f, 0);
        sRenderer = effects[0].GetComponent<SpriteRenderer>();
        sRenderer.flipY = flipY;

        effects.Add(ProduceEffect(target, new Vector3(0,0,0), new Vector3(0,0,0), 62, false, Color.red, 2));
        effects[1].transform.Rotate(new Vector3(0,0,-45));
        effects[1].transform.localScale = new Vector3(1, 0.4f, 1);
        sRenderer = effects[1].GetComponent<SpriteRenderer>();
        sRenderer.flipY = flipY;

        effects.Add(ProduceEffect(target, new Vector3(0,0,0), new Vector3(0,0,0), 62, false, Color.red, 2));
        effects[2].transform.Rotate(new Vector3(0,0,-45));
        effects[2].transform.localScale = new Vector3(1, 0.4f, 1);
        effects[2].transform.localPosition -= new Vector3(0.15f, 0.15f, 0);
        sRenderer = effects[2].GetComponent<SpriteRenderer>();
        sRenderer.flipY = flipY;

        yield return null;
    }

    public IEnumerator BuildEnemySmite(GameObject parent, Vector3 target)
    {
        Vector3 moveVector = (target - parent.transform.position);
        StartCoroutine(GameObject.Find("Main Camera").GetComponent<CameraManager>().Shake(0.15f, 0.5f));
        List<GameObject> effects = new List<GameObject>();
        effects.Add(ProduceEffect(parent, new Vector3(0,0,0), new Vector3(0,0,0), 2, false, new Color(0.5f, 0.35f, 0f), -5));
        effects[0].transform.localPosition += moveVector.normalized * (1f);
        effects[0].GetComponent<Animator>().speed = 1.8f;
        yield return StartCoroutine(WaitFrames(18));
        effects.Add(ProduceEffect(parent, new Vector3(0,0,0), new Vector3(0,0,0), 3, false, new Color(0.52f, 0.35f, 0.15f), 2));
        effects[1].transform.localPosition += moveVector.normalized * (1f);
        effects[1].transform.localPosition += new Vector3(0f, -0.6f, 0f);
        effects[1].GetComponent<Animator>().speed = 1.8f;
        //LookAtAndMove(effects[0], target, 0.35f);
        yield return null;
    }

    public IEnumerator BuildLichMagicCircle(GameObject Summoner, GameObject Target)
    {
        List<GameObject> effects = new List<GameObject>();
        effects.Add(ProduceEffect(Target, new Vector3(0,0,0), new Vector3(0,0,0), 57, false, Color.black, -5));
        effects.Add(ProduceEffect(Summoner, new Vector3(0,-0.20f,0), new Vector3(0,0,0), 40, false, Color.black, -5));
        yield return null;
    }

    public IEnumerator BuildFriendlyHeal(GameObject parent)
    {
        
        GameObject effect;
        effect = ProduceEffect(parent, new Vector3(0,+0.05f,0), new Vector3(0,0,0), 54, false, new Color(1,1,1,0.8f), 2);
        effect.name = "Healed";
        effect.GetComponent<Animator>().speed = 1.5f;
        
        yield return null;
    }

    public IEnumerator BuildEnemyHeal(GameObject parent)
    {
        if(parent.transform.Find("Healed") == null && !parent.name.Equals("Cleric(Clone)"))
        {
            GameObject effect;
            effect = ProduceEffect(parent, new Vector3(0f,0f,0f), new Vector3(0,0,0), 10, false, Color.green, 2);
            effect.name = "Healed";
            effect.GetComponent<Animator>().speed = 0.60f;
        }
        yield return null;
    }

    public IEnumerator BuildEnemyHealer(GameObject parent)
    {
        List<GameObject> effects = new List<GameObject>();
        effects.Add(ProduceEffect(parent, new Vector3(0,-0.2f,0), new Vector3(0,0,0), 40, false, Color.green, -5));
        effects[0].GetComponent<Animator>().speed = 1.5f;
        effects[0].transform.localScale += new Vector3(1f, 0f, 0f);
        effects.Add(ProduceEffect(parent, new Vector3(0,+0.5f,0), new Vector3(0,0,0), 50, false, Color.green, 2));
        effects[1].GetComponent<Animator>().speed = 1.5f;
        yield return null;
    } 

    public IEnumerator BuildHeroPunch(GameObject parent, GameObject target)
    {
        yield return new WaitForEndOfFrame();
        List<GameObject> effects = new List<GameObject>();
        GameObject curObj;
        for(int i = 0; i < 5; i++)
        {
            effects.Add(ProduceEffect(parent, new Vector3(0,-0.2f,0), new Vector3(0,0,0), 22, false, Color.white, 2));
            curObj = effects[i];
            LookAtAndMove(curObj, target.transform.position, 0.3f);
            curObj.transform.position += new Vector3(UnityEngine.Random.Range(-0.2f, 0.2f), UnityEngine.Random.Range(-0.2f, 0.2f), 0);
            curObj.transform.localRotation = Quaternion.Euler(new Vector3(0,0,UnityEngine.Random.Range(-180, 180)));
            yield return StartCoroutine(WaitFrames(8));
        }
    }

    public IEnumerator BuildHeroFist(GameObject parent, GameObject target)
    {
        yield return new WaitForEndOfFrame();
        List<GameObject> effects = new List<GameObject>();
        effects.Add(ProduceEffect(parent, new Vector3(0,-0.2f,0), new Vector3(0,0,0), 23, false, Color.yellow, 2));
        LookAtAndMove(effects[0], target.transform.position, 3.5f);
        effects[0].transform.localScale = new Vector3(1f, 1.2f, 1f);
        effects[0].GetComponent<Animator>().speed = 1.6f;
        yield return null;
    }

    public IEnumerator BuildHeroLaser(GameObject parent, GameObject target)
    {
        List<GameObject> effects = new List<GameObject>();

        GameObject.Find("Canvas").transform.Find("AbsoluteWhite").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("AbsoluteWhite").transform.position = parent.transform.position;
        parent.GetComponent<SpriteRenderer>().sortingOrder = 11;

        yield return StartCoroutine(GameManager.StopTimeCoroutine(0.3f));

        GameObject.Find("Canvas").transform.Find("AbsoluteWhite").gameObject.SetActive(false);
        parent.GetComponent<SpriteRenderer>().sortingOrder = 0;

        effects.Add(ProduceEffect(parent, new Vector3(0,0f,0), new Vector3(0,0,0), 64, true, Color.red, 3));
        LookAtAndMove(effects[0], target.transform.position, 12.5f);
        effects[0].transform.localScale = new Vector3(3f, 15f, 1f);
        effects[0].GetComponent<Animator>().speed = 1.6f;

        effects.Add(ProduceEffect(parent, new Vector3(0,0f,0), new Vector3(0,0,0), 65, true, Color.red, 4));
        LookAtAndMove(effects[1], target.transform.position, 1f);
        effects[1].transform.localScale = new Vector3(2f, 0.5f, 1f);
        effects[1].GetComponent<Animator>().speed = 1.6f;

        yield return StartCoroutine(GameObject.Find("Main Camera").GetComponent<CameraManager>().Shake(0.25f, 1.5f));
        yield return StartCoroutine(GameObject.Find("Main Camera").GetComponent<CameraManager>().Shake(0.1f, 0.3f));

        Destroy(effects[0]);
        Destroy(effects[1]);
    }

    public IEnumerator BuildHeroCharge(GameObject parent, GameObject target)
    {
        yield return new WaitForEndOfFrame();
    }

    public IEnumerator BuildHeroTeleport(GameObject parent, GameObject target)
    {
        yield return new WaitForEndOfFrame();
    }

    public IEnumerator BuildFleshDeath(Vector3 pos)
    {
        GameObject effect;
        int rand = Random.Range(0,100);
        if(rand < 50)
            effect = ProduceEffect(pos, new Vector3(0,0,0), 33, false, Color.white, 0);
        else
            effect = ProduceEffect(pos, new Vector3(0,0,0), 34, false, Color.white, 0);
        effect.transform.localRotation = Quaternion.Euler(0f,0f,Random.Range(-180, 180));
        yield return null;
    }

    public IEnumerator BuildSkeletonDeath(Vector3 pos)
    {
        GameObject effect;
        int rand = Random.Range(0,100);
        effect = ProduceEffect(pos, new Vector3(0,0,0), 14, false, Color.white, 0);
        yield return null;
    }

    public IEnumerator BuildFireBallCast(GameObject parent, Vector3 target)
    {
        GameObject effect;
        effect = ProduceEffect(parent, new Vector3(0f,0f,0f), new Vector3(0,0,0), 13, false, Color.red, 1);
        LookAtAndMove(effect, target, 0.75f);
        effect.transform.localRotation = Quaternion.Euler(0f,0f,0f);
        effect.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
        effect.GetComponent<Animator>().speed = 1.8f;
        yield return null;
    }

    public IEnumerator BuildFireBallEffect(GameObject parent)
    {
        yield return new WaitForEndOfFrame();
        GameObject effect = ProduceEffect(parent, new Vector3(0f, -0.0f, 0f), new Vector3(0,0,0), 30, true, Color.red, 2);
        effect.transform.rotation = parent.transform.rotation;
        effect.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
        effect.GetComponent<SpriteRenderer>().flipY = true;
        effect.GetComponent<Animator>().speed = 1.25f;
    }

    public IEnumerator BuildFireBallExplosion(Vector3 pos)
    {
        GameObject effect;
        effect = ProduceEffect(pos, new Vector3(0,0,0), 60, false, Color.white, 0);
        effect.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
        effect.GetComponent<Animator>().speed = 0.75f;
        yield return null;
    }

    public IEnumerator BuildFlameTrail(Vector3 source, Vector3 dest)
    {
        float length = Mathf.Abs(Vector3.Distance(dest, source));
        Vector3 moveVector = (dest - source).normalized;
        List<GameObject> effects = new List<GameObject>();

        for(float i = 0f; i < 20; i++)
        {
            Debug.Log("I'm in iteration");
            float randX, randY;
            randX = Random.Range(-0.15f, 0.15f);
            randY = Random.Range(-0.15f, 0.15f);
            Vector3 pos = source + moveVector*(i/20)*(length) + new Vector3(randX, randY, 0);
            effects.Add(ProduceEffect(pos, Vector3.zero, 61, false, Color.white, -1));
            yield return null;
        }
        yield return null;
    }

    public void Delegate_FireBallCast(GameObject parent, Vector3 target)
    {
        StartCoroutine(BuildFireBallCast(parent, target));
    }

    public void Delegate_FireBallExplosion(Vector3 pos)
    {
        StartCoroutine(BuildFireBallExplosion(pos));
    }

    public IEnumerator BuildCommandCirle(Vector3 pos)
    {
        GameObject effect;
        effect = ProduceEffect(pos, Vector3.zero, 57, false, Color.white, -2);
        effect.GetComponent<Animator>().speed = 1.2f;
        effect.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        effect.transform.localRotation = Quaternion.Euler(new Vector3(0,0,Random.Range(-180,180)));
        yield return null;
    }

    public IEnumerator BuildCommandUnit(GameObject parent)
    {
        GameObject effect;
        effect = ProduceEffect(parent, Vector3.zero, Vector3.zero, 16, false, Color.white, 2);
        effect.transform.localPosition += Vector3.up*(0.5f);
        yield return null;
    }

    public void Delegate_CommandUnit(GameObject parent)
    {
        StartCoroutine(BuildCommandUnit(parent));
    }

    public void Delegate_CommandCircle(Vector3 pos)
    {
        StartCoroutine(BuildCommandCirle(pos));
    }
}
