

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Genes : ScriptableObject
{
    public NeuralNetwork network;
    public Color color;
    public float swimspeed = 0.1f;
    public float turnspeed = 10;
    public static int max_amount_of_parts = 5;
    public static float predatorChance = 0;
    int amount_of_possible_parts = 4;
    public int amount_of_food,bodytype,generation,parts_count;
    public string diet;
    public List<int> parts = new List<int>();
    public List<float> partpositions = new List<float>();
    public Genes(bool ignore = false)
    {
        if (ignore)
        {
            return;
        }
        generation = 0;
        parts_count = max_amount_of_parts;
        color = new Color(
            Random.Range(0f, 255f)/255,
            Random.Range(0f, 255f)/255,
            Random.Range(0f, 255f)/255
            );
        if (Random.value * 100 < predatorChance)
        {
            diet = "Meat";
        }
        else
        {
            diet = "Plant";
        }
        for(var i = 0; i<max_amount_of_parts; i++)
        {
            int index;
            if (Random.value > 0.5f)
            {
                index = Random.Range(0, amount_of_possible_parts + 1);
            }
            else
            {
                index = 0;
            }
            parts.Add(index);
            if (index == 0)
            {
                parts_count--;
            }
        }
        amount_of_food = (int)Mathf.Ceil(parts_count /3);
    }
    public Genes(Genes parent)
    {
        generation = parent.generation + 1;
        parts_count = max_amount_of_parts;
        float colormut = CreatureCreator.mutationChance;
        color = new Color(
            randnormal(colormut, parent.color.r),
            randnormal(colormut, parent.color.g),
            randnormal(colormut, parent.color.b)
            );
        if (CreatureCreator.roll())
        {
            if (parent.diet == "Plant")
            {
                diet = "Meat";
            }
            else
            {
                diet = "Plant";
            }
        }
        else
        {
            diet = parent.diet;
        }
        for (var i = 0; i < max_amount_of_parts; i++)
        {
            int index;
            if (CreatureCreator.roll())
            {
                index = Random.Range(0, amount_of_possible_parts + 1);
            }
            else
            {
                index = parent.parts[i];
            }
            parts.Add(index);
            if (index == 0)
            {
                parts_count--;
            }
        }
        amount_of_food = (int)Mathf.Ceil(parts_count / 3);
    }
    public void Save(string name = null)
    {
        if(name == null)
        {
            float x = Random.value;
            name = x.ToString();
        }
        Genes g = Instantiate(this);
        AssetDatabase.CreateAsset(g, "Assets/Saved Creatures/" + name + ".asset");
    }
    public static float randnormal(float distance, float mid)
    {
        return mid + Random.Range(-distance, distance);
    }
}
