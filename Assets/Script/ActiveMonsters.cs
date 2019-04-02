using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMonsters : MonoBehaviour
{
    public GameObject slimeBlue;
    public GameObject slimeBlue_Position;

    public GameObject slimePurple;

    public GameObject slimeGreen;
    public GameObject slimeGreen_Position;
    
    public GameObject mosterSnail;


    // 碰到鑰匙，就生怪
    void OnTriggerEnter2D(Collider2D toucher)
    {
        if (toucher.name == "mainCharacter")
        {
            //1)Blue 場上已經有一個藍色，在指定位置 複製出另外一個藍色
            Instantiate(slimeBlue, slimeBlue_Position.transform.position, Quaternion.identity);

            //2)Purple 如果一開始就在場上，只是關著，那單純的開啟跟刪除即可
            slimePurple.SetActive(true);  // 也可以 destroy

            //3)Green 在 position物件位置，生成
            // var tempGreen = Instantiate(slimeGreen, slimeGreen_Position.transform.position, Quaternion.identity);
            // tempGreen.SetActive(true);

            //4)蝸牛prefab 如果他是prefab的話，才需要把prefab instatiate 拉進來
            // Instantiate(mosterSnail, mosterSnail.transform.position, Quaternion.identity);  //生在原地

            // 位置的部分可以代換成：
            // 所掛物件位置(此處是旗子)：transform.position
            // 生在 自己這個prefab 的位置：mosterSnail.transform.position
            // 已宣告之物件變數的位置：slimeGreen_Position 的位置：slimeGreen_Position.transform.position

            // 某個名稱的場上物件的位置(不用先宣告變數)：box_position.transform.position
            var box_position = GameObject.Find("box");
            Instantiate(mosterSnail, box_position.transform.position, Quaternion.identity);

            // 如果你想殺他
            var Snail_Temp = Instantiate(mosterSnail, new Vector3(0, -0.2f, 0), Quaternion.identity);
            Destroy(Snail_Temp,2.0f);  //這樣你就可以殺他


        }
    }
}
