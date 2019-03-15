using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class mainCharacter_all_01 : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody2D;  // 宣告一個 私有+"Rigidbody2D"類型 的變數
    private Animator m_Animator;  // 為了要取得 animator 的 component，所以先做一個 m_***
    private SpriteRenderer m_SpriteRenderer;
    private Transform m_transform;
    private Vector2 moveDir;
    private Vector2 jumpDir;
    

    // inspector顯示的位數好像是此處 +1位；自動判讀 大寫+小寫 = 單字的結尾，第一個大寫前的一串小寫 = 單字
    public float moveSpeed = 3.5f;  // 宣告一個 公開+浮點數 的變數，並令其值
    public float jumpSpeed = 5.0f;

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        Debug.Log("Awake階段");
    }

    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_transform = GetComponent<Transform>();  // 是初始位置，還是是一個會持續變更值的容器?

        // [練習] 讓角色在 start 時，移動到特定位置 (與箱子重疊)
        // GetComponent<Transform>().localPosition = new Vector3 (2,-1,0); //(左右,上下,無)
    }

    // Update is called once per frame
    void Update()
    {
        // 以一個速率移動：初始位置+位移量*(後一秒-前一秒)
        // 每個影格都執行+100，那手機FPS=30，電腦FPS=120，那手機上就只會執行 100*30次，而電腦則會執行=100*120=比較多次
        // 所以 Time.deltaTime 的值，在手機= 1/30，在電腦= 1/120
        // 這種寫法有可能會因為
        // transform.localPosition += new Vector3(2.5f, 0, 0) * Time.deltaTime;
        // GetComponent<Transform>().localPosition += new Vector3(2.5f, 0, 0) * Time.deltaTime;



        // 為了避免卡進箱子被擠出來又被卡進箱子repeat，改用物理的方式移動(而不是單純位移)。
        // 設定向量速度 = 給他一個方向向量作為推動的力量

        // 基本邏輯判斷結構：if(){}
        // 複合邏輯判斷結構：或 = if(A || B){}、且 = if(A && B){}

        // 按住右
        if (Input.GetKey(KeyCode.RightArrow))
        {
            // m_Rigidbody2D.velocity = new Vector2(moveSpeed,0);
            moveDir.x = moveSpeed;

            // m_Animator.SetFloat("MoveSpeed",1);  // 他會去找 animator裡面的float變數 "MoveSpeed"，並令為1
            m_Animator.SetBool("isMove", true);  // 切記！在 animator 狀態切換時，condition條件是 "and"
            // 還有向左/向右的條件要同一個，不然會抖腳 (切過去卻沒切回來)
            // How建議：可以先寫好狀態結構再製作，另外他好像有模糊的建議"把向左和向右分開寫"的概念
            
            m_SpriteRenderer.flipX = false;  // 米飯

        }
        // 按住左
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // m_Rigidbody2D.velocity = new Vector2(-moveSpeed, 0);
            moveDir.x = -moveSpeed;
            // m_Animator.SetFloat("MoveSpeed", 1);
            
            // 處理轉向
            // GetComponent<SpriteRenderer>().flipX = true;  // Jumi
            m_SpriteRenderer.flipX = true;  // 米飯

            m_Animator.SetBool("isMove", true);
        }

        // 放開右 || 放開左
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            moveDir = Vector2.zero;
            m_Animator.SetBool("isMove", false);
            // GetComponent<SpriteRenderer>().flipX = false;  // Jumi
        }


        // 目的：讓當前的 Rigidbody2D 重力(x,y)，x等於你方向鍵的力，y=他原先的重力
        // 所以你先令一個 moveDir = (x,y)，x = moveSpeed
        // y = 當前的 Rigidbody2D 重力(x,y)部分的y
        // 再把這個 (x,y)，重新命給 當前的 Rigidbody2D 重力(x,y)
        moveDir.y = m_Rigidbody2D.velocity.y;  // 當前 rigidbody2D 已經令給 m_rigidbody2D，取得 m_rigidbody2D 的重力數值作為moveDir.y

        m_Rigidbody2D.velocity = moveDir;  // 這時候的 moveDir=(x,y)=( moveSpeed或-moveSpeed , 當前重力數值=m_Rigidbody2D.velocity.y )



        // 按下空白
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // m_Rigidbody2D.velocity = new Vector2(0, jumpHight);
            jumpDir.y = jumpSpeed;
            m_Animator.SetBool("isMove", true);

            // 要怎樣才能寫出不會連跳
            // if (transform.localPosition.y == m_transform.localPosition.y + jumpHight)
            // {
            //     jumpHight = 0;
            // }
        }
        // 放開空白
        if (Input.GetKeyUp(KeyCode.Space))
        {
            // m_Rigidbody2D.velocity = new Vector2(0, -jumpHight*0.2f); //如果不寫分段，直接*0.5會太怪異
            // m_Rigidbody2D.velocity = Vector2.zero;
            jumpDir = Vector2.zero;
            // 放開空白就恢復idle很怪，應該要加上條件：當高度=多少
            m_Animator.SetBool("isMove", false);
        }

        jumpDir.y = m_Rigidbody2D.velocity.y;  // 當前 rigidbody2D 已經令給 m_rigidbody2D，取得 m_rigidbody2D 的重力數值作為moveDir.y
        m_Rigidbody2D.velocity = jumpDir;

        // 向後(左)跳，向前(右)跳 = 按住+按住
        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.RightArrow))
        {
            m_Rigidbody2D.velocity = new Vector2(moveSpeed, jumpSpeed);
        }

        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.LeftArrow))
        {
            m_Rigidbody2D.velocity = new Vector2(-moveSpeed, jumpSpeed * 0.8f);
        }


    }

    void LateUpdate()
    {

    }
}