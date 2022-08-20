//using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using UnityEngine.Pool;

public class GameMgr : MonoBehaviour
{
    private ObjectPool<Bullet> _bulletPool;
    private ObjectPool<Rock> _rockPool;

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
    private float checkTime = 20;
    public float timer;
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
        _bulletPool = new ObjectPool<Bullet>(
            createFunc: () =>
            {
                var createdBullet = Instantiate(bulletPrefab);
                createdBullet.poolToReturn = _bulletPool;
                return createdBullet;
            }
                ,
            actionOnGet: (bullet) =>
            {
                bullet.gameObject.SetActive(true);
                bullet.Reset();
            },
            actionOnRelease: (Bullet) =>
            {
                Bullet.gameObject.SetActive(false);
            },
            actionOnDestroy: (bullet) =>
            {
                Destroy(bullet.gameObject);
            }, maxSize: 30
            );
        _rockPool = new ObjectPool<Rock>(
            createFunc: () =>
            {
                var createdBullet = Instantiate(rockPrefab);
                createdBullet.poolToReturn = _rockPool;
                return createdBullet;
            }
                ,
            actionOnGet: (rock) =>
            {
                rock.gameObject.SetActive(true);
                rock.Reset();
            },
            actionOnRelease: (rock) =>
            {
                rock.gameObject.SetActive(false);
            },
            actionOnDestroy: (rock) =>
            {
                Destroy(rock.gameObject);
            }, maxSize: 30
            );
        Init();

    }
    void Init()
    {
        UIMgr.Instance.OnPlay();
        spawnRate = 1f;
        //spwanRateMax = 0.8f;
        player = FindObjectOfType<Player>();
        if (player) { player.Init(); }
        bulletPrefab = Resources.Load<Bullet>("Prefabs/Sphere");
        rockPrefab = Resources.Load<Rock>("Prefabs/PT_Generic_Rock_01_LOD0");
        //Debug.Log(bulletPrefab);

        turrets = GameObject.FindGameObjectsWithTag("turret");
        rockRespawn = GameObject.FindGameObjectWithTag("rockRespawn");
        // 오브젝트 풀링.
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
            //var rock = Instantiate(rockPrefab);
            var rock = _rockPool.Get();
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
            //var bullet = Instantiate(bulletPrefab);
            var bullet = _bulletPool.Get();
            if (bullet && player)
            {
                bullet.EventHadleOnCollisionPlayer += player.OnDamaged;
                bullet.EventHadleOnCollisionPlayer += () => { UIMgr.Instance.GameOver(itemRank); };
            }
            if (bullet) listBullet.Add(bullet);
            return bullet;
        }
        return null;
    }


    void SpawnBullet()
    {
        if (0 >= turrets.Length) return;
        // 사용되고 있지 않은(비활성화 상태) 탄환을 찾는다.
        var bullet = listBullet.Find(b => !b.gameObject.activeSelf);
        // 사용되지 않는 탄환이 없다면 추가로 만든다.
        if (!bullet) bullet = MakeBullet();
        if (bullet)
        {
            // 탄환 발사.
            var pos_index = UnityEngine.Random.Range(0, turrets.Length);
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
        // 사용되고 있지 않은(비활성화 상태) 탄환을 찾는다.
        var rock = listRock.Find(b => !b.gameObject.activeSelf);
        // 사용되지 않는 탄환이 없다면 추가로 만든다.
        if (!rock) rock = MakeRock();
        if (rock)
        {
            // 탄환 발사.
            var pos = rockRespawn.transform.position + Vector3.up * 1.5f;
            rock.SetPosition(pos);
            var dir = (player.position - pos).normalized;
            dir.y = 0.2f;
            var force = Random.Range(6, 10);
            rock.OnFire(dir, force * 100);

        }
    }
    public event Action EventHadleOnCollisionPlayer;

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
            if(timer < 0)
            {
                if (null != EventHadleOnCollisionPlayer) EventHadleOnCollisionPlayer();
                gameObject.SetActive(false);
                UIMgr.Instance.GameOver(itemRank);
            }
            //else if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene("Dodge");

        }

    }


}
