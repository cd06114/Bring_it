//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour
{
    public static GameMgr Instance { get; private set; }
    public int point = 0;
    private Player player;
    GameObject[] turrets;
    GameObject rockRespawn;
    private Bullet bulletPrefab;
    private Rock rockPrefab;
    private List<Bullet> listBullet;
    private List<Rock> listRock;
    [SerializeField] private float spwanRateMin;
    [SerializeField] private float spwanRateMax;
    [SerializeField] private float rockSpwanRateMin;
    [SerializeField] private float rockSpwanRateMax;
    public int itemRank = 0;
    public int gotItem = 0;

    private float spawnRate = 1f;
    private float rockSpawnRate = 1f;
    private float checkTime = 10;
    public float timer = 10f;
    private void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += (scene, mode) => { Init(); };
            return;
        }
        Destroy(gameObject);
    }

    void Start()
    {
        Init();
    }
    void Init()
    {
        UIMgr.Instance.OnPlay();
        timer = 10;
        spawnRate = 1f;
        //spwanRateMax = 0.8f;
        player = FindObjectOfType<Player>();
        if (player) { player.Init(); }
        bulletPrefab = Resources.Load<Bullet>("Prefabs/Sphere");
        rockPrefab = Resources.Load<Rock>("Prefabs/PT_Generic_Rock_01_LOD0");
        //Debug.Log(bulletPrefab);

        turrets = GameObject.FindGameObjectsWithTag("turret");
        rockRespawn = GameObject.FindGameObjectWithTag("rockRespawn");
        // ������Ʈ Ǯ��.
        listBullet = new List<Bullet>();
        for (int i = 0; turrets.Length > i; i++)
        {
            var bullet = MakeBullet();
        }
        listRock = new List<Rock>();
        var rock = MakeRock();

    }

    private Rock MakeRock()
    {
        if (rockPrefab)
        {
            //Debug.Log("----------------");
            var rock = Instantiate(rockPrefab);
            //if (rock && player)
            //{
            //    rock.EventHadleOnCollisionPlayer += player.OnDamaged;
            //    rock.EventHadleOnCollisionPlayer += () => { UIMgr.Instance.GameOver(timer); };
            //}
            if (rock) listRock.Add(rock);
            return rock;
        }
        return null;
    }

    Bullet MakeBullet()
    {
        if (bulletPrefab)
        {
            //Debug.Log("----------------");
            var bullet = Instantiate(bulletPrefab);
            if (bullet && player)
            {
                bullet.EventHadleOnCollisionPlayer += player.OnDamaged;
                bullet.EventHadleOnCollisionPlayer += () => { UIMgr.Instance.GameOver(timer); };
            }
            if (bullet) listBullet.Add(bullet);
            return bullet;
        }
        return null;
    }


    void SpawnBullet()
    {
        if (0 >= turrets.Length) return;
        // ���ǰ� ���� ����(��Ȱ��ȭ ����) źȯ�� ã�´�.
        var bullet = listBullet.Find(b => !b.gameObject.activeSelf);
        // ������ �ʴ� źȯ�� ���ٸ� �߰��� �����.
        if (!bullet) bullet = MakeBullet();
        if (bullet)
        {
            // źȯ �߻�.
            var pos_index = Random.Range(0, turrets.Length);
            var pos = turrets[pos_index].transform.position + Vector3.up * 1.5f;
            bullet.SetPosition(pos);
            var dir = (player.position - pos).normalized;
            dir.y = 0.2f;
            var force = Random.Range(6, 10);
            bullet.OnFire(dir, force * 100);

        }
    }

    void SpawnRock()
    {
        // ���ǰ� ���� ����(��Ȱ��ȭ ����) źȯ�� ã�´�.
        var rock = listRock.Find(b => !b.gameObject.activeSelf);
        // ������ �ʴ� źȯ�� ���ٸ� �߰��� �����.
        if (!rock) rock = MakeRock();
        if (rock)
        {
            // źȯ �߻�.
            var pos = rockRespawn.transform.position + Vector3.up * 1.5f;
            rock.SetPosition(pos);
            var dir = (player.position - pos).normalized;
            dir.y = 0.2f;
            var force = Random.Range(6, 10);
            rock.OnFire(dir, force * 100);

        }
    }

    void Update()
    {
        if (player)
        {
            if (player.isLive)
            {
                checkTime += Time.deltaTime;
                if (spawnRate <= checkTime)
                {
                    checkTime = 0;
                    spawnRate = Random.Range(spwanRateMin, spwanRateMax);
                    rockSpawnRate = Random.Range(rockSpwanRateMin, rockSpwanRateMax);
                    SpawnBullet();
                    SpawnRock();
                }
                timer -= Time.deltaTime;
                UIMgr.Instance.Timer = timer;
            }
            //else if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene("Dodge");

        }
    }


}