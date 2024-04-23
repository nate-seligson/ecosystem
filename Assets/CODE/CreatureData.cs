using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CreatureData
{
    public static string GenerateFacts(Creature creature)
    {
        string[] data_list =
        {
            "Lifespan: " + creature.lifespan.ToString(),
            "Child Rate: " + creature.childrate.ToString(),
            "Starvation: " + creature.foodTimer.ToString(),
            "Starvation Rate:" + creature.starvationRate.ToString(),
            "Swim Speed: " + creature.genes.swimspeed.ToString(),
            "Diet: " + creature.genes.diet,
            "Generation: " + creature.genes.generation.ToString()
        };
        string finaltext = "Creature Statistics:\n";
        foreach(string data in data_list)
        {
            finaltext += data + "\n";
        }
        return finaltext;
    }
    
}
