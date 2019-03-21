using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveArrayCoins_note : MonoBehaviour
{
    public GameObject[] CoinsArray;  // 先到面板設定長度，才會出現設定內容的欄位
    public GameObject scoreObj;

    public GameObject slimeBlue;
    public GameObject slimeBlue_Position;

    public GameObject slimePurple;

    public GameObject slimeGreen;
    public GameObject slimeGreen_Position;
    
    public GameObject mosterSnail;


    void Start()
    {
        Debug.Log("CoinsArray的長度 = " + CoinsArray.Length);
        for (int i=0; i < CoinsArray.Length;i+=1)
        {
            Debug.Log("CoinsArray[" + i + "].name = " + CoinsArray[i].name);
            // Debug.Log("CoinsArray[" + i + "] = " + CoinsArray[i]);  //這樣寫不知道是什麼
        }
    }

    // 碰到旗子，就把這三個金幣打開，並將Score物件開起來
    void OnTriggerEnter2D(Collider2D toucher)
    {
        if (toucher.name == "mainCharacter")
        {
            for (int i=0; i < CoinsArray.Length; i++)
            {
                CoinsArray[i].SetActive(true);  // 米飯：把特定的物件打開(??)
            }

            // 這次的寫法：分數物件不是一個prefab，他本來就在場上，預設是關閉，只是控制他"打開"，旗子賦值的是場上的物件
            // var scoreTarget = Instantiate(scoreObj, transform.position, Quaternion.identity);
            // scoreTarget.SetActive(true);  // 旗子賦值的如果是場上的物件，就需要這一行
            // Destroy(scoreTarget,1.5f);

            // 上次的寫法：分數物件做成prefab，本來不在場上，預設是打開，把他call出來，旗子賦值的是prefab，就不必多一行setActive
            var scoreTarget = Instantiate(scoreObj, transform.position, Quaternion.identity);
            Destroy(scoreTarget, 1.0f);

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
            var Snail_Temp = Instantiate(mosterSnail, transform.position, Quaternion.identity);
            Destroy(Snail_Temp, 2.0f);  //這樣你就可以殺他


            //無效1)Green_Temp 在 position物件位置，生成
            // slimeGreen.SetActive(false);
            Instantiate(slimeGreen, slimeGreen_Position.transform.position, Quaternion.identity);
            var slimeGreen_Temp = GameObject.Find("slimeGreen(Clone)");
            Destroy(slimeGreen_Temp);  // 妳也刪不掉
            // slimeGreen_Temp.SetActive(false);
            // slimeGreen_Temp.SetActive(true);  // 在 position位置 有clone東西，但沒有辦法打開，並跳錯

            // NullReferenceException: Object reference not set to an instance of an object
            // ActiveArrayObjs.OnTriggerEnter2D(UnityEngine.Collider2D toucher)
            // (at Assets / Script / ActiveArrayObjs.cs:82)


            //【NOTE】find的部分：

            // case1
            // 存在一個 prefab a，一個public物件變數A，並設定兩者連結
            // 先 instantiateA這個東西，然後這時 場上會 有一個Clone體 "a(Clone)"

            // 把這個Clone體 指派給一個暫時變數 var temp
            // 那妳就可以對她做一些事


            // case2
            // 只存在一個場上物件A，預設是關閉， 一個public物件變數A，並設定兩者連結
            // 先 instantiateA這個東西，然後這時 場上會 有一個Clone體 "a(Clone)"
            // 這個a(Clone) 也是關閉的

            // find這個Clone體的名稱 並 指派給一個暫時變數 var temp

            // 但妳無法對這個變數 temp做任何操作(刪除 開啟 都不行)

            // NullReferenceException: Object reference not set to an instance of an object
            // ActiveArrayObjs.OnTriggerEnter2D(UnityEngine.Collider2D toucher)
            // (at Assets / Script / ActiveArrayObjs.cs:82)

        }
    }
}
