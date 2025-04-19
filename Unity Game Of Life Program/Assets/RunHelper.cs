using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RunHelper : MonoBehaviour
{
    public Slider slider;
    public GameOfLife game;
    public void RunAtFloatSpeed()
    {
        game.RunRepeatedly(slider.value);
    }
}
