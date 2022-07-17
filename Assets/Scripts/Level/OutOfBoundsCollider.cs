using System;
using System.Collections;
using UnityEngine;

public class OutOfBoundsCollider : MonoBehaviour
{
    public float delay = 0;
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.IsPlayer() && !triggered)
        {
            triggered = true;
            StartCoroutine(Activate(delay));
        }
    }

    public IEnumerator Activate(float delay)
    {
        yield return new WaitForSeconds(delay);
        LevelEventManager.PlayerDied?.Invoke();
        triggered = false;
    }
}
