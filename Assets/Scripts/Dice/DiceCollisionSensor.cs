using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCollisionSensor : MonoBehaviour
{
    private BoxCollider vertexCollider;

    void Start() {
        vertexCollider = GetComponent<BoxCollider>();
        ShowCollider(); 
    }


    void ShowCollider() {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.SetParent(this.transform);
        cube.transform.position = this.transform.position;
        cube.transform.localScale = vertexCollider.size;
        cube.GetComponent<BoxCollider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider other) {
        // Debug.Log("Collision detected");
        DiceCollisionManager.Instance.Collided.Invoke();
    }
}
