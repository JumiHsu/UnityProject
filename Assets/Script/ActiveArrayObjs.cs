using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveArrayObjs : MonoBehaviour
{
    public GameObject[] CoinsArray;  // 先到面板設定長度，才會出現設定內容的欄位
    void Start()
    {
        Debug.Log("CoinsArray.Length = " + CoinsArray.Length);
        for (int i=0; i < CoinsArray.Length;i++)
        {
            Debug.Log("CoinsArray[" + i + "].name = " + CoinsArray[i].name);
            // Debug.Log("CoinsArray[" + i + "] = " + CoinsArray[i]);  //這樣寫不知道是什麼
        }
        
    }

    void OnTriggerEnter2D(Collider2D toucher)
    {
        if (toucher.name == "mainCharacter")
        {
            // for ()
            // {

            // }
            Destroy(gameObject);
        }
    }
}
