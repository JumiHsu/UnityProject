using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class mainCharacter_localposition : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody2D;  // 宣告一個 私有+"Rigidbody2D"類型 的變數
    private Animator m_Animator;  // 為了要取得 animator 的 component，所以先做一個 m_***

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        Debug.Log("Awake階段");
    }

    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();

        // [練習] 讓角色在 start 時，移動到特定位置 (與箱子重疊)
        GetComponent<Transform>().localPosition = new Vector3 (2,0.6f,0); //(左右,上下,無)
    }

    // Update is called once per frame
    void Update()
    {
        // 以一個速率移動：初始位置+位移量*(後一秒-前一秒)
        // 每個影格都執行+100，那手機FPS=30，電腦FPS=120，那手機上就只會執行 100*30次，而電腦則會執行=100*120=比較多次
        // 所以 Time.deltaTime 的值，在手機= 1/30，在電腦= 1/120
        // 這種寫法有可能會因為在奇怪的地方產生碰撞，而發生奇怪的推擠現象
        transform.localPosition += new Vector3(2.5f, 0, 0) * Time.deltaTime;
        GetComponent<Transform>().localPosition += new Vector3(2.5f, 0, 0) * Time.deltaTime;

    }

    void LateUpdate()
    {

    }
}