using System.Collections.Generic;
using UnityEngine;

public class CreatureCreator : MonoBehaviour
{
    public static GameObject[] BaseCreature;
    public GameObject[] set;
    public static float mutationChance = 0.01f;
    public static int amtofcreatures = 0;
    public static int maxcreatures = 100;
    void Awake()
    {
        if (set != null && BaseCreature == null)
        {
            BaseCreature = set;
        }
    }
    public static bool roll()
    {
        if (Random.Range(0, 101) <= mutationChance)
        {
            return true;
        }
        return false;
    }
    public void Build(Genes genes)
    {
        GameObject creature = Instantiate(BaseCreature[genes.bodytype], transform.position * 1000, Quaternion.Euler(0, 0, 0));
        Creature code = InitializeData(creature, null, genes);
        generateParts(creature, null, false);
        code.Eyes = creature.GetComponentsInChildren<eyescript>();
        creature.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        creature.transform.position = transform.position;
    }
    public static void createChild(GameObject parent)
    {
        if (amtofcreatures >= maxcreatures)
        {
            return;
        }
        amtofcreatures++;
        int bodytype = baseCreatureIndex(parent);
        GameObject creature = Instantiate(
            BaseCreature[bodytype],
            parent.transform.position * 1000,
            Quaternion.Euler(0, 0, 0)
            );
        Creature code = InitializeData(creature, parent);
        generateParts(creature, parent);
        MakeBrain(creature, code, parent);
        code.genes.bodytype = bodytype;
        creature.transform.position = parent.transform.position;
        creature.transform.rotation = parent.transform.rotation;

    }
    public void createRandom()
    {
        if (amtofcreatures >= maxcreatures)
        {
            return;
        }
        amtofcreatures++;
        int bodytype = baseCreatureIndex();
        GameObject creature = Instantiate(
            BaseCreature[bodytype],
            transform.position * 1000,
            transform.rotation
            );
        Creature code = InitializeData(creature);
        generateParts(creature);
        MakeBrain(creature, code);
        code.genes.bodytype = bodytype;
        creature.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        creature.transform.position = transform.position;
    }
    public static void MakeBrain(GameObject creature, Creature code, GameObject parent = null)
    {
        code.Eyes = creature.GetComponentsInChildren<eyescript>();
        if (code.Eyes.Length > 0)
        {
            if (parent != null)
            {
                code.genes.network = new NeuralNetwork(parent.GetComponent<Creature>().genes.network, creature);
            }
            else
            {
                code.genes.network = new NeuralNetwork
                    (
                    (code.Eyes.Length * 12) + 1,
                    6 * code.Eyes.Length,
                    3,
                    code.Eyes.Length
                    );
            }
        }
    }
    public static void generateParts(GameObject creature, GameObject parent = null, bool mutate = true)
    {
        Creature code = creature.GetComponent<Creature>();
        if (parent != null)
        {
            code.genes.partpositions = parent.GetComponent<Creature>().genes.partpositions;
        }
        float accentcoloramount = 0.1f;
        Color clr = new Color
                (
                    Genes.randnormal(accentcoloramount, code.genes.color.r),
                    Genes.randnormal(accentcoloramount, code.genes.color.g),
                    Genes.randnormal(accentcoloramount, code.genes.color.b)
                );
        int iterator = 0;
        foreach (int id in code.genes.parts)
        {
            var part = GetPartFromId(id);
            if (part == null)
            {
                continue;
            }
            float rotation;
            if (mutate && (roll() || parent == null))
            {
                rotation = SetRotation(code, iterator);
            }
            else
            {
                if (code.genes.partpositions.Count - 1 >= iterator)
                {
                    rotation = code.genes.partpositions[iterator];
                }
                else
                {
                    rotation = SetRotation(code, iterator);
                }
            }
            int amount = FindAmount(rotation);
            for (var i = 0; i < amount; i++)
            {
                AttatchPart(part, creature, rotation, clr);
                rotation = -rotation;
            }
            part.changeStats(code);
            iterator++;
        }
    }
    public static void AttatchPart(Part part, GameObject creature, float rotation, Color clr)
    {
        //create part
        GameObject local = Instantiate(
            part.obj,
            creature.transform.position + Vector3.up,
            creature.transform.rotation
            );
        //rotate around to rotation
        local.transform.RotateAround(
            creature.transform.position,
            Vector3.forward,
            rotation
            );
        //stick onto mesh
        RaycastHit2D hit = Physics2D.Raycast(
            local.transform.position,
            local.transform.TransformDirection(Vector2.down),
            10
            );
        local.transform.position = hit.point;
        //scale to creature
        local.transform.localScale = Vector2.Scale(local.transform.localScale, creature.transform.localScale);
        //set parent
        local.transform.parent = creature.transform;
        if (!part.keepColor)
        {
            local.GetComponent<SpriteRenderer>().color = clr;
        }
    }
    public static float SetRotation(Creature code, int iterator)
    {
        float rotation = Random.Range(0f, 180f);
        if (code.genes.partpositions.Count - 1 >= iterator)
        {
            code.genes.partpositions[iterator] = rotation;
        }
        else
        {
            code.genes.partpositions.Add(rotation);
        }
        return rotation;
    }
    public static int FindAmount(float rotation)
    {
        if (rotation == 0 || rotation == 180)
        {
            return 1;
        }
        return 2;
    }
    public static int baseCreatureIndex(GameObject parent = null)
    {
        if (roll() || !parent)
        {
            return Random.Range(0, BaseCreature.Length);
        }
        return parent.GetComponent<Creature>().genes.bodytype;
    }
    public static Creature InitializeData(GameObject creature, GameObject parent = null, Genes genes = null)
    {
        Creature code = creature.GetComponent<Creature>();
        if (genes == null)
        {
            if (parent)
            {
                code.genes = new Genes(parent.GetComponent<Creature>().genes);
            }
            else
            {
                code.genes = new Genes();
            }
        }
        else
        {
            code.genes = genes;
        }
        if (code.genes.diet == "Plant")
        {
            creature.tag = "Meat";
        }
        creature.name = "Creature";
        creature.GetComponent<SpriteRenderer>().color = code.genes.color;
        return code;
    }
    public static Part GetPartFromId(int id)
    {
        Part part = null;
        switch(id)
        {
            case 1:
                part = new Eye();
                break;
            case 2:
                part = new Flipper();
                break;
            case 3:
                part = new Stomach();
                break;
            case 4:
                part = new Pouch();
                break;
            case 5:
                part = new Spike();
                break;
        }
        return part;
    }
}