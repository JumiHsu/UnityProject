using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerCoin : MonoBehaviour
{
    public GameObject soundObject;  // 這是一個虛擬物件。稍後要拿來放產生出來的音效prefab，public type varName;
    public Text ScoreText;
    private bool isGetScoreEffect = false;
    private float timer = 0.0f;
    // public GameManager _GameManager;  //1 沒有 static 的話


    void OnTriggerEnter2D(Collider2D toucher)  // 參數：一個Collider2D類別 的變數， 變數名稱=toucher
        {
            Debug.Log(toucher.name);  // 他會打印出 "進入trigger區域的那個物件" 的名稱(沒有返回東西)

            if (toucher.name == "mainCharacter")
            {
                // 將音效生在金幣所在處：transform.position = GetComponent<Transform>().position
                var soundObj = Instantiate(soundObject, transform.position, Quaternion.identity);  //只能用在class內，外人看不到
                
                // _GameManager.gameScore += 1;  //1 沒有 static 的話
                // GameManager.gameScore += 1;  //2 可以直接改用 類別名稱.變數 去呼叫並指派
                GameManager.instance.GetScore(1);  //3 // 現在有了 instance ，就可以改用 instance 去取，一個金幣1分

                Destroy(gameObject);  // gameObject=金幣
                Destroy(soundObj, 0.5f);  // 音效prefab，注意要等音效播完才銷毀

            }
        }


    void Update()
    {
        Color hideColor = new Color(ScoreText.color.r, ScoreText.color.g, ScoreText.color.b, 0.0f);
        Color showColor = new Color(ScoreText.color.r, ScoreText.color.g, ScoreText.color.b, 1.0f);

        if (isGetScoreEffect && timer < 4)
        {
            timer += Time.deltaTime * 8;

            if (timer % 2 > 1.0f)
            {
                ScoreText.color = hideColor;
            }
            else
            {
                ScoreText.color = showColor;
            }
        }
        else
        {
            timer = 0;
            isGetScoreEffect = false;
        }
    }
}
