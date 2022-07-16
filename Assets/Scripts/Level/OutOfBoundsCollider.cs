using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class OutOfBoundsCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.IsPlayer())
        {
            LevelEventManager.Died?.Invoke();
        }
    }
}
