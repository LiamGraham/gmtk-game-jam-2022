using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;

    // Following Camera Settings
    public Vector3 offset = new Vector3(0, 3, -15);
    public Vector3 rotationOffset = new Vector3(10, 0, 0);

    // Smooth camera settings
    public float interpolationSpeed = 0.05f;
    public bool enableSmooth = true;

    private Vector3 rotationMultiplier = new Vector3(1, 1, 0);

    void LateUpdate()
    {
        PositionFollowCamera();
    }

    private void PositionFollowCamera()
    {
        float interpolationRatio = Mathf.Min(interpolationSpeed * Time.deltaTime, 1);

        // Multiply by the rotation multiplier (to set z to 0)
        var playerRotationAngles = Vector3.Scale(player.transform.rotation.eulerAngles, rotationMultiplier);
        // Calculate desired rotation of camera
        var desiredRotation = CalculateDesiredRotation(playerRotationAngles, rotationOffset);
        var desiredOffset = CalculateDesiredOffset(playerRotationAngles, offset);


        // Here we use linear interpolation for smooth camera movement
        transform.position = enableSmooth ? Vector3.Lerp(transform.position, desiredOffset, interpolationRatio) : desiredOffset;
        transform.rotation = enableSmooth ? Quaternion.Lerp(transform.rotation, desiredRotation, interpolationRatio) : desiredRotation;
    }

    private Quaternion CalculateDesiredRotation(Vector3 playerRotationAngles, Vector3 rotationOffset)
    {
        // Calculate desired rotation of camera
        return Quaternion.Euler(playerRotationAngles + rotationOffset);
    }

    private Vector3 CalculateDesiredOffset(Vector3 playerRotationAngles, Vector3 cameraOffset)
    {
        // Calculate rotation offset for camera based on offset
        var rotatedOffset = Quaternion.Euler(playerRotationAngles) * cameraOffset;
        return player.transform.position + rotatedOffset;
    }
}
