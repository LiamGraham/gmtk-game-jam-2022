using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoalCollider : MonoBehaviour
{
    public UnityEvent LevelFinishedEvent = new();

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        Console.WriteLine("Collision", other.gameObject.tag);
        LevelFinishedEvent.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
