using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NeuralNetwork : ScriptableObject
{
    public int iNodes;
    public int hNodes;
    public int oNodes;
    public int layercount;
    public List<(Matrix weights, Matrix biases)> layers = new List<(Matrix weights, Matrix biases)>();
    public int[] activation_functions;
    public NeuralNetwork(int inputnodes, int hiddennodes, int outputnodes, int amount_of_layers)
    {
        iNodes = inputnodes;
        hNodes = hiddennodes;
        oNodes = outputnodes;
        layercount = amount_of_layers;
        layers.Add((new Matrix(hNodes, iNodes), new Matrix(hNodes, iNodes)));
        for(var i = 0; i < amount_of_layers; i++)
        {
            layers.Add((new Matrix(hNodes, hNodes), new Matrix(hNodes, hNodes)));
        }
        layers.Add((new Matrix(oNodes, hNodes), new Matrix(oNodes, hNodes)));
        activation_functions = new int[amount_of_layers + 2];
        randomize();
    }
    public NeuralNetwork(NeuralNetwork parent, GameObject creature)
    {
        iNodes = parent.iNodes;
        oNodes = parent.oNodes;
        if (CreatureCreator.roll())
        {
            hNodes = 6 * creature.GetComponent<Creature>().Eyes.Length;
        }
        else
        {
            hNodes = parent.hNodes;
        }
        if (CreatureCreator.roll())
        {
            layercount = creature.GetComponent<Creature>().Eyes.Length;
        }
        else
        {
            layercount = parent.layercount;
        }
        layers.Add((new Matrix(hNodes, iNodes), new Matrix(hNodes, iNodes)));
        for (var i = 0; i < layercount; i++)
        {
            layers.Add((new Matrix(hNodes, hNodes), new Matrix(hNodes, hNodes)));
        }
        layers.Add((new Matrix(oNodes, hNodes), new Matrix(oNodes, hNodes)));
        activation_functions = new int[layercount + 2];
        mutate(parent);
    }
    public void randomize()
    {
        foreach((Matrix weights, Matrix biases) layer in layers)
        {
            layer.weights.randomize();
            layer.biases.randomize();
            activation_functions[layers.IndexOf(layer)] = Random.Range(0, 2);
        }

    }
    public void mutate(NeuralNetwork parent)
    {
        foreach ((Matrix weights, Matrix biases) layer in layers)
        {
            layer.weights.mutate(parent.layers[layers.IndexOf(layer)].weights);
            layer.biases.mutate(parent.layers[layers.IndexOf(layer)].biases);
            if (CreatureCreator.roll())
            {
                activation_functions[layers.IndexOf(layer)] = Random.Range(0, 2);
            }
            else
            {
                activation_functions[layers.IndexOf(layer)] = parent.activation_functions[layers.IndexOf(layer)];
            }
        }
    }
    public void set(NeuralNetwork n)
    {
        layers = n.layers;
    }
    public Matrix output(Matrix inputs)
    {
        Matrix currentMatrix = new Matrix(0,0);
        foreach ((Matrix weights, Matrix biases) layer in layers)
        {
            currentMatrix = layer.weights.dot(inputs);
            currentMatrix.add(layer.biases);
            activate(currentMatrix,layers.IndexOf(layer));
            inputs = currentMatrix;
        }
        return currentMatrix;
    }
    void activate(Matrix m, int index)
    {
        switch (activation_functions[index])
        {
            case 0:
                m.sigmoid();
                break;
            case 1:
                m.ReLU();
                break;
        }
    }
}
