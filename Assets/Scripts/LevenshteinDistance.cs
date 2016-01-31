using UnityEngine;
using System.Collections;
using System;

public static class LevenshteinDistance {


    public static int LevenshteinDistanceFunction(string String1, string String2)
    {
        if (String1.Equals(String2)) return 0;

        int MatrixRow = String1.Length + 1;
        int MatrixCol = String2.Length + 1;

        int[,] LevenshteinDistanceMatrix = new int[MatrixRow, MatrixCol];
        //Initializing Matrix
        for(int i = 0; i < MatrixRow; i++)
        {
            LevenshteinDistanceMatrix[i, 0] = i;
        }

        for (int i = 0; i < MatrixCol; i++)
        {
            LevenshteinDistanceMatrix[0, i] = i;
        }

        for(int i = 1; i < MatrixRow; i++)
        {
            for(int j = 1; j < MatrixCol; j++)
            {
                int Cost;

                if (String1[i - 1] == String2[j - 1]) Cost = 0;
                else Cost = 1;

                LevenshteinDistanceMatrix[i, j] = Math.Min(Math.Min(    LevenshteinDistanceMatrix[i - 1, j] + 1,
                                                                        LevenshteinDistanceMatrix[i, j - 1] + 1),
                                                                        LevenshteinDistanceMatrix[i - 1, j - 1] + Cost);   
            }
        }

        Debug.Log("Distance beetween " + String1  + " and " + String2 + " = " + LevenshteinDistanceMatrix[MatrixRow - 1, MatrixCol - 1]);
        return LevenshteinDistanceMatrix[MatrixRow - 1, MatrixCol - 1];
    }



}
