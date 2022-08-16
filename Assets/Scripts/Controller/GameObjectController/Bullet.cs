using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Bullet : MonoBehaviour
{
    private Rigidbody rigid;

    private void Awake()
    {
        // 오브젝트 풀링(pooling)을 사용할 것이므로 생성 후 비활성화.
        if (!rigid) rigid = GetComponent<Rigidbody>();
        gameObject.SetActive(false);

    }

    public event Action EventHadleOnCollisionPlayer;
    private void OnTriggerEnter(Collider other)
    {
        var tag = other.tag;
        if (tag.Equals("Player"))//"Player" == other.tag
        {
            if (null != EventHadleOnCollisionPlayer) EventHadleOnCollisionPlayer();
            UIMgr.Instance.GameOver(GameMgr.Instance.itemRank);
        }
        else if (tag.Equals("Respawn") || other.name.Equals(name)) { gameObject.SetActive(false); return; }
        gameObject.SetActive(false);
    }

    
    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }
    public void OnFire(Vector3 dir, float force)
    {
        gameObject.SetActive(true);
        rigid.velocity = Vector3.zero;
        rigid.AddForce(dir.normalized * force);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
