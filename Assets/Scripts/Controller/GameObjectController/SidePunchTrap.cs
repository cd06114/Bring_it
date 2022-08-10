using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidePunchTrap : MonoBehaviour
{
    Vector3 pos; //처음 시작 위치
    public float movingTime = 2f; // 움직이는 시간
    public Vector3 target; //목표 위치
    public Vector3 velo = new Vector3(100f,0,0);
    bool isMoved = false;

    //Vector3 velo = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoved == false)
        {
            if (movingTime < 0)
            {
                movingTime = 5f;
                isMoved = true;
            }
            else
            {
                transform.position = Vector3.SmoothDamp(transform.position, target, ref velo, 0.1f);
                movingTime -= Time.deltaTime;
            }
        }
        else
        {
            if (movingTime < 0)
            {
                movingTime = 1f;
                isMoved = false;
            }
            else
            {
                transform.position = Vector3.SmoothDamp(transform.position, pos, ref velo, 1f);
                movingTime -= Time.deltaTime;
            }
        }
    }
    
    void moveTarget(Vector3 pos, Vector3 target, ref Vector3 velo, float speed)
    {

    }
}
