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

    void Awake() {
        if(instance == null) {
            instance = this;
        }
    }
    
    // void GetCoin() {
    //     GameManager.instance.gameScore += 1;
    // }

    public void GetScore(int value) {
        gameScore += value;
        coinText.text = gameScore.ToString();
        if (gameScore >= 3) {
            winUI.SetActive(true);
        }
    }

    public void ReduceHP(int value) {
        gameHP -= value;
        HPText.text = gameHP.ToString();
        if (gameHP == 0) {
            loseUI.SetActive(true);
        }
    }

}
