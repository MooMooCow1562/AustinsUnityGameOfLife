using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameOfLife : MonoBehaviour
{
    public BoardMaker boardMaker, nextBoardMaker;
    public int randomSeed = 50;
    public int range;
    public bool connected = false;
    private Rules rules;
    private FindNextState search;

    private void Start()
    {
        rules = gameObject.GetComponent<Rules>();
        search = gameObject.GetComponent<FindNextState>();
    }
    /// <summary>
    /// Connects the Game of Life to a fully generated board.
    /// </summary>
    public void ConnectToBoard()
    {
        if (boardMaker.getBoard() != null)
        {
            connected = true;
            Debug.Log("Board connected successfully.");
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public void RandomizeBoard()
    {
        //do nothing if board is not connected to this script yet.
        if (!connected)
        {
            Debug.Log("Board not yet connected.");
            return;
        }
        // do something if board is connected.

        for (int i = 0; i < boardMaker.getBoard().GetLength(0); i++)
        {
            for (int j = 0; j < boardMaker.getBoard().GetLength(1); j++)
            {
                int newState = Random.Range(0, 3);
                switch (newState)
                {
                    case 0: nextBoardMaker.getBoard()[i, j].GetComponent<SpriteRenderer>().color = rules.Dead_Cell; break;
                    case 1: nextBoardMaker.getBoard()[i, j].GetComponent<SpriteRenderer>().color = rules.Ghost_Cell; break;
                    case 2: nextBoardMaker.getBoard()[i, j].GetComponent<SpriteRenderer>().color = rules.Living_Cell; break;
                }
            }
        }
        CloneBoard();
        Debug.Log("Randomized the board!");
    }
    public void SetRandomSeed(string seed)
    {
        if (!connected)
        {
            Debug.Log("Board not yet connected.");
            return;
        }
        int[] seeder = seed.ToIntArray();
        int newSeed = 0;
        for (int i = 0; i < seeder.Length; i++)
        {
            newSeed += seeder[i];
        }
        randomSeed = newSeed;
        Random.InitState(randomSeed);
        Debug.Log("Successfully changed the seed.");
    }
    /// <summary>
    /// resets both boards.
    /// </summary>
    public void Reset()
    {
        for (int i = 0; i < boardMaker.getBoard().GetLength(0); i++)
        {
            for (int j = 0; j < boardMaker.getBoard().GetLength(1); j++)
            {
                boardMaker.getBoard()[i, j].GetComponent<SpriteRenderer>().color = rules.Dead_Cell;
            }
        }
        ReverseCloneBoard();
        Debug.Log("Boards reset.");
    }
    /// <summary>
    /// runs the game a single generation
    /// </summary>
    public void Run()
    {
        ReverseCloneBoard();
        for (int i = 0; i < boardMaker.getBoard().GetLength(0); i++)
        {
            for (int j = 0; j < boardMaker.getBoard().GetLength(1); j++)
            {
                nextBoardMaker.getBoard()[i, j].GetComponent<SpriteRenderer>().color = search.NextState(i, j, boardMaker.getBoard(), Color.white);//the rules.Dead_Cell die once more.
            }
        }
        CloneBoard();
        Debug.Log("Ran successfully.");
    }
    public void RunRepeatedly(float gameSpeed)
    {
        InvokeRepeating("Run", 1 * gameSpeed, 1 * gameSpeed);
    }
    public void CloneBoard()
    {
        for (int i = 0; i < boardMaker.getBoard().GetLength(0); i++)
        {
            for (int j = 0; j < boardMaker.getBoard().GetLength(1); j++)
            {
                boardMaker.getBoard()[i, j].GetComponent<SpriteRenderer>().color = nextBoardMaker.getBoard()[i, j].GetComponent<SpriteRenderer>().color;
            }
        }
    }
    public void ReverseCloneBoard()
    {
        for (int i = 0; i < boardMaker.getBoard().GetLength(0); i++)
        {
            for (int j = 0; j < boardMaker.getBoard().GetLength(1); j++)
            {
                nextBoardMaker.getBoard()[i, j].GetComponent<SpriteRenderer>().color = boardMaker.getBoard()[i, j].GetComponent<SpriteRenderer>().color;
            }
        }
    }
}

