using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class MainCharacter_force_kao_ori : MonoBehaviour
{
    // 為了要取得 Animator 的 component，所以先做一個 m_Animator
    private Rigidbody2D m_Rigidbody2D;
    private Animator m_Animator;
    private SpriteRenderer m_SpriteRenderer;
    private Transform m_transform;

    public static float moveSpeed = 3.5f;
    public Vector2 moveDir;
    public int jumpForce = 400;

    public bool isGroundRaycast;  // 用raycast來判斷，但沒作用
    public bool isGroundPosY;  // 用高度來判斷，很爛，但有作用
    public bool isGrounded = false;  // 用接觸的物件的tag來判斷，但沒作用

    public GameObject groundedObj;  //判斷踩著的物體是什麼
    public ContactPoint2D[] contacts;  // contacts是一個陣列
    public float hurtForce = 200.0f;

    public GameObject CANT_beHurtedFlagPrefab;
    private GameObject CANT_beHurtedFlag = null;

	private bool isMonsterTouch = false;
	private float timerMonsterTouch = 0;

    // public ContactPoint2D[] hurtContacts;  // contacts是一個陣列

    public static Vector2[] normalContacts;

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


        // isGroundRaycast = 最好的條件
        // isGrounded = 沒加2D所以壞掉，已修復
        // isGroundPosY = 不好，設定在空中無法按下space，雖可以阻止連跳，但會讓角色無法站在箱子上跳躍
        if (Input.GetKeyDown(KeyCode.Space) && isGroundRaycast)
        {
            m_Rigidbody2D.AddForce(Vector2.up * jumpForce);  //= (0,1)*jumpForce = (0, jumpForce)
            m_Animator.SetTrigger("jumpTrigger");
            Debug.Log("jumpTrigger的狀態為：" + m_Animator.GetBool("jumpTrigger"));
        }


        // 本來以為會像游泳，結果發現比較像是 Flappy Bird 的飛
        if (Input.GetKeyDown(KeyCode.V) && m_Rigidbody2D.velocity.y < 5.0f )
        {
            m_Rigidbody2D.AddForce(Vector2.up * jumpForce * 0.5f);
            m_Animator.SetTrigger("swimTrigger");
            Debug.Log("swimTrigger的狀態為：" + m_Animator.GetBool("swimTrigger"));
        }



        // 目的：讓當前的 Rigidbody2D 重力(x,y)，x等於你方向鍵的力，y=他原先的重力
        // 所以你先令一個 moveDir = (x,y)，x = moveSpeed
        // y = 當前的 Rigidbody2D 重力(x,y)部分的y
        // 再把這個 (x,y)，重新命給 當前的 Rigidbody2D 重力(x,y)
        moveDir.y = m_Rigidbody2D.velocity.y;  // 當前 rigidbody2D 已經令給 m_rigidbody2D，取得 m_rigidbody2D 的重力數值作為moveDir.y
		if (isMonsterTouch == true)
		{
			timerMonsterTouch += 0.02f;
			if (timerMonsterTouch < 0.5f)
				moveDir.x = m_Rigidbody2D.velocity.x;
			else
			{
				isMonsterTouch = false;
				timerMonsterTouch = 0;
			}
		}

		m_Rigidbody2D.velocity = moveDir;  // 這時候的 moveDir=(x,y)=( moveSpeed或-moveSpeed , 當前重力數值=m_Rigidbody2D.velocity.y )



        //每秒都判斷是否離地
        //1 用 OnCollisionStay 判斷觸及的物件tag
        m_Animator.SetBool("isGround", isGrounded);


        //2 判斷 y是否 < 0.115f
        if (transform.position.y < 0.115f)
        {
            isGroundPosY = true;
        }
        else
        {
            isGroundPosY = false;
        }


        //3 判斷 接觸點打出的射線 - 他會總是打的到人：改成 Physics2D即可
        if (Physics2D.Raycast(transform.position, -Vector2.up, 1.0f))
        {
            // Debug.DrawLine(startPoint, endPoint, Color.red);
            isGroundRaycast = true;
            m_Animator.SetBool("isGround", true);
        }
        else
        {
            isGroundRaycast = false;
            m_Animator.SetBool("isGround", false);  //不只是跳的那一下=離地，落地中也應該要是離地
        }

    }


    //註2
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Standable") )  // 判斷踩著的物體是不是Ground，是的話：就判斷一下他的normal值的y是多少
        {
            foreach (ContactPoint2D element in other.contacts)  //element是接觸點相關資訊，其中一個資訊是接觸點法線normal
            {
                if (element.normal.y > 0.25f)
                {
                    isGrounded = true;
                    groundedObj = other.gameObject;  //如果踩著的物體，他的tag是 ground，就把他指派給 groundedObj
                    break;
                }
            }
        }



        // 不知道為什麼，他跳的效果不好，會先微微後退(正確) 然後強力往上(不要這樣)
        // 判斷踩著的物體是不是Monster，是的話：給他一個法線方向的力
        if (other.gameObject.CompareTag("Monster") )  //&& CANT_beHurtedFlag == null
        {
			isMonsterTouch = true;
			var hurtVector = other.contacts[0].normal + new Vector2(hurtForce * 2, hurtForce * 0.5f);
            m_Rigidbody2D.AddForce(hurtVector );
            // Debug.Log("被 Monster 彈開了!" + "力道是" + hurtForce);
            Debug.Log("彈開新力道 = "+other.contacts[0].normal + new Vector2(hurtForce * 2, hurtForce * 0.5f));
            Debug.Log("接觸點向量的長度=" + other.contacts.Length);


            // 註1
            for (int i=0; i<other.contacts.Length; i++)
            {
                Debug.Log("i = " +i +"，第" +i +"個接觸點為：" +other.contacts[i].point +"，該點法向量為：" +other.contacts[i].normal);
            }

            // CANT_beHurtedFlag = Instantiate(CANT_beHurtedFlagPrefab,m_transform.position,Quaternion.identity);


        }
            // Destroy(CANT_beHurtedFlag,2.0f);
    }

    /* 
    UnassignedReferenceException: The variable CANT_beHurtedFlagPrefab of MainCharacter_force has not been assigned.
    You probably need to assign the CANT_beHurtedFlagPrefab variable of the MainCharacter_force script in the inspector.
    UnityEngine.Object.Internal_InstantiateSingle (UnityEngine.Object data, UnityEngine.Vector3 pos, UnityEngine.Quaternion rot) <0x3f6231b0 + 0x00062> in <ddf1b6b8983d46ce84146e6e1ef5a65d>:0
    UnityEngine.Object.Instantiate (UnityEngine.Object original, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation) (at C:/buildslave/unity/build/Runtime/Export/UnityEngineObject.bindings.cs:211)
    UnityEngine.Object.Instantiate[T] (T original, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation) (at C:/buildslave/unity/build/Runtime/Export/UnityEngineObject.bindings.cs:285)
    MainCharacter_force.OnCollisionEnter2D (UnityEngine.Collision2D other) (at Assets/Script/MainCharacter_force.cs:180)
    */





    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject == groundedObj)
        {
            groundedObj = null;
            isGrounded = false;
        }
    }


}


/* 註1
            // 為什麼這樣就跳錯
            for (int i=0; i<=other.contacts.Length; i++) {
                Debug.Log("i = " + i + "，" + other.contacts[i].normal);
                // Debug.Log( i.ToString() + other.contacts[i].normal.x.ToString() + other.contacts[i].normal.y.ToString());
                //normalContacts[i] = other.contacts[i].normal;
            }
            // 錯誤訊息
            // IndexOutOfRangeException: Index was outside the bounds of the array.
            // MainCharacter_force.OnCollisionEnter2D(UnityEngine.Collision2D other)(at Assets / Script / MainCharacter_force.cs:166)


            // 在這段裡面加上 normalContacts[i] = other.contacts[i].point; 也會跳錯
            for (int i=0; i<other.contacts.Length; i++)
            {
                Debug.Log("i = " +i +"，第" +i +"個接觸點為：" +other.contacts[i].point +"，該點法向量為：" +other.contacts[i].normal);
                // normalContacts[i] = other.contacts[i].point;
            }
                // 錯誤訊息
                // NullReferenceException: Object reference not set to an instance of an object
                // MainCharacter_force.OnCollisionEnter2D(UnityEngine.Collision2D other)(at Assets / Script / MainCharacter_force.cs:176)
*/

/* 註2
    // Script error: OnCollisionStay
    // This message parameter has to be of type: Collision
    // The message will be ignored.
    // 是我 方法名稱 寫錯了，應該要加上 2D
*/