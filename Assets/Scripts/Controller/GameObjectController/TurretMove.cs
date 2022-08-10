using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMove : MonoBehaviour
{
    public Rigidbody turret;
    float v1_x,v1_y;
    float v2_x,v2_y;
    private float timer = 0;
    //private float checkTime = 2;
    // Start is called before the first frame update
    void Start()
    {
        v1_x = Random.Range(-10, 10); v1_y = Random.Range(-10, 10);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>2)
        {
            timer = 0; v1_y = Random.Range(-10, 10);
        }
        if (turret.position.x>25)
        {
            v1_x = Random.Range(-10, 0);
        }
        if (turret.position.x < -25)
        {
            v1_x = Random.Range(0, 10);
        }


        if(turret.position.z<-230)
        {
            v1_y = 10;
        }
        if (turret.position.z > -180)
        {
            v1_y = -10;
        }
        turret.velocity = new Vector3(v1_x, 0,v1_y);
    }
}
