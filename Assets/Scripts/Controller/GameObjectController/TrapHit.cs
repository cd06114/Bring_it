using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapHit : MonoBehaviour
{
    GameObject GameObject;
    Rigidbody rigid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            //Vector3 relVelocity = other.GetComponent<Rigidbody>().velocity - rigid.velocity;
            other.GetComponent<Rigidbody>().AddExplosionForce(300, transform.position, 300,10);
            Debug.Log("ontrigger");
            //rigid.AddExplosionForce(100, transform.position, 100);
        }
    }
}
