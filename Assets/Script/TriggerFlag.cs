using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFlag : MonoBehaviour
{
    public GameObject soundObject;

    void OnTriggerEnter2D(Collider2D toucher)
    {
        Debug.Log(toucher.name);

        if(toucher.name == "mainCharacter")
        {
            Destroy(gameObject);

            var soundTarget = Instantiate(soundObject, transform.position, Quaternion.identity);
            Destroy(soundTarget,2.0f);

            // Instantiate(soundObject, transform.position, Quaternion.identity);
            // var soundTarget = GameObject.Find("FlagTouch_DM-CGS-45(Clone)");
            // Destroy(soundTarget,4.0f);
        }
    }
}
