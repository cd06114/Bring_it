using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform PlayerTransform;
    Vector3 Offset;
    public GameObject GameObject;
    public GameObject cam1;
    public GameObject cam2;
    public bool isLookFront = true;
    private void Awake()
    {
        PlayerTransform = GameObject.transform;
        Offset = transform.position - PlayerTransform.position;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate() //Updat에서 다하고 따라 붙는거라서 lateupdate
    {
    }

    void Update()
    {
        Vector3 cam1Pos = cam1.transform.position;
        Vector3 cam2Pos = cam2.transform.position;
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            transform.position = cam2Pos;
            cam2.transform.position = cam1Pos;
            cam1.transform.Rotate(0, 180, 0);
            if (isLookFront)
            {

            }
        }
        else
        {
            transform.position = PlayerTransform.position + Offset;
        }
    }
}
