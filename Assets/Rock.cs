using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Pool;

public class Rock : MonoBehaviour
{
    private Rigidbody rigid;

    public bool IsTriggered { get; private set; }
    public IObjectPool<Rock> poolToReturn;
    public void Reset()
    {
        IsTriggered = false;
    }
    private void Awake()
    {
        // 오브젝트 풀링(pooling)을 사용할 것이므로 생성 후 비활성화.
        if (!rigid) rigid = GetComponent<Rigidbody>();
        gameObject.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        var tag = other.tag;
        //if (tag.Equals("Player"))//"Player" == other.tag
        //{
        //    if (null != EventHadleOnCollisionPlayer) EventHadleOnCollisionPlayer();
        //    UIMgr.Instance.GameOver(GameMgr.Instance.itemRank);
        //}
        if (IsTriggered)
        {
            return;
        }
        if (tag.Equals("Respawn") || other.name.Equals(name))
        {

            IsTriggered = true;
            StartCoroutine(DestroyRock());
        }
        gameObject.SetActive(false);
    }

    //public event Action EventHadleOnCollisionPlayer;

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

    private IEnumerator DestroyRock()
    {
        yield return new WaitForSeconds(3f);
        poolToReturn.Release(this);
    }
}
