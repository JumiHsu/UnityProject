using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter_force_ub00 : MonoBehaviour
{
    // 為了要取得 Animator 的 component，所以先做一個 m_Animator
    private Rigidbody2D m_Rigidbody2D;
    private Animator m_Animator;
    private SpriteRenderer m_SpriteRenderer;
    private Transform m_transform;

    public float moveSpeed = 3.5f;
    public Vector2 moveDir;

    public float jumpForce = 350.0f;

    public float jumpSpeed = 5.0f;
    public Vector2 jumpDir;


    public bool isGroundRaycast;  // 用raycast來判斷，但沒作用
    public bool isGroundPosY;  // 用高度來判斷，很爛，但有作用
    public bool isGrounded = false;  // 用接觸的物件的tag來判斷，但沒作用
    
    public GameObject groundedObj;  //判斷踩著的物體是什麼
    public ContactPoint2D[] contacts;  // contacts是一個陣列

    

    void Awake()
    {
        Debug.Log("林克醒醒");
    }


    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_transform = GetComponent<Transform>();  // 是初始位置，還是是一個會持續變更值的容器?

        // isGroundRaycast = Physics.Raycast(transform.position, -Vector3.up, 0.2f);
        // isGroundPosY = transform.position.y < 0.115f == true ;

    }



        // 移動方式1：定義此物件的移動速度向量，而不是單純位移
        // m_Rigidbody2D.velocity = new Vector2(-moveSpeed, 0);
        // 跳躍方式1：定義此物件的移動速度向量 (不計重力)
        // m_Rigidbody2D.velocity = new Vector2(0, jumpSpeed);
        // 跳躍方式2：對物件給予一個物理上的力(一個方向向量)，跟1的差別在，force會與向下的重力效果一起計算
        // m_Rigidbody2D.AddForce(Vector2.up * jumpForce);

    void Update()
    {
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
            moveDir.x = -moveSpeed;
            m_SpriteRenderer.flipX = true;
            m_Animator.SetBool("isMove", true);
        }

        // 放開右 || 放開左
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            moveDir.x = 0;  // moveDir = Vector2.zero;
            m_Animator.SetBool("isMove", false);
        }


        // isGroundPosY = 這個條件不好，設定在空中無法按下space，雖可以阻止連跳，但會讓角色無法站在箱子上跳躍
        // isGrounded = 這個條件沒有發動
        // isGroundRaycast = 這個條件沒有發動

        // 不做離地判斷 = 用重力的話，其實還是可在空中連跳(且越跳越高)，但會因為重力的影響，而沒那麼容易越跳越高
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            m_Rigidbody2D.AddForce(Vector2.up * jumpForce);  //new Vector2 (0,1) * jumpForce=new Vector2(0, jumpForce)
            m_Animator.SetTrigger("jumpTrigger");
            // this.GetComponent<Animator>().SetTrigger("jumpTrigger");

            // m_Animator.SetBool("isMove", true);
            m_Animator.SetBool("isGrounding", false);  // 這一步沒有作用
            m_Animator.SetBool("isGrounded", true);  // 這一步沒有作用
        }
        

        // 去觀察施予力而跳躍的時候，當前的移動速度
        if (m_Rigidbody2D.velocity.y != 0)
        {
            // Debug.Log("按下空白的位移速度：" + m_Rigidbody2D.velocity);
        }

        // 目的：讓當前的 Rigidbody2D 重力(x,y)，x等於你方向鍵的力，y=他原先的重力
        // 所以你先令一個 moveDir = (x,y)，x = moveSpeed
        // y = 當前的 Rigidbody2D 重力(x,y)部分的y
        // 再把這個 (x,y)，重新命給 當前的 Rigidbody2D 重力(x,y)
        moveDir.y = m_Rigidbody2D.velocity.y;  // 當前 rigidbody2D 已經令給 m_rigidbody2D，取得 m_rigidbody2D 的重力數值作為moveDir.y

        m_Rigidbody2D.velocity = moveDir;  // 這時候的 moveDir=(x,y)=( moveSpeed或-moveSpeed , 當前重力數值=m_Rigidbody2D.velocity.y )



        // 每秒都判斷是否離地
        m_Animator.SetBool("isGrounded", isGrounded);

        // 嘗試使用另外兩個判斷
        // isGroundPosY.SetBool("isGroundPosY",isGroundPosY);
        // isGroundRaycast.SetBool("isGroundRaycast", isGroundRaycast);
        if (transform.position.y < 0.115f)
        {
            isGroundPosY = true;
            // m_Animator.SetBool("isGrounding", true);
        }
        else
        {
            isGroundPosY = false;
            // m_Animator.SetBool("isGrounding", false);
        }

        // Debug.Log("transform.position是" + transform.position);

        if (Physics.Raycast(transform.position, -Vector3.up, 1.0f))
        {
            Debug.Log("transform.position是" + transform.position);
            isGroundRaycast = true;
            m_Animator.SetBool("isGrounding", true);
        }
        else
        {
            isGroundRaycast = false;
            m_Animator.SetBool("isGrounding", false);
        }
    }


    void OnCollisionStay(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))  // 判斷踩著的物體是不是Ground，是的話：就判斷一下他的normal值的y是多少
        {
            foreach (ContactPoint2D element in other.contacts)
            {
                Debug.Log("element.normal.y 的值 =" + element.normal.y);
                if (element.normal.y > 0.25f)
                {
                    isGrounded = true;
                    groundedObj = other.gameObject;  //如果踩著的物體，他的tag是 ground，就把他指派給 groundedObj
                    break;
                }
            }
        }
    }
    // Script error: OnCollisionStay
    // This message parameter has to be of type: Collision
    // The message will be ignored.


    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject == groundedObj)
        {
            groundedObj = null;
            isGrounded = false;
            Debug.Log("other.gameObject == groundedObj成立!!");
        }
    }



}