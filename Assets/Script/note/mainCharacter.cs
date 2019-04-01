using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class mainCharacter : MonoBehaviour
{
    // 為了要取得 Animator 的 component，所以先做一個 m_Animator
    private Rigidbody2D m_Rigidbody2D;
    private Animator m_Animator;
    private SpriteRenderer m_SpriteRenderer;
    private Transform m_transform;

    public float moveSpeed = 3.5f;
    public Vector2 moveDir;
    
    public float jumpForce = 750.0f;

    public float jumpSpeed = 5.0f;
    public Vector2 jumpDir;



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
    }

    

    void Update()
    {
        // 物理方式移動(而不是單純位移)：設定向量速度 = 給他一個方向向量作為推動的力量

        // 按住右
        if (Input.GetKey(KeyCode.RightArrow))
        {
            // m_Rigidbody2D.velocity = new Vector2(moveSpeed,0);
            moveDir.x = moveSpeed;

            // 處理轉向
            m_Animator.SetBool("isMove", true);
            m_SpriteRenderer.flipX = false;

        }
        // 按住左
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // m_Rigidbody2D.velocity = new Vector2(-moveSpeed, 0);
            moveDir.x = -moveSpeed;
            
            // 處理轉向
            m_SpriteRenderer.flipX = true;
            m_Animator.SetBool("isMove", true);
        }

        // 放開右 || 放開左
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            moveDir = Vector2.zero;
            m_Animator.SetBool("isMove", false);
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
            m_Rigidbody2D.velocity = new Vector2(0, jumpSpeed);
            // moveDir.y = jumpSpeed;
            // jumpDir.y = jumpSpeed;

            m_Animator.SetBool("isMove", true);
            // 要怎樣才能寫出不會連跳
            // if (transform.position.y == m_transform.localPosition.y + jumpSpeed)
            // {
            //     jumpSpeed = 0;
            // }
        }
        // 放開空白
        if (Input.GetKeyUp(KeyCode.Space))
        {
            m_Rigidbody2D.velocity = new Vector2(0, -jumpSpeed*0.2f); //如果不寫分段，直接*0.5會太怪異
            // m_Rigidbody2D.velocity = Vector2.zero;
            // jumpDir = Vector2.zero;
            // moveDir = Vector2.zero;
            
            // 放開空白就恢復idle很怪，應該要加上條件：當高度=多少
            m_Animator.SetBool("isMove", false);
        }

        // jumpDir.y = m_Rigidbody2D.velocity.y;
        // m_Rigidbody2D.velocity = jumpDir;

        // moveDir.y = m_Rigidbody2D.velocity.y;  // 當前 rigidbody2D 已經令給 m_rigidbody2D，取得 m_rigidbody2D 的重力數值作為moveDir.y
        // m_Rigidbody2D.velocity = moveDir;  // 這時候的 moveDir=(x,y)=( moveSpeed或-moveSpeed , 當前重力數值=m_Rigidbody2D.velocity.y )


        // 向後(左)跳，向前(右)跳 = 按住+按住
        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.RightArrow))
        {
            m_Rigidbody2D.velocity = new Vector2(moveSpeed, jumpSpeed);
        }

        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.LeftArrow))
        {
            m_Rigidbody2D.velocity = new Vector2(-moveSpeed, jumpSpeed * 0.5f);
        }


    }

    void LateUpdate()
    {

    }
}