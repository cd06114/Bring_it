using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TimeItem : MonoBehaviour
{
    
    // Start is called before the first frame update
    float rotSpeed = 100f;
    //public int grade;
    GameMgr GameMgr;
    public static TimeItem Instance { get; private set; }
    private void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        
    }
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        var tag = other.tag;
        if (tag.Equals("Player"))//"Player" == other.tag
        {
            if (gameObject.transform.position.z < -150)
            {
                GameMgr.Instance.timer += 3;
                UIMgr.Instance.alarmText.text = "+3 Seconds";
            }
            else if (gameObject.transform.position.z < 0)
            {
                GameMgr.Instance.timer += 5;
                UIMgr.Instance.alarmText.text = "+5 Seconds";
            }
            else if (gameObject.transform.position.z > 100)
            {
                GameMgr.Instance.timer += 10;
                UIMgr.Instance.alarmText.text = "+10 Seconds";
            }
            else
            {
                GameMgr.Instance.timer += 20;
                UIMgr.Instance.alarmText.text = "+20 Seconds";
            }
            gameObject.SetActive(false);
        }

    }

    void Update()
    {
        transform.Rotate(new Vector3(0,  rotSpeed * Time.deltaTime,0));
    }
}
