using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveArrayObjs : MonoBehaviour
{
    public GameObject[] CoinsArray;  // 先到面板設定長度，才會出現設定內容的欄位
    public GameObject Score;

    void Start()
    {
        Debug.Log("CoinsArray的長度 = " + CoinsArray.Length);
        for (int i=0; i < CoinsArray.Length;i+=1)
        {
            Debug.Log("CoinsArray[" + i + "].name = " + CoinsArray[i].name);
            // Debug.Log("CoinsArray[" + i + "] = " + CoinsArray[i]);  //這樣寫不知道是什麼
        }
        
    }

    // 碰到旗子，就把這三個金幣打開
    void OnTriggerEnter2D(Collider2D toucher)
    {
        if (toucher.name == "mainCharacter")
        {
            for (int i=0; i < CoinsArray.Length; i++)
            {
                CoinsArray[i].SetActive(true);  // 米飯：把特定的物件打開(??)
            }
            
            var Objscore = Instantiate(Score, transform.position, Quaternion.identity);
            Objscore.SetActive(true);
            Destroy(Objscore,1.5f);
            
            // 這個寫法不行，不知道為什麼
            //Instantiate(Score, transform.position, Quaternion.identity);
            //var target = GameObject.Find("Score(Clone)");
            //target.SetActive(true);
            // Destroy(target,1.5f);

            Destroy(gameObject);  // gameObject=旗子
        }
    }
}
