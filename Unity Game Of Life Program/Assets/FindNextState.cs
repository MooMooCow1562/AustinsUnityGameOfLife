using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class FindNextState : MonoBehaviour
{
    
    public Rules rules;
    public Color NextState(int column, int row, GameObject[,] board, Color searchState)
    {
        int numOfAdjacent = CheckNearCells(column, row, board,searchState);
        bool shouldLive, shouldComeAlive;
        shouldLive = false;
        shouldComeAlive = false;

        foreach (int rule in rules.Survival_Rules) 
        {
            if (numOfAdjacent == rule)
            {
                shouldLive = true;
                break;
            }
        }

        foreach (int rule in rules.Appearance_Rules)
        {
            if (numOfAdjacent == rule)
            {
                shouldComeAlive = true;
                break;
            }
        }

        if ((!shouldLive))
        {
            if (board[column,row].GetComponent<SpriteRenderer>().color == rules.Living_Cell)
                return rules.Ghost_Cell;
            return rules.Dead_Cell;
        }

        if (shouldComeAlive)
        {
            return rules.Living_Cell;
        }

        if (board[column, row].GetComponent<SpriteRenderer>().color == rules.Ghost_Cell)
        {
            return rules.Dead_Cell;
        }

        return board[column, row].GetComponent<SpriteRenderer>().color;
    }

    private int CheckNearCells(int x, int y, GameObject[,] board, Color searchState)
    {
        int halfRange = rules.Search_Range / 2;
        int count = 0;
        for (int row = -halfRange; row < rules.Search_Range - halfRange; row++)
        {
            for (int col = -halfRange; col < rules.Search_Range - halfRange; col++)
            {
                bool isAlive = (board[FixFlow(x + row, board.GetLength(0)),FixFlow(y + col, board.GetLength(1))].GetComponent<SpriteRenderer>().color == rules.Living_Cell);
                bool isNotSelf = (!board[FixFlow(x + row, board.GetLength(0)), FixFlow(y + col, board.GetLength(1))].Equals(board[FixFlow(x, board.GetLength(0)), FixFlow(y, board.GetLength(1))]));
                if (isAlive && isNotSelf)
                {
                    count++;
                }
            }
        }
        return count;
    }

    private int FixFlow(int broken, int dimension)
    {
        int internalBroken = broken;
        return UnUnderflow(UnOverflow(internalBroken, dimension), dimension);
    }

    private int UnOverflow(int broken, int dimension)
    {
        int internalBroken = broken;
        return internalBroken % dimension;
    }

    private int UnUnderflow(int broken, int dimension)
    {
        int internalBroken = broken;
        if (internalBroken < 0)
        {
            while (internalBroken < 0)
            {
                internalBroken += dimension;
            }
        }
        return internalBroken;
    }
}
