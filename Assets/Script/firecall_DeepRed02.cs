using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class firecall_DeepRed02 : MonoBehaviour
{
    private Transform m_Transform;
    public ContactPoint2D[] contacts;  // contacts是一個陣列

    // 火球一進場景就在場上
    // 音效則是碰到才生
    public GameObject soundPrefab;

    // 扣血相關
    int reduce_HP_fireball = 2;
    public Text HPText;
    public bool isReduceHPEffect;
    float timer_HPEffect = 0.0f;


    void Start()
    {
        m_Transform = GetComponent<Transform>();
    }

    void OnTriggerEnter2D(Collider2D toucher)
    {
        if (toucher.name == "mainCharacter")
        {
            //接觸log
            Debug.Log("主角碰到火球啦痛痛");
            //音效
            var soundObj = Instantiate(soundPrefab, m_Transform.position, Quaternion.identity);
            //扣血
            GameManager.instance.ReduceHP(reduce_HP_fireball);  // 實例化 GameManager 去呼叫裡面的方法，扣一次1血

            //血量text閃爍
            isReduceHPEffect = true;

            //善後
            Destroy(soundObj,2.0f);
        }
    }

    
    void Update()
    {

        // 血量text閃爍
        Color hideColor = new Color(HPText.color.r, HPText.color.g, HPText.color.b, 0.0f);
        Color showColor = new Color(HPText.color.r, HPText.color.g, HPText.color.b, 1.0f);

        if (isReduceHPEffect && timer_HPEffect < 4)
        {
            timer_HPEffect += Time.deltaTime * 16;

            if (timer_HPEffect % 2 > 1.0f)
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
            timer_HPEffect = 0;
            isReduceHPEffect = false;
        }

    }
}
