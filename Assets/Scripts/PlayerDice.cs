using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDice : MonoBehaviour
{
    private const int CORNERS = 8;
    public float m_ImpluseForce = 5f;
    public float m_ImplusePosition = 0.05f;
    public bool m_Active = true;
    public float random_dir = 30f;
    public GameObject diceVertexPrefab;
    Rigidbody m_Rigidbody;
    
    private static PlayerDice _instance;
    public static PlayerDice Instance { get { return _instance; } }

    void Awake() {
        if (_instance == null) {
            _instance = this;
        }
        CreateVertexSensors();
    }

    // Start is called before the first frame update
    void Start()
    {
        this.m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.m_Active) {
            if (Input.GetButtonDown("Fire1")) {
                Vector3 ImpulseDir = Quaternion.Euler(Random.Range(-random_dir, random_dir), Random.Range(-random_dir,random_dir), Random.Range(-random_dir,random_dir)) * (Vector3.up * this.m_ImpluseForce);
                //below for add force at position
                Vector3 ImpulsePosition = Quaternion.Euler(Random.Range(-random_dir, random_dir), Random.Range(-random_dir,random_dir), Random.Range(-random_dir,random_dir)) * (Vector3.down * this.m_ImplusePosition);
                this.m_Rigidbody.AddForceAtPosition(ImpulseDir, ImpulsePosition);
                // this.m_Active = false;
            }
        } else {
            if (this.m_Rigidbody.IsSleeping()) {
                this.m_Active = true;
            }
        }
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
