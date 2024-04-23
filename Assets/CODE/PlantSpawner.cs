using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpawner : MonoBehaviour
{
    public GameObject plant;
    public static int amtofplants = 5000;
    public float range = 40;
    public static float waitTime = 0.1f;
    public static List<GameObject> plants = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for(var i = 0; i< amtofplants; i++)
        {
            plants.Add(MakePlant());
        }
        StartCoroutine("loop");
    }
    GameObject MakePlant()
    {
        return Instantiate(
            plant,
            transform.position + new Vector3(Random.Range(-range, range), Random.Range(-range, range), 0),
            transform.rotation
            );
    }
    IEnumerator loop()
    {
        yield return new WaitForSeconds(waitTime);
        int rand = Random.Range(0, plants.Count - 1);
        if (plants.Count <= amtofplants) {
            plants.Add(MakePlant());
        }
        if (plants.Count >= amtofplants) {
            Destroy(plants[rand]);
            plants.RemoveAt(rand);
        }
        StartCoroutine("loop");
    }
}
