using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Matrix
{
    public int rows;
    public int cols;
    public float[,] matrix = new float[0, 0];
    public Matrix(int r, int c)
    {
        rows = r;
        cols = c;
        matrix = new float[rows, cols];
    }
    public Matrix dot(Matrix n)
    {
        Matrix result = new Matrix(rows, n.cols);
        if (cols == n.rows)
        {
            for(var i = 0; i < rows; i++)
            {
                for(var j = 0; j < n.cols; j++)
                {
                    float s = 0;
                    for(var k = 0; k<n.rows; k++)
                    {
                        s += matrix[i, k] * n.matrix[k, j];
                    }
                    result.matrix[i,j] = s;
                }
            }
        }
        return result;
    }
    public void add(Matrix n)
    {
        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                matrix[i, j] += n.matrix[i, j];
            }
        }
    }
    public void randomize()
    {
        for(var i = 0; i <rows; i++)
        {
            for(var j = 0; j<cols; j++)
            {
                matrix[i, j] = Random.Range(-20f, 20f);
            }
        }
    }
    public void mutate(Matrix parent)
    {
        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                if (CreatureCreator.roll())
                {
                    matrix[i, j] = parent.matrix[i,j] + Random.Range(-5f, 5f);
                }
                else
                {
                    matrix[i, j] = parent.matrix[i, j];
                }
            }
        }
    }
    public void sigmoid()
    {
        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
               matrix[i, j] = 1 / (1 + Mathf.Exp(-matrix[i,j]));
            }
        }
    }
    public void ReLU()
    {
        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                if (matrix[i, j] < 0)
                {
                    matrix[i, j] = 0;
                }
            }
        }
    }
    public int findHighestIndex()
    {
        int high = -1;
        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                if (matrix[i, j] > high)
                {
                    high = i;
                }
            }
        }
        return high;
    }
    public void print()
    {
        for (var i = 0; i < rows; i++)
        {
            List<float> list = new List<float>();
            for (var j = 0; j < cols; j++)
            {
                list.Add(matrix[i, j]);
            }
            string line = "";
            foreach (float item in list)
            {
                line += item.ToString() + " , ";
            }
            Debug.Log(line);
        }
    }
}