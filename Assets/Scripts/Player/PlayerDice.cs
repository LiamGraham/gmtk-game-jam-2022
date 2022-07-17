using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDice : MonoBehaviour
{
    public float maxAngularVelocity = 500;
    public GameObject DiceVertexPrefab;

    new Rigidbody rigidbody;

    private static PlayerDice _instance;
    public static PlayerDice Instance { get { return _instance; } }

    public Vector3 WorldCenterOfMass => rigidbody.worldCenterOfMass;

    /// <summary>
    /// Triggered once the player dice has stopped moving, i.e. Velocity is 0 and angular momentum is 0.
    /// </summary>
    public UnityEvent<int> OnPlayerStationary;

    private bool inMovement = false;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            // Is it possible to have multiple dice?
            Debug.LogError("There should only be one player dice.");
        }

        CreateVertexSensors();

        OnPlayerStationary = new();
    }


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.maxAngularVelocity = maxAngularVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (!inMovement && !IsStationary())
        {
            inMovement = true;
        }
        else if (inMovement && IsStationary())
        {
            OnPlayerStationary.Invoke(calculateRollResult());
            inMovement = false;
        }
    }

    public void ResetToPosition(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;

        if (rigidbody != null)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
    }

    public bool IsStationary()
    {
        // return rigidbody.velocity.magnitude < 0.001
        //     && rigidbody.angularVelocity.magnitude < 0.001;
        return rigidbody.IsSleeping();
    }

    public void AddForce(Vector3 force)
    {
        rigidbody.AddForce(force, ForceMode.Impulse);
    }

    public void AddTorque(Vector3 torque)
    {
        rigidbody.AddTorque(torque, ForceMode.VelocityChange);
    }

    void CreateVertexSensors()
    {
        BoxCollider diceCollider = GetComponent<BoxCollider>();
        Bounds diceBounds = diceCollider.bounds;
        Vector3 minOffset = diceBounds.min - diceBounds.center;
        Vector3 maxOffset = diceBounds.max - diceBounds.center;

        for (int i = 0; i < 4; i++)
        {
            Vector3 minPos = (Quaternion.Euler(0, 90 * i, 0) * minOffset) + diceBounds.center;
            Instantiate(DiceVertexPrefab, minPos, Quaternion.identity, transform);
            Vector3 maxPos = (Quaternion.Euler(0, 90 * i, 0) * maxOffset) + diceBounds.center;
            Instantiate(DiceVertexPrefab, maxPos, Quaternion.identity, transform);
        }
    }

    int calculateRollResult() {
        var dirList = new List<Vector3> {
            new Vector3(0,1,0),
            new Vector3(-1,0,0),
            new Vector3(0,0,1),
            new Vector3(0,0,-1),
            new Vector3(1,0,0),
            new Vector3(0,-1,0),
        };

        var rotation = transform.rotation;
        var max_no = -1;
        var max_product = -1f;

        for (var i=0; i<6; i++) {
            var product = Vector3.Dot(Vector3.up, (rotation * dirList[i]));
            if (product > max_product) {
                max_product = product;
                max_no = i + 1;
            }
        }

        return max_no;
    }
}
