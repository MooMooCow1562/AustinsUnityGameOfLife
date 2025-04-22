using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsistentScaling : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Camera>().aspect = 16 / 9f;
    }

}
