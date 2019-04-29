using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class slimeBlue : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody2D;
    public GameObject soundPrefab;
    private Animator m_Animator;
    public Text HPText;

    public float moveSpeed = 1.0f;
    public Vector2 moveDir;

    public bool isReduceHPEffect;
    public float timer = 0.0f;
    private int reduceHP_blue = 1;

    public ContactPoint2D[] contacts;  // contacts是一個陣列

    void Start() {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
    }


    void OnTriggerEnter2D(Collider2D toucher) {
        // 碰到角色
        if (toucher.name == "mainCharacter") {

            // 接觸
            Debug.Log("誰碰到藍色史萊姆了!? =" + toucher.name);

            // 扣血
            var soundObj = Instantiate(soundPrefab, transform.position, Quaternion.identity);
            GameManager.instance.ReduceHP(reduceHP_blue);  // 實例化 GameManager 去呼叫裡面的方法，扣一次1血
            // HPText.text = "0";  // 直接歸0，可是這樣你沒有改到真正的score，只是粗暴的直接改 UItext 顯示

            // 血量text會閃一下
            isReduceHPEffect = true;

            Destroy(soundObj, 0.5f);
        }


        // other.gameObject.CompareTag("Ground")

        // 碰到其他任何物體(非地面非角色)
        if (toucher.name != null && toucher.tag != "Ground" && toucher.tag != "Player") {
        // if (toucher.name != null && toucher.tag != "Ground" && m_Animator.GetBool("isFacingLeft") == true) {
            Debug.Log("************告訴我他的tag! =" + toucher.tag);
            // Thread.Sleep(1000);  // delay 1 秒 = 1000
            // m_Animator.SetBool("isFacingLeft",false);
            // m_Animator.SetBool("isFacingRight", true);
            if (m_Animator.GetBool("isFacingRight"))
            {
                m_Animator.SetBool("isFacingRight", false);
                m_Animator.SetBool("isFacingLeft", true);
            }
            else if(m_Animator.GetBool("isFacingLeft"))
            {
                m_Animator.SetBool("isFacingLeft", false);
                m_Animator.SetBool("isFacingRight", true);
            }
            else
            {
                Debug.Log("檢查 slimeBlue 的來回巡邏程式碼");
            }

        }

        // 碰到其他任何物體(非地面非角色)
        // if (toucher.name != null && toucher.tag != "Ground" && m_Animator.GetBool("isFacingLeft") == false){
        //     Debug.Log("告訴我他的tag! =" + toucher.tag);
        //     Thread.Sleep(1000);  // delay 1 秒 = 1000
        //     m_Animator.SetBool("isFacingLeft", true);
        //     m_Animator.SetBool("isFacingRight", false);
        // }
    }






    void Update()
        {
            // HP數字閃爍效果
            Color hideColor = new Color(HPText.color.r, HPText.color.g, HPText.color.b, 0.0f);
            Color showColor = new Color(HPText.color.r, HPText.color.g, HPText.color.b, 1.0f);

            if (isReduceHPEffect && timer < 4) {
                timer += Time.deltaTime * 8;

                if (timer % 2 > 1.0f) {
                    HPText.color = hideColor;
                }
                else {
                    HPText.color = showColor;
                }
            }
            else {
                timer = 0;
                isReduceHPEffect = false;
            }

        // 巡邏
        if (m_Animator.GetBool("isFacingLeft")){
            moveDir.x = -moveSpeed;
            m_Rigidbody2D.velocity = moveDir;
            moveDir.y = m_Rigidbody2D.velocity.y;
        }

        if (m_Animator.GetBool("isFacingLeft") == false)
        {
            moveDir.x = moveSpeed;
            m_Rigidbody2D.velocity = moveDir;
            moveDir.y = m_Rigidbody2D.velocity.y;
        }

        
        }
    
}
