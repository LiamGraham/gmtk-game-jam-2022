using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotIndicator : MonoBehaviour
{

    public float minLength = 0.5f;
    public float maxLength = 1.5f;
    public float minAlpha = 0f;
    public float maxAlpha = 1f;
    public float initialScale = 0.2f;
    public GameObject player;
    SpriteRenderer spriteRenderer;

    // // float currentLength;
    // // float currentAlpha;
    // // Start is called before the first frame update
    void Start()
    {
        this.transform.rotation = Quaternion.LookRotation(-Vector3.up, Vector3.forward);
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // // Update is called once per frame
    void Update()
    {
        this.setDirection(player.transform.position - this.transform.position);
        this.setPower((player.transform.position - this.transform.position).magnitude/6f);
    }

    public void setPower(float power) {
        //set power as a float from 0 to 1
        Debug.Log(power.ToString());
        power = Mathf.Min(0, power);
        power = Mathf.Max(1, power);
        //set length
        var newLength = minLength + (maxLength - minLength) * power;
        this.transform.localScale = (new Vector3(1, newLength, 1)) * this.initialScale;
        //set alpha
        var newAlpha = (minAlpha + (maxAlpha - minAlpha) * power);
        var newColor = new Color(this.spriteRenderer.color.r, this.spriteRenderer.color.g, this.spriteRenderer.color.b, newAlpha);
        this.spriteRenderer.color = newColor;
    }

    public void setDirection(Vector3 direction) {
        // this.transform.LookAt(target, Vector3.up);
        var goalRotation = Quaternion.LookRotation(direction, Vector3.up);
        var additionalRotation = Quaternion.LookRotation(-Vector3.up, Vector3.forward);
        this.transform.rotation = goalRotation * additionalRotation;
    }
}
