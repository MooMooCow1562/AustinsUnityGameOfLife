using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class BoardMaker : MonoBehaviour
{
    public int columns, rows;
    public float scalar;
    public GameObject gameCell;
    public Transform parent;
    private GameObject[,] board;
    public void Start()
    {
        board = new GameObject[columns, rows];

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                //creating new cell
                board[i, j] = Instantiate(gameCell, gameObject.transform);
                //setting it's position
                board[i, j].GetComponent<Transform>().position = new Vector3(gameObject.GetComponent<Transform>().position.x + i, gameObject.GetComponent<Transform>().position.y - j, 0);
                //naming the cell
                board[i, j].name = "Cell: " + i + " " + j;
                //giving the cell it's position in the grid
                board[i, j].GetComponent<GridPosition>().xPos = i;
                board[i, j].GetComponent<GridPosition>().yPos = j;
                //coloring the cell black
                board[i, j].GetComponent<SpriteRenderer>().color = Color.black;

            }
        }
        gameObject.GetComponent<Transform>().localScale = new Vector3(scalar, scalar, scalar);
        if (!GetComponentInParent<GameOfLife>().connected)
        {
            GetComponentInParent<GameOfLife>().SendMessage("ConnectToBoard");
        }
    }
    public GameObject[,] getBoard()
    {
        return board;
    }

}
