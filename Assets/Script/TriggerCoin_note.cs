using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCoin_note : MonoBehaviour
{
    public GameObject soundObject;  // 這是一個虛擬物件。稍後要拿來放產生出來的音效prefab，public type varName;

    // func：OnTriggerEnter2D，參數：Collider2D類別，變數名稱toucher，void：不回傳東西的func
    void OnTriggerEnter2D(Collider2D toucher)
    {
        Debug.Log(toucher.name);  // 他他會打印出 "進入trigger區域的那個物件" 的名稱(沒有返回東西)

        if(toucher.name == "mainCharacter")
        {
            // 產生指定的prefab，參數要嘛只用1個，要嘛3個都用
            // Instantiate(soundObject, new Vector3(0, 0, 0),Quaternion.identity);  //生在指定位置
            // Instantiate(soundObject, GetComponent<Transform>().position, Quaternion.identity);  //生在金幣所在處
            var soundObj = Instantiate(soundObject, transform.position, Quaternion.identity);  //只能用在class內，外人看不到
            Destroy(gameObject);  // gameObject=金幣
            Destroy(soundObj);  // 音效prefab

            // 雖然可以work但不太好的寫法
            // Instantiate(soundObject, transform.position, Quaternion.identity);
            // var target = GameObject.Find("handleCoins(Clone)");
            // Destroy(target,1.5f);

            //刪掉角色自己了!!!
            // Destroy(toucher.gameObject);
        }
    }
}
