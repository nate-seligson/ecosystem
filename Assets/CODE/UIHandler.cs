using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class UIHandler : MonoBehaviour
{
    public TMP_InputField[] elems;
    public TextMeshProUGUI facts;
    float[] vals = {
        CreatureCreator.mutationChance,
        CreatureCreator.maxcreatures,
        Genes.max_amount_of_parts,
        1,
        PlantSpawner.amtofplants
    };

    // Start is called before the first frame update
    void Start()
    {
        for(var i = 0; i<elems.Length; i++)
        {
            elems[i].text = vals[i].ToString();
        }
    }
    void Update()
    {
        if (CameraFocus.toggleCreatureView)
        {
            facts.text = CreatureData.GenerateFacts(CameraFocus.targetedCreature.gameObject.GetComponent<Creature>());
        }
        else if (facts.text != "")
        {
            facts.text = "";
        }
    }
    // Update is called once per frame
    void OnGUI()
    {
        CreatureCreator.mutationChance = float.Parse(elems[0].text);
        CreatureCreator.maxcreatures = int.Parse(elems[1].text);
        Genes.max_amount_of_parts = int.Parse(elems[2].text);
        Time.timeScale = float.Parse(elems[3].text);
        PlantSpawner.amtofplants = int.Parse(elems[4].text);
    }
}
