using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rules : MonoBehaviour
{
    //new rules.
    public Color Living_Cell = Color.white;
    public Color Ghost_Cell = Color.gray;
    public Color Dead_Cell = Color.black;
    public Color Highlight = Color.blue;
    public int Brush_Size = 1;
    public int[] Appearance_Rules = { 3 };
    public int[] Survival_Rules = { 2, 3 };
    public int Search_Range = 3;
}
