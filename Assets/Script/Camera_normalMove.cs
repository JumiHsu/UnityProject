using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_normalMove : MonoBehaviour
{
    // 設定跟隨目標
    public Transform followTarget;
    private Transform m_Transform;
    private float cameraY;
    private float cameraZ;

    void Start()
    {
        m_Transform = GetComponent<Transform>();
        cameraY = m_Transform.position.y;
        cameraZ = m_Transform.position.z;

    }

    void LateUpdate()
    {
        if (followTarget != null)
        {
            var targetPoint = followTarget.position;
            targetPoint.y = cameraY;
            targetPoint.z = cameraZ;
            if( targetPoint.x > -3.28f && targetPoint.x < 30.0f)
            {
                // 平滑效果，數字越小越有滑溜感；數字越大攝影機越穩定
                m_Transform.position = Vector3.Lerp(m_Transform.position, targetPoint, Time.deltaTime * 10.0f);
            }
            else if( targetPoint.x <= -3.28f )
            {
                m_Transform.position = new Vector3 (-3.0f,cameraY,cameraZ);
            }
            else
            {
                m_Transform.position = new Vector3(29.8f, cameraY, cameraZ);
            }
        }
    }
}
