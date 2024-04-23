using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerspawner : MonoBehaviour
{
    public GameObject spawner;
    public int amount;
    public float range;
    // Start is called before the first frame update
    void Start()
    {
        for (var i = 0; i < amount; i++) {
            Instantiate(spawner, new Vector2(transform.position.x + 
                Random.Range(-range, range), transform.position.y + 
                Random.Range(-range, range)),
                transform.rotation
                );
        }
    }
}
