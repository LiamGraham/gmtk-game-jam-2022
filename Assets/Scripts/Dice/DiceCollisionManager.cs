using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DiceCollisionManager : MonoBehaviour
{
    public static UnityEvent Collided = new();
    private DiceSoundController soundController;
    private bool didCollide = false;
    private static DiceCollisionManager _instance;
    public static DiceCollisionManager Instance { get { return _instance; } }
    // Start is called before the first frame update
    private int stepCount = 0;
    public int collisionWindow = 15;
    void Awake()
    {
        if (_instance == null) {
            _instance = this;
        }
    }

    void Update() {
        if (!didCollide) {
            return;
        }

        if (stepCount == collisionWindow) { 
            didCollide = false;
            stepCount = 0;
        } else {
            stepCount++;
        }
    }

    public void OnCollision() {
        if (!didCollide) {
            Collided.Invoke();
            didCollide = true;
        }
    }
}
