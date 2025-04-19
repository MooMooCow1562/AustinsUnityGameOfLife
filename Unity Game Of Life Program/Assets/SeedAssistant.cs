using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SeedAssistant : MonoBehaviour
{
    public TMP_InputField text;
    public GameOfLife game;
    public void SetSeed()
    {
        //stop nothing from being entered.
        if (text.text == null)
        {
            return;
        }

        game.SetRandomSeed(text.text);
    }
}
