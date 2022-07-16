using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDice : MonoBehaviour
{
    public float m_ImpluseForce = 5f;
    public float m_ImplusePosition = 0.05f;
    public GameObject diceVertexPrefab;
    Rigidbody m_Rigidbody;
    
    private static PlayerDice _instance;
    public static PlayerDice Instance { get { return _instance; } }

    public Vector3 worldCenterOfMass {get {return m_Rigidbody.worldCenterOfMass; }}

    void Awake() {
        if (_instance == null) {
            _instance = this;
        }
        CreateVertexSensors();
    }

    public static UnityEvent PlayerStationary = new();

    // Start is called before the first frame update
    void Start()
    {
        this.m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsStationary()) {
            PlayerStationary.Invoke();
        }
    }

    public bool IsStationary()
    {
        return m_Rigidbody.velocity.magnitude < 0.01 
            && m_Rigidbody.angularVelocity.magnitude < 0.01;
    }

    public void addForce(Vector3 force) {
        m_Rigidbody.AddForce(force, ForceMode.Impulse);
    }

    public void addTorque(Vector3 torque) {
        m_Rigidbody.AddTorque(torque, ForceMode.Impulse);
    }

    void CreateVertexSensors() {
        BoxCollider diceCollider = GetComponent<BoxCollider>(); 
        Bounds diceBounds = diceCollider.bounds;
        Vector3 minOffset = diceBounds.min - diceBounds.center;
        Vector3 maxOffset = diceBounds.max - diceBounds.center;

        for (int i = 0; i < 4; i++) {
            Vector3 minPos = (Quaternion.Euler(0, 90 * i, 0) * minOffset) + diceBounds.center;
            Instantiate(diceVertexPrefab, minPos, Quaternion.identity, this.transform);
            Vector3 maxPos = (Quaternion.Euler(0, 90 * i, 0) * maxOffset) + diceBounds.center;
            Instantiate(diceVertexPrefab, maxPos, Quaternion.identity, this.transform);
        }
    }
}
