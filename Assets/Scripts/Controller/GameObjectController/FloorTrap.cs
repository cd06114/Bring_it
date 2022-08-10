using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
public class FloorTrap : MonoBehaviour
{
    float movingTime = 1f;
    public float rotSpeed = 0f;
    public GameObject GameObject;
    bool isMoved= false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GameObject.transform.rotation.z);
        //transform.Rotate(new Vector3(0, 0, rotSpeed * Time.deltaTime));

        if (isMoved == false)
        {
            if (GameObject.transform.rotation.z <= -0.4)
            {
                isMoved = true;
            }
                transform.Rotate(new Vector3(0, 0, -2*rotSpeed * Time.deltaTime));

                movingTime -= Time.deltaTime;
            
        }
        else
        {
            if (GameObject.transform.rotation.z >= 0)
            {
                isMoved = false;

            }
                transform.Rotate(new Vector3(0, 0, 0.2f * rotSpeed * Time.deltaTime));
                movingTime -= Time.deltaTime;
        }
    }
}
