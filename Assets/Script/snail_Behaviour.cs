using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snail_Behaviour : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody2D;
    private Animator m_Animator;
    private SpriteRenderer m_SpriteRenderer;
    private Transform m_transform;

    public Vector2 moveDir;
    public float moveSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_transform = GetComponent<Transform>();  // 是初始位置，還是是一個會持續變更值的容器?
    }


    void Update()
    {
        // moveDir.x = -moveSpeed;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // m_Rigidbody2D.velocity = new Vector2(-moveSpeed, 0);
            moveDir.x = -moveSpeed;
        }

    }
}
