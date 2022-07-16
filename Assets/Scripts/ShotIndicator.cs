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

    // // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.LookRotation(-Vector3.up, Vector3.forward);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // // Update is called once per frame
    void Update()
    {
        setDirection(player.transform.position - transform.position);
        setPower((player.transform.position - transform.position).magnitude/6f);
    }

    public void setPower(float power) {
        //set power bounds from 0 to 1
        power = Mathf.Max(0, power);
        power = Mathf.Min(1, power);

        //set length
        var newLength = minLength + (maxLength - minLength) * power;
        transform.localScale = (new Vector3(1, newLength, 1)) * initialScale;
        //set alpha
        var newAlpha = (minAlpha + (maxAlpha - minAlpha) * power);
        var newColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
        spriteRenderer.color = newColor;
    }

    public void setDirection(Vector3 direction) {
        var goalRotation = Quaternion.LookRotation(direction, Vector3.up);
        var additionalRotation = Quaternion.LookRotation(-Vector3.up, Vector3.forward);
        transform.rotation = goalRotation * additionalRotation;
    }
}
