using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slimePurple : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody2D;
    public GameObject soundPrefab;
    public Text HPText;

    public bool isReduceHPEffect;
    public float timer = 0.0f;
    private int reduceHP_purple = 3;

    public ContactPoint2D[] contacts;  // contacts是一個陣列


    void OnTriggerEnter2D(Collider2D toucher) {
        if (toucher.name == "mainCharacter") {

            // 接觸
            Debug.Log("誰碰到紫色史萊姆了!? =" + toucher.name);

            // 扣血
            var soundObj = Instantiate(soundPrefab, transform.position, Quaternion.identity);
            GameManager.instance.ReduceHP(reduceHP_purple);  // 實例化 GameManager 去呼叫裡面的方法，扣一次1血


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
                timer += Time.deltaTime * 16;

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
