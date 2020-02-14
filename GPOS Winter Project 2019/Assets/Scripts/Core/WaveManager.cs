using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class WaveManager : MonoBehaviour
{
    private int wavenum;
    private UnitFactoryManager factorymanager;
    private Coroutine WaveCoroutine;
    private bool WaveWaiting;
    private Player player;
    private WaveList wavelist;

    public int getWave
    {
        get { return wavenum; }
    }

    [System.Serializable]
    public class Squad
    {
        public int Portal_Number;
        public string unitName;
        public int unitNumber;
    }

    [System.Serializable]
    public class subWave
    {
        public List<Squad> squads;
        public void summon()
        {
            foreach (Squad squad in squads)
            {
                GameObject.Find("UnitFactory").GetComponent<UnitFactoryManager>().PlaceUnit("Building", GameObject.Find("MapManager").GetComponent<MapManager>().GetPortalPos()[squad.Portal_Number], squad.unitName, squad.unitNumber, 0.1f);
            }
        }
    }

    [System.Serializable]
    public class Wave
    {
        public List<subWave> subWaves;
    }

    [System.Serializable]
    public class WaveList
    {
        public Wave[] Waves;
    }

    // Start is called before the first frame update
    void Reset()
    {
        wavenum = 0;
    }

    void Awake()
    {
        factorymanager = GameObject.Find("UnitFactory").GetComponent<UnitFactoryManager>();
        player = GameObject.Find("Player").GetComponent<Player>();
        wavelist = new WaveList();
        FileStream fileStream = new FileStream(string.Format("{0}/{1}/{2}.json", Application.dataPath, "JSON", "Waves"), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        wavelist = JsonUtility.FromJson<WaveList>(jsonData);
    }

    private void Start()
    {
        if (factorymanager.isFactoryLoaded)
        {
            //factorymanager.PlaceUnit("LichKing", new Vector2(2,4));
            //factorymanager.PlaceUnit("Ghost", new Vector2(0,4));
            //factorymanager.PlaceUnit("Ghost", new Vector2(4, 6));
        }
        StartCoroutine(RunWave());
    }

    private void Update()
    {
    }

    IEnumerator RunWave()
    {
        for (wavenum = 0; wavenum < wavelist.Waves.Length; wavenum++)
        {
            int payback = 0;
            GameObject[] Friends = GameObject.FindGameObjectsWithTag("Friendly");
            for (int j = 0; j < Friends.Length; j++)
            {
                if (Friends[j].GetComponent<NPC>() != null)
                {
                    payback += Friends[j].GetComponent<NPC>().Notch;
                    Friends[j].GetComponent<NPC>().Die();
                }
            }
            player.addExp(payback / 4);
            player.Heal(player.MaxHealth);

            //다음 웨이브 시작까지 남은시간 카운트(기획 명세 필요)
            yield return new WaitForSeconds(20f);

            for (int j = 0; j < wavelist.Waves[wavenum].subWaves.Count; j++)
            {
                yield return new WaitForSeconds(10f);
                wavelist.Waves[wavenum].subWaves[j].summon();
            }
            yield return new WaitUntil(isClear);
        }
    }

    protected bool isClear()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && GameObject.FindGameObjectsWithTag("Building").Length == 0;
    }
}
