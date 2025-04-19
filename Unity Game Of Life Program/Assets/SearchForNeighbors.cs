using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SearchForNeighbors : MonoBehaviour
{
    /// <summary>
    /// Given a column coordinate and a row coordinate, searches the given board for a square range of cells with a given state.
    /// <br/>
    /// A range of 2 or 3 represents the standard search range for conway's game of life.
    /// <br/>
    /// Range value is divided by 2, and will only search in odd sized search ranges.
    /// <br/>
    /// </summary>
    /// <param name="column"></param>
    /// <param name="row"></param>
    /// <param name="range"></param>
    /// <param name="board"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    /// <exception cref="System.Exception"></exception>
    public int Search(int column, int row, int range, GameObject[,] board, Color state)
    {
        int internalRange = range;
        //check to see if board exists
        if (board == null)
        {
            throw new System.Exception("Board was null, cannot search for living neighbors.");
        }

        //sanitize range and resize range.
        if (internalRange > 1)
        {
            if (internalRange % 2 == 0)
            {
                internalRange += 1;
            }
            internalRange = internalRange / 2;
        }
        else
        {
            throw new System.Exception("Cannot have a search range smaller than 2.");
        }

        if (board.GetLength(0) < internalRange || board.GetLength(1) < internalRange)
        {
            Debug.LogWarning("Board is too small on at least one axis to search effectively.\nProceeding with search regardless.");
        }

        int livingNeighbors = 0;
        int cellsChecked = 0;
        //for every cell in a range*range grid
        for (int i = column - internalRange; i <= column + internalRange; i++)
        {
            for (int j = row - internalRange; j <= row + internalRange; j++)
            {
                if (!((i < 0) || (j < 0) || (i > board.GetLength(0) - 1) || (j > board.GetLength(1) - 1)))//if cell is not out of bounds
                {
                    Color cellState = board[i, j].GetComponent<SpriteRenderer>().color;
                    if (board[i, j] != board[column, row] && cellState == state)
                    {
                        livingNeighbors++;//add to the count
                    }
                }
                cellsChecked++;
                Debug.Log("Checked (" + i + ", " + j + ").");
            }
        }
        Debug.Assert(cellsChecked >= range * range, "Actual cells checked = " + cellsChecked + "\n Check occured on cell: (" + column + ", " + row + ").");
        return livingNeighbors;
    }

    /// <summary>
    /// This functions nearly identical to search, but instead of throwing exceptions when rolling over the edges, it wraps back around.
    /// </summary>
    /// <param name="column"></param>
    /// <param name="row"></param>
    /// <param name="range"></param>
    /// <param name="board"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    /// <exception cref="System.Exception"></exception>
    public int ToroidalSearch(int column, int row, int range, GameObject[,] board, Color state)
    {
        int internalRange = range;
        //check to see if board exists
        if (board == null)
        {
            throw new System.Exception("Board was null, cannot search for living neighbors.");
        }

        Debug.Assert(column < board.GetLength(0));
        Debug.Assert(row < board.GetLength(1));

        //sanitize range and resize range.
        if (internalRange > 1)
        {
            if (internalRange % 2 == 0)
            {
                internalRange += 1;
            }
            internalRange = internalRange / 2;
        }
        else
        {
            throw new System.Exception("Cannot have a search range smaller than 2.");
        }

        if (board.GetLength(0) < internalRange || board.GetLength(1) < internalRange)
        {
            Debug.LogWarning("Board is too small on at least one axis to search effectively.\nProceeding with search regardless.");
        }

        int livingNeighbors = 0;
        int cellsChecked = 0;
        //for every cell in a range*range grid
        for (int i = column - internalRange; i <= column + internalRange; i++)
        {
            for (int j = row - internalRange; j <= row + internalRange; j++)
            {
                int toroidalI = i;
                int toroidalJ = j;
                if ((i < 0) || (j < 0) || (i > board.GetLength(0) - 1) || (j > board.GetLength(1) - 1))//if cell is out of bounds
                {//wrap around.
                    if (i < 0)
                    {
                        toroidalI = i + board.GetLength(0);
                    }
                    else if (i > board.GetLength(0) - 1)
                    {
                        toroidalI = i - board.GetLength(0);
                    }
                    if (j < 0)
                    {
                        toroidalJ = j + board.GetLength(1);
                    }
                    else if (j > board.GetLength(1) - 1)
                    {
                        toroidalJ = j - board.GetLength(1);
                    }
                    Debug.Log("Impossible cell (" + i + ", " + j + ") translated into possible cell (" + toroidalI + ", " + toroidalJ + ").");
                }
                Color cellState = board[toroidalI, toroidalJ].GetComponent<SpriteRenderer>().color;
                if (board[toroidalI, toroidalJ] != board[column, row] && cellState == state) 
                {
                    livingNeighbors++;//add to the count
                }
                cellsChecked++;
                Debug.Log("Checked (" + toroidalI + ", " + toroidalJ + ").");
            }
        }
        Debug.Assert(cellsChecked >= range * range, "Actual cells checked = " + cellsChecked + "\n Check occured on cell: (" + column + ", " + row + ").");
        return livingNeighbors;
    }
}
