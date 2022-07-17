using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    public float amplitude = 5;
    public float speed = 1;

    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var position = amplitude * Mathf.Sin(speed * Time.time);
        transform.position = startPosition +  (Vector3.up * position);
    }
}
