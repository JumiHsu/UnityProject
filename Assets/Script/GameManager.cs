using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // public static int gameScore = 0; // 本來是透過 static 去取 gameScore
    public int gameScore = 0;
    public int gameHP = 10;
    public GameObject winUI;
    public GameObject loseUI;
    public Text coinText;
    public Text HPText;
    private float timer = 0.0f;

    void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

    public void GetScore(int value) {
        gameScore += value;
        coinText.text = gameScore.ToString();  // 把一個變數轉成字串type，扔進去
        // if (gameScore >= 103) {
        //     winUI.SetActive(true);
        // }
    }

    public void ReduceHP(int value) {
        gameHP -= value;
        HPText.text = gameHP.ToString();
        // if (gameHP <= 0) {
        //     HPText.text = "0";
        //     loseUI.SetActive(true);
        // }
    }

    void Update() {
        if (gameScore >= 103)
        {
            winUI.SetActive(true);
        }
        if (gameHP <= 0)
        {
            HPText.text = "0";
            loseUI.SetActive(true);
        }

    }
}
