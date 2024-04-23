using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spike : MonoBehaviour
{
    bool ready = false;
    void Start()
    {
        StartCoroutine("readup");
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.transform.gameObject.tag == "Player" && ready)
        {
            col.transform.gameObject.GetComponent<Creature>().die();
        }
    }
    IEnumerator readup()
    {
        yield return new WaitForSeconds(3f);
        ready = true;
    }
}
