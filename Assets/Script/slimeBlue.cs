using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeBlue : MonoBehaviour
{
    // 音效物件
    // 進入 collider 的判斷
    private Rigidbody2D m_Rigidbody2D;
    public GameObject soundPrefab;
    public ContactPoint2D[] contacts;  // 一個陣列，用來存放接觸點向量


    void OnTriggerEnter2D(Collider2D toucher) {
        if (toucher.name == "mainCharacter") {
            Debug.Log("誰碰到藍色史萊姆了!? =" + toucher.name);
            var soundObj = Instantiate(soundPrefab, transform.position, Quaternion.identity);
            GameManager.instance.ReduceHP(1);  // 實例化 GameManager 去呼叫裡面的方法，扣一次1血
            Destroy(soundObj, 0.5f);
        }
    }


    void OnCollisionStay2D(Collider2D toucher) {
        if (toucher.name == "mainCharacter") {
            // m_Rigidbody2D.AddForce(toucher.contacts[0].normal );
            // Debug.Log("踩到藍色史萊姆的頭了!");

            // foreach (ContactPoint2D element in toucher.contacts) {
            //     Debug.Log(element.normal);
            //     }
            }
        }
}
