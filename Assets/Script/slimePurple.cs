using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class slimePurple : MonoBehaviour
{
    private Transform m_Transform;
    public ContactPoint2D[] contacts;  // contacts是一個陣列

    int reduceHP_purple = 3;
    public Text HPText;
    public bool isReduceHPEffect;
    float timer_HPEffect = 0.0f;

    public GameObject soundPrefab;

    // 巡邏總控
    string patrol_method = "";
    float slimePurple_moveSpeed = 0.8f;

    // 巡邏1
    float patrolCount_i =0.0f;
    // 巡邏2
    public GameObject slimePurple_StartPoint;
    public GameObject slimePurple_EndPoint;
    bool isLRCheck = false;
    public bool moveL = false;
    public bool moveR = false;

    // 純屬筆記
    float timer_f;
    int timer_i;



    void Start() 
    {
        m_Transform = GetComponent<Transform>();
        // 巡邏方式開關
        // patrol_method = "fixMoveTime";
        patrol_method = "fix_Start_End_Pos";

        // 巡邏2：起始兩點位置生成
        if (patrol_method == "fix_Start_End_Pos")
        {
            slimePurple_StartPoint = Instantiate(slimePurple_StartPoint, slimePurple_StartPoint.transform.position, Quaternion.identity);
            slimePurple_EndPoint = Instantiate(slimePurple_EndPoint, slimePurple_EndPoint.transform.position, Quaternion.identity);
            if(slimePurple_StartPoint.transform.position.x < slimePurple_EndPoint.transform.position.x)
            {
                isLRCheck = true;
            }
            else
            {
                isLRCheck = false;
            }
            Debug.Log("起點(左方點)位置" + slimePurple_StartPoint.transform.position.x);
            Debug.Log("起點(右方點)位置" + slimePurple_EndPoint.transform.position.x);
            Debug.Log("L是不是在R左邊" + isLRCheck);

            //1 判斷初始位置
            // if 位置>S 打開moveL
            // if 位置<=S，打開moveR
            //2 移動邏輯
            // if moveL=T && 位置>S ,Left()
            // if 位置=S ,moveR打開，moveL關掉，Right()
            // if moveR=T && 位置<E ,Right()
            // if 位置=E ,moveL打開，moveR關掉，Left()
            if (m_Transform.position.x > slimePurple_StartPoint.transform.position.x)
            {
                moveL=true;
            }
            else
            {
                moveR = true;
            }

        }
    }


    void OnTriggerEnter2D(Collider2D toucher)
    {
        if (toucher.name == "mainCharacter") {
            // 接觸log
            // Debug.Log("誰碰到紫色史萊姆了!? =" + toucher.name);

            // 音效
            var soundObj = Instantiate(soundPrefab, transform.position, Quaternion.identity);

            // 扣血
            GameManager.instance.ReduceHP(reduceHP_purple);  // 實例化 GameManager 去呼叫裡面的方法，扣一次1血

            // 血量text閃爍開關
            isReduceHPEffect = true;

            // 善後
            Destroy(soundObj, 0.5f);
        }
    }


    void slimePurple_moveLeft(float slimePurple_moveSpeed)
    {
        m_Transform.position += new Vector3(-slimePurple_moveSpeed, 0, 0) * Time.deltaTime;
    }

    void slimePurple_moveRight(float slimePurple_moveSpeed)
    {
        m_Transform.position += new Vector3(slimePurple_moveSpeed, 0, 0) * Time.deltaTime;
    }


    void Update()
    {
        // 純屬筆記
        timer_f = Time.time;  // deltaTime就是每一幀Time.time的差值
        timer_i = Mathf.FloorToInt(timer_f);

        // 巡邏1：鎖定移動時間
        var moveTimeLeft = 3;
        var moveTimeRight = 6;
        if (patrol_method == "fixMoveTime")
        {
            patrolCount_i += Time.deltaTime;

            if (patrolCount_i < moveTimeLeft)
            {
                slimePurple_moveLeft(slimePurple_moveSpeed);
            }
            else if ( patrolCount_i >= moveTimeLeft && patrolCount_i < moveTimeRight )
            {
                slimePurple_moveRight(slimePurple_moveSpeed);
            }
            else
            {
                patrolCount_i = 0.0f;
                Debug.Log("patrolCount_i = 0 了!!");
            }
        }


        // 巡邏2：鎖定起始兩點位置
        if (patrol_method == "fix_Start_End_Pos" && isLRCheck)
        {
            // Debug.Log("紫色史萊姆位置：" + m_Transform.position.x);

            if(moveL == true && m_Transform.position.x > slimePurple_StartPoint.transform.position.x)
            {
                slimePurple_moveLeft(slimePurple_moveSpeed);
            }
            else if (m_Transform.position.x <= slimePurple_StartPoint.transform.position.x)
            {
                moveR = true;
                moveL = false;
                slimePurple_moveRight(slimePurple_moveSpeed);
            }
            else if (moveR == true && m_Transform.position.x < slimePurple_EndPoint.transform.position.x)
            {
                slimePurple_moveRight(slimePurple_moveSpeed);
            }
            else if  (m_Transform.position.x >= slimePurple_EndPoint.transform.position.x)
            {
                moveL = true;
                moveR = false;
                slimePurple_moveLeft(slimePurple_moveSpeed);
            }
        }



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
