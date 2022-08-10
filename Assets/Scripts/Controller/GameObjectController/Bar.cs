using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Bar : MonoBehaviour
{
    // Start is called before the first frame update
    float rotSpeed = 100f;

    
    void Start()
    {

    }

    // Update is called once per frame
    //public event Action EventHadleOnCollisionPlayer;
    //private void OnTriggerEnter(Collider other)
    //{
    //    var tag = other.tag;
    //    if (tag.Equals("Player"))//"Player" == other.tag
    //    {
    //        if (null != EventHadleOnCollisionPlayer) EventHadleOnCollisionPlayer();
    //        Destroy(other.gameObject);
    //    }
    //    else if (tag.Equals("Respawn") || other.name.Equals(name)) { gameObject.SetActive(false); return; }
    //    gameObject.SetActive(false);
    //}

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotSpeed * Time.deltaTime));
    }
}
