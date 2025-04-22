using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class HighlightOnHover : MonoBehaviour
{
    private Color oldColor;
    private int[] pos;

    private void Start()
    {
        pos = new int[] { gameObject.GetComponent<GridPosition>().xPos, gameObject.GetComponent<GridPosition>().yPos };
    }

    private void OnMouseEnter()
    {
        oldColor = gameObject.GetComponent<SpriteRenderer>().color;
        gameObject.GetComponent<SpriteRenderer>().color = Vector4.Lerp(oldColor, gameObject.GetComponentInParent<Transform>().GetComponentInParent<Rules>().Highlight, .5f);
    }
    private void OnMouseExit()
    {
        gameObject.GetComponent<SpriteRenderer>().color = oldColor;
    }

    private void OnMouseDown()
    {
        Rules rules = gameObject.GetComponentInParent<Transform>().GetComponentInParent<Rules>();
        if (oldColor == rules.Dead_Cell)
        {
            oldColor = rules.Living_Cell;
        } else if (oldColor == rules.Ghost_Cell)
        {
            oldColor = rules.Dead_Cell;
        }else if (oldColor == rules.Living_Cell)
        {
            oldColor= rules.Ghost_Cell;
        }
        Invoke("OnMouseExit", 0f);
        Invoke("OnMouseEnter", 0f);
    }

}
