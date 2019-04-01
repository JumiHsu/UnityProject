using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter_force_ub02 : MonoBehaviour
{
    // 為了要取得 Animator 的 component，所以先做一個 m_Animator
    private Rigidbody2D m_Rigidbody2D;
    private Animator m_Animator;
    private SpriteRenderer m_SpriteRenderer;
    private Transform m_transform;

    private bool isGround;
    private bool isGetKeyDownIdle;
    private bool isGetKeyIdle;
    private bool isGetKeyUpIdle;
    private bool isPlayerIdle;

    public float moveSpeed = 3.5f;
    public Vector2 moveDir;

    public float jumpForce = 350.0f;

    public float jumpSpeed = 5.0f;
    public Vector2 jumpDir;

    public bool isGrounded = false;
    public GameObject groundedObj;  //判斷踩著的物體是什麼

    public ContactPoint2D[] contacts;  // contacts是一個陣列

    

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

        isGround = Physics.Raycast(transform.position, -Vector3.up, 0.2f);
        // isGround = transform.position.y < 0.115f == true ;
        isGetKeyDownIdle = (Input.GetKeyDown(KeyCode.RightArrow) == false) && (Input.GetKeyDown(KeyCode.LeftArrow) == false);
        isGetKeyIdle = (Input.GetKey(KeyCode.RightArrow) == false)  && (Input.GetKey(KeyCode.LeftArrow)==false);
        isGetKeyUpIdle = (Input.GetKeyUp(KeyCode.RightArrow) == false) && (Input.GetKeyUp(KeyCode.LeftArrow) == false);
        isPlayerIdle = (isGetKeyDownIdle==false && isGetKeyIdle==false && isGetKeyUpIdle==false);
    }





    void Update()
    {
        // 每秒都判斷是否離地
        m_Animator.SetBool("isGrounded",isGrounded);
        

        // 移動方式1：定義此物件的移動速度向量，而不是單純位移 (不計重力)

        // 按住右
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDir.x = moveSpeed;

            m_SpriteRenderer.flipX = false;  // 處理轉向
            m_Animator.SetBool("isMove", true);

        }
        // 按住左
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDir.x = -moveSpeed;  // m_Rigidbody2D.velocity = new Vector2(-moveSpeed, 0);

            m_SpriteRenderer.flipX = true;
            m_Animator.SetBool("isMove", true);
        }

        // 放開右 || 放開左
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            moveDir.x = 0;  // moveDir = Vector2.zero;
            m_Animator.SetBool("isMove", false);
        }


        // 跳躍方式1：定義此物件的移動速度向量 (不計重力)
        // m_Rigidbody2D.velocity = new Vector2(0, jumpSpeed);

        // 跳躍方式2：對物件給予一個物理上的力(一個方向向量)，跟1的差別在，force會與向下的重力效果一起計算
        // m_Rigidbody2D.AddForce(Vector2.up * jumpForce);

        // && isGround = 這個條件不好，設定在空中無法按下space，雖可以阻止連跳，但會讓角色無法站在箱子上跳躍
        // 用重力的話，雖然可在空中連跳，但會因為無法對抗重力，而可以避免越跳越高
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            m_Rigidbody2D.AddForce(Vector2.up * jumpForce);  //new Vector2 (0,1) * jumpForce=new Vector2(0, jumpForce)
            m_Animator.SetTrigger("jumpTrigger");

            // m_Animator.SetBool("isMove", true);
            m_Animator.SetBool("isGrounding", false);  // 這一步沒有作用
        }
        

        // 去觀察施予力而跳躍的時候，當前的移動速度
        if (m_Rigidbody2D.velocity.y != 0)
        {
            Debug.Log("按下空白的位移速度：" + m_Rigidbody2D.velocity);
        }


        // // 放開空白就恢復idle很怪，應該要加上"任何時候，當 觸地+無左右按鍵輸入"，則關閉move動畫
        // if (isGround && isPlayerIdle)  //且沒有任何壓下左右鍵
        // {
        //     m_Animator.SetBool("isMove", false);
        // }

        // if (isGround)
        // {
        //     m_Animator.SetBool("isGrounding", true);
        // }



        // // 向後(左)跳，向前(右)跳 = 按住+按住
        // if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.RightArrow))
        // {
        //     // m_Rigidbody2D.velocity = new Vector2(moveSpeed, jumpSpeed * 0.5f);

        //     moveDir.x = moveSpeed;
        //     m_Rigidbody2D.AddForce(new Vector2(0, 5.0f));  // 5.0f 是 line101 觀察得來的，jumpForce的話會噴去外太空
        // }

        // if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.LeftArrow))
        // {
        //     // m_Rigidbody2D.velocity = new Vector2(-moveSpeed, jumpSpeed * 0.5f);

        //     moveDir.x = -moveSpeed;
        //     m_Rigidbody2D.AddForce(new Vector2(0, 5.0f));
        //     // m_Rigidbody2D.AddForce( new Vector2 (0,jumpForce));
        // }



        // 目的：讓當前的 Rigidbody2D 重力(x,y)，x等於你方向鍵的力，y=他原先的重力
        // 所以你先令一個 moveDir = (x,y)，x = moveSpeed
        // y = 當前的 Rigidbody2D 重力(x,y)部分的y
        // 再把這個 (x,y)，重新命給 當前的 Rigidbody2D 重力(x,y)
        moveDir.y = m_Rigidbody2D.velocity.y;  // 當前 rigidbody2D 已經令給 m_rigidbody2D，取得 m_rigidbody2D 的重力數值作為moveDir.y

        m_Rigidbody2D.velocity = moveDir;  // 這時候的 moveDir=(x,y)=( moveSpeed或-moveSpeed , 當前重力數值=m_Rigidbody2D.velocity.y )

    }



    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))  // 判斷踩著的物體是不是Ground，是的話：就判斷一下他的normal值的y是多少
        {
            foreach (ContactPoint2D element in other.contacts)
            {
                if (element.normal.y > 0.25f)
                {
                    isGrounded = true;
                    groundedObj = other.gameObject;  //如果踩著的物體tag是 ground，就把他指派給 groundObj
                    break;
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject == groundedObj)
        {
            groundedObj = null;
            isGrounded = false;
        }
    }


}