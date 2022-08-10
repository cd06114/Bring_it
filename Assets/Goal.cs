using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(GameMgr.Instance.itemRank != 0)
            {
                GameMgr.Instance.gotItem = GameMgr.Instance.itemRank;
                Debug.Log(GameMgr.Instance.gotItem);
            }

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
