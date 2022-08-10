using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    public int rank;

    public static Item Instance { get; private set; }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(GameMgr.Instance.itemRank <= rank)
            {
                UIMgr.Instance.alarmText.text = string.Format("Got {0} rank item", rank);
                Player.Instance.rigid.mass += (rank - GameMgr.Instance.itemRank) * 2;
                GameMgr.Instance.itemRank = rank;
            }
            gameObject.SetActive(false);
        }
    }

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
