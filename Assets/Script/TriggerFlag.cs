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
            var soundObj = Instantiate(soundObject, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(soundObj,1.2f);
        }
    }
}
