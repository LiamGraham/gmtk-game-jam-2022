using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public Vector3 direction = Vector3.left;

    public float magintude = 1;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.AngleAxis(Time.time * magintude, direction);
    }
}
