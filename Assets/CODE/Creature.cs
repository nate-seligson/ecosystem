using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public eyescript[] Eyes;
    public Genes genes;
    public float foodTimer = 100;
    public float starvationRate = 7f;
    public float lifespan = 2000f;
    public int childrate = 1;
    int foodEaten = 0;
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.transform.gameObject.tag == genes.diet)
        {
            foodTimer += 100;
            foodEaten += 1;
            if (foodEaten >= genes.amount_of_food){
                foodEaten = 0;
                for (var i = 0; i < childrate; i++)
                {
                    CreatureCreator.createChild(gameObject);
                }
            }
            Destroy(col.transform.gameObject);
            if(col.transform.gameObject.name == "Creature")
            {
                CreatureCreator.amtofcreatures--;
            }
            else if(col.transform.gameObject.tag == "Plant")
            {
                PlantSpawner.plants.Remove(col.transform.gameObject);
            }
        }
    }
    void FixedUpdate()
    {
        if (Eyes.Length > 0)
        {
            Debug.Log("yip!");
            int move_decision = FindMove();
            interpret(move_decision);
        }
    }
    void Update()
    {
        GainHunger();
    }
    void GainHunger()
    {
        foodTimer -= starvationRate * Time.deltaTime;
        lifespan -= starvationRate * Time.deltaTime;
        if (foodTimer <= 0 || lifespan <=0  )
        {
            die();
        }
    }
    int FindMove()
    {
        Matrix inputs = new Matrix((Eyes.Length * 12) + 1, 1);
        List<float> vals = new List<float>();
        vals.Add(GetComponent<Rigidbody2D>().velocity.magnitude);
        foreach (eyescript Eye in Eyes)
        {
            vals.AddRange(Eye.see());
        }
        for (var i = 0; i < vals.Count - 1; i++)
        {
            inputs.matrix[i, 0] = vals[i];
        }
        return genes.network.output(inputs).findHighestIndex();
    }
    void interpret(int move)
    {
        switch (move)
        {
            case 0:
                transform.Translate(Vector2.up *genes.swimspeed* Time.deltaTime);
                break;
            case 1:
                transform.Rotate(Vector3.forward * genes.turnspeed * Time.deltaTime);
                break;
            case 2:
                transform.Rotate(Vector3.forward * -genes.turnspeed * Time.deltaTime);
                break;
        }
    }
    public void die()
    {
        CreatureCreator.amtofcreatures--;
        Destroy(gameObject);
    }
}
