using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDice : MonoBehaviour
{
    public float ImpluseForce = 5f;
    public float ImplusePosition = 0.05f;
    public GameObject DiceVertexPrefab;

    new Rigidbody rigidbody;

    private static PlayerDice _instance;
    public static PlayerDice Instance { get { return _instance; } }

    public Vector3 WorldCenterOfMass => rigidbody.worldCenterOfMass;

    /// <summary>
    /// Triggered once the player dice has stopped moving, i.e. Velocity is 0 and angular momentum is 0.
    /// </summary>
    public UnityEvent OnPlayerStationary;

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
            OnPlayerStationary.Invoke();
            inMovement = false;
        }
    }

    public void ResetToPosition(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;

        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }

    public bool IsStationary()
    {
        return rigidbody.velocity.magnitude < 0.001
            && rigidbody.angularVelocity.magnitude < 0.001;
    }

    public void AddForce(Vector3 force)
    {
        rigidbody.AddForce(force, ForceMode.Impulse);
    }

    public void AddTorque(Vector3 torque)
    {
        rigidbody.AddTorque(torque, ForceMode.Impulse);
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
}
