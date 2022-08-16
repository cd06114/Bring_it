using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMove : MonoBehaviour
{
    public Rigidbody turret;
    float v1_x,v1_y;
    float v2_x,v2_y;
    private float timer = 0;
    public Rigidbody rigid;
    public float Speed = 2.0f;
    public float rotateSpeed = 2.0f;
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
        float x = rigid.velocity.x;
        float z = rigid.velocity.z;
        turret.velocity = new Vector3(v1_x, 0,v1_y);
        Vector3 dir = new Vector3(x, 0, z);
        // 바라보는 방향으로 회전 후 다시 정면을 바라보는 현상을 막기 위해 설정
        if (!(x == 0 && z == 0))
        {
            // 회전하는 부분. Point 1.
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotateSpeed);
        }
    }
}
