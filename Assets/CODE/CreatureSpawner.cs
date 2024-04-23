using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
    public Genes g;
    void Start()
    {
        CreatureCreator c = GetComponent<CreatureCreator>();
        if (g == null)
        {
            c.createRandom();
        }
        else
        {
            c.Build(g);
        }
    }
}
