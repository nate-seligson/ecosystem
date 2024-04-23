using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Part
{
    public GameObject obj { get; set; }
    public bool keepColor = true;
    public float sizeMultiplier = 1;
    public virtual void changeStats(Creature code)
    {
        return;
    }
}
public class Eye : Part
{
    public Eye()
    {
        obj = (GameObject) Resources.Load("eye", typeof(GameObject));
    }
}
public class Flipper : Part
{
    public Flipper()
    {
        obj = (GameObject)Resources.Load("Flipper", typeof(GameObject));
        keepColor = false;
    }
    public override void changeStats(Creature code)
    {
        code.genes.swimspeed += 1;
        code.genes.turnspeed += 100;
    }
}
public class Stomach : Part
{
    public Stomach()
    {
        obj = (GameObject)Resources.Load("Stomach", typeof(GameObject));
        keepColor = false;
    }
    public override void changeStats(Creature code)
    {
        code.starvationRate --;
    }
}
public class Pouch : Part
{
    public Pouch()
    {
        obj = (GameObject)Resources.Load("Pouch", typeof(GameObject));
        keepColor = false;
    }
    public override void changeStats(Creature code)
    {
        code.childrate++;
    }
}
public class Spike : Part
{
    public Spike()
    {
        obj = (GameObject)Resources.Load("Spike", typeof(GameObject));
        keepColor = true;
    }
}
