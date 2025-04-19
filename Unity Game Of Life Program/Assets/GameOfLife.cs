using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameOfLife : MonoBehaviour
{
    public boardMaker boardMaker;
    public Color alive, dead, ghost;
    public int randomSeed = 50;
    public bool toroid = false;
    public int range;
    private GameObject[,] current, next;
    private bool connected = false;
    private Rules rules;
    private SearchForNeighbors search;
    private void Start()
    {
        rules = gameObject.GetComponent<Rules>();
        search = gameObject.GetComponent<SearchForNeighbors>();
    }

    /// <summary>
    /// Connects the Game of Life to a fully generated board.
    /// </summary>
    public void ConnectToBoard()
    {
        if (boardMaker.getBoard() != null)
        {
            current = boardMaker.getBoard();
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
        next = (GameObject[,])current.Clone();
        for (int i = 0; i < current.GetLength(0); i++)
        {
            for (int j = 0; j < current.GetLength(1); j++)
            {
                int newState = Random.Range(0, 3);
                switch (newState)
                {
                    case 0: next[i, j].GetComponent<SpriteRenderer>().color = dead; break;
                    case 1: next[i, j].GetComponent<SpriteRenderer>().color = ghost; break;
                    case 2: next[i, j].GetComponent<SpriteRenderer>().color = alive; break;
                }
            }
        }
        current = (GameObject[,])next.Clone();
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
        for (int i = 0; i < current.GetLength(0); i++)
        {
            for (int j = 0; j < current.GetLength(1); j++)
            {
                current[i, j].GetComponent<SpriteRenderer>().color = dead;
            }
        }
        next = (GameObject[,])current.Clone();
        Debug.Log("Boards reset.");
    }
    /// <summary>
    /// runs the game a single generation
    /// </summary>
    public void Run()
    {
        next = (GameObject[,])current.Clone();
        for (int i = 0; i < current.GetLength(0); i++)
        {
            for (int j = 0; j < current.GetLength(1); j++)
            {
                //prep search value
                int searchVal;

                //if the world is a 2d toroid.
                if (toroid)
                {//search toroidal space.
                    searchVal = search.ToroidalSearch(i, j, range, current, alive);
                }
                else //if the world is not a 2d toroid
                {//search normal space.
                    searchVal = search.Search(i, j, range, current, alive);
                }
                Debug.Log("Found " + searchVal + " living Cells near ("+i+", "+j+").");
                //if our dear cell is alive
                if (current[i, j].GetComponent<SpriteRenderer>().color == alive)
                {
                    if (searchVal <= rules.solitude) //if the cell is lonely
                    {
                        next[i, j].GetComponent<SpriteRenderer>().color = ghost;//cell dies
                    }else if (searchVal >= rules.overcrowding) //if the cell is being moshed
                    {
                        next[i, j].GetComponent<SpriteRenderer>().color = ghost;//cell dies
                    }else if(searchVal == rules.rebirth || searchVal == rules.survival)
                    {
                        next[i, j].GetComponent<SpriteRenderer>().color = alive;//it continues living.
                    }
                }
                else if (current[i, j].GetComponent<SpriteRenderer>().color == dead || current[i, j].GetComponent<SpriteRenderer>().color == ghost) {
                    if (searchVal == rules.rebirth)//if the cell is in the proper conditions to be reborn
                    {
                        next[i, j].GetComponent<SpriteRenderer>().color = alive;//the cell is born again.
                    }
                    else //otherwise
                    {
                        next[i, j].GetComponent<SpriteRenderer>().color = dead;//the dead die once more.
                    }
                }
            }
        }
        current = (GameObject[,])next.Clone();
        Debug.Log("Ran successfully.");
    }
    public void RunRepeatedly(float gameSpeed)
    {
        InvokeRepeating("Run", 1*gameSpeed,1*gameSpeed);
    }
}
