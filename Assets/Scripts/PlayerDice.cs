using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDice : MonoBehaviour
{

    public float m_ImpluseForce = 5f;
    public float m_ImplusePosition = 0.05f;
    public bool m_Active = true;
    public float random_dir = 30f;
    Rigidbody m_Rigidbody;

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
}
