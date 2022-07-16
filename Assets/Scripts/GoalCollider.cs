using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        Console.WriteLine("Collision", other.gameObject.tag);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
