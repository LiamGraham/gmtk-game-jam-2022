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
        var material = GetComponent<MeshRenderer>().material;
        material.SetFloat("MinWorldHeight", transform.position.y);
    }

    void Update()
    {
        var material = GetComponent<MeshRenderer>().material;
        material.SetFloat("MinWorldHeight", transform.position.y);
    }

}
