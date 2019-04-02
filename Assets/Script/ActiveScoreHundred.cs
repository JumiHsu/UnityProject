using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveScoreHundred : MonoBehaviour
{
    public GameObject scoreObj;
    public Text ScoreText;
    private bool isGetScoreEffect = false;
    private float timer = 0.0f;


    void Start()
    {
    }

    // 碰到旗子，就實現 Score prefab
    void OnTriggerEnter2D(Collider2D toucher)
    {
        if (toucher.name == "mainCharacter" && GameManager.instance.gameScore == 3)
        {
            // 寫法1：分數物件做成prefab，本來不在場上，預設是打開，把他call出來，旗子賦值的是prefab，就不必多一行setActive
            var scoreTarget = Instantiate(scoreObj, transform.position, Quaternion.identity);
            Destroy(scoreTarget, 1.0f);

            // 寫法2：分數物件是一個 本來就在場上 的物件，預設是關閉，只是控制他"打開"，旗子賦值的是場上的物件
            // var scoreTarget = Instantiate(scoreObj, transform.position, Quaternion.identity);
            // scoreTarget.SetActive(true);  // 旗子賦值的如果是場上的物件，就需要這一行
            // Destroy(scoreTarget,1.5f);

            GameManager.instance.GetScore(100);
        }
    }


    void Update()
    {
        Color hideColor = new Color(ScoreText.color.r, ScoreText.color.g, ScoreText.color.b, 0.0f);
        Color showColor = new Color(ScoreText.color.r, ScoreText.color.g, ScoreText.color.b, 1.0f);

        if (isGetScoreEffect && timer < 4)
        {
            timer += Time.deltaTime * 15;

            if (timer % 2 > 1.0f)
            {
                ScoreText.color = hideColor;
            }
            else
            {
                ScoreText.color = showColor;
            }
        }
        else
        {
            timer = 0;
            isGetScoreEffect = false;
        }
    }


}
