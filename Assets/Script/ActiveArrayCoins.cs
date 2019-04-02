using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveArrayCoins : MonoBehaviour
{
    public GameObject[] CoinsArray;  // 先到面板設定長度k，才會出現k個欄位讓你設定內容 =金幣

    void Start()
    {
        Debug.Log("CoinsArray的長度 = " + CoinsArray.Length);
        for (int i=0; i < CoinsArray.Length;i+=1)
        {
            Debug.Log("CoinsArray[" + i + "].name = " + CoinsArray[i].name);
        }
    }

    // 碰到旗子，就把這三個金幣打開，並將Score物件開起來
    void OnTriggerEnter2D(Collider2D toucher)
    {
        if (toucher.name == "mainCharacter")
        {
            for (int i=0; i < CoinsArray.Length; i++)
            {
                CoinsArray[i].SetActive(true);  // 把向量中的金幣物件打開
            }

        }
    }
}
