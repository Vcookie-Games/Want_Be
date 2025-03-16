using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReuseSystem.Helper.Extensions
{
    public static class MatrixExtension
    {
        public static T[,] GetSubMatrix<T>(this T[,] matrix,int startRow,int startColumn, int row, int column)
        {
            T[,] result = new T[row, column];
            for (int i = 0; i <  row; i++)
            {
                for (int j = 0; j <  column; j++)
                {
                    result[i, j] = matrix[startRow + i, startColumn + j];
                }
            }

            return result;
        }
    }
}
