using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemRandomSeed : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        System.Random rnd = new System.Random(1234);
        for (int i = 0; i < 5; i++)
        {
            Debug.Log(rnd.Next(1, 7));
        }
    }

}
