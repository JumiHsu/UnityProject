using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slimeBlue : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody2D;
    private Material m_Material;
    private Renderer m_Renderer;
    public GameObject soundPrefab;
    public ContactPoint2D[] contacts;  // 一個陣列，用來存放接觸點向量
    public Text HPText;

    public bool isReduceHPEffect;
    public float timer = 0.0f;


    void OnTriggerEnter2D(Collider2D toucher) {
        if (toucher.name == "mainCharacter") {

            // 接觸
            Debug.Log("誰碰到藍色史萊姆了!? =" + toucher.name);

            // 扣血
            var soundObj = Instantiate(soundPrefab, transform.position, Quaternion.identity);
            GameManager.instance.ReduceHP(1);  // 實例化 GameManager 去呼叫裡面的方法，扣一次1血
            // HPText.text = "0";  // 直接歸0，可是這樣你沒有改到真正的score，只是粗暴的直接改UItext

            // 血量text會閃一下
            isReduceHPEffect = true;

            Destroy(soundObj, 0.5f);
        }
    }


    void Update()
        {
            Color hideColor = new Color(HPText.color.r, HPText.color.g, HPText.color.b, 0.0f);
            Color showColor = new Color(HPText.color.r, HPText.color.g, HPText.color.b, 1.0f);

            if (isReduceHPEffect && timer < 4)
            {
                timer += Time.deltaTime * 8;

                if (timer % 2 > 1.0f)
                {
                    HPText.color = hideColor;
                }
                else
                {
                    HPText.color = showColor;
                }
            }
            else
            {
                timer = 0;
                isReduceHPEffect = false;
            }
        }
    
}
