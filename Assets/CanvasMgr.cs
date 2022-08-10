using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMgr : MonoBehaviour
{
    public GameObject player;
    public GameObject gameObject;
    Vector3 dist;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 canvasPos = gameObject.transform.position;
        dist = playerPos - canvasPos;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        gameObject.transform.position = playerPos - dist;
    }
}
