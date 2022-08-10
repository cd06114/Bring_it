using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Rock : MonoBehaviour
{
    private Rigidbody rigid;

    private void Awake()
    {
        // 오브젝트 풀링(pooling)을 사용할 것이므로 생성 후 비활성화.
        if (!rigid) rigid = GetComponent<Rigidbody>();
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
}
