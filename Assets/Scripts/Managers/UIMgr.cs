using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIMgr : MonoBehaviour
{
    public static UIMgr Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] public TextMeshProUGUI alarmText;
    private GameObject gameOverPanel;
    public float Timer
    {
        set { if (timer /*or null != timer*/) timer.text = string.Format("{0:N2}", value); }
    }
    private void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            if (gameOverText) gameOverPanel = gameOverText.transform.parent.gameObject;
            return;
        }
        Destroy(gameObject);
    }

    public void GameOver(float time)
    {
        if (gameOverText && gameOverPanel)
        {
            var bestTime = PlayerPrefs.GetFloat("BestTime", 0);
            bestTime = Mathf.Max(bestTime, time);
            gameOverText.text = string.Format("<b>Press <color=red>R</color> to Restart</b>\n<i>Best Time : {0:N2}</i>", bestTime);
            PlayerPrefs.SetFloat("BestTime", bestTime);
            gameOverPanel.SetActive(true);
        }
    }

    public void ItemGet()
    {

    }
    public void OnPlay()
    {
        if (gameOverPanel) gameOverPanel.SetActive(false);
    }
}