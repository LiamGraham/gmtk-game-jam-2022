using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public float Speed { get; set; } = 1;


    // Start is called before the first frame update
    void Start()
    {
        Console.WriteLine($"Speed Set {Speed}");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(Speed * Time.deltaTime, 0);
    }
}
