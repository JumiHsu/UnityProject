using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCoin : MonoBehaviour
{
    public GameObject soundObject;  // 這是一個虛擬物件。稍後要拿來放產生出來的音效prefab，public type varName;

    void OnTriggerEnter2D(Collider2D toucher)  // 參數：一個Collider2D類別 的變數， 變數名稱=toucher
    {
        Debug.Log(toucher.name);  // 他會打印出 "進入trigger區域的那個物件" 的名稱(沒有返回東西)

        if(toucher.name == "mainCharacter")
        {
            // 生在金幣所在處：transform.position = GetComponent<Transform>().position
            var soundObj = Instantiate(soundObject, transform.position, Quaternion.identity);  //只能用在class內，外人看不到
            Destroy(gameObject);  // gameObject=旗子
            Destroy(soundObj,0.5f);  // 音效prefab，注意要等音效播完才銷毀
            
            double[] test01 = { 0, 1, 22, 333 };
            Debug.Log("test01是" + test01);  // System.Double[]
        }
    }
}
