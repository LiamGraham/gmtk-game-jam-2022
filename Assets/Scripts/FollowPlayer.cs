using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject m_Player;
    public Vector3 m_OffsetY = new Vector3(0, 0.5f, 0);
    public float m_MinDistance = 3f;
    public float m_InterpolationSpeed = 2f;
    public bool m_EnableSmooth = true;

    void LateUpdate()
    {
        PositionFollowCamera();
    }

    private void PositionFollowCamera()
    {
        float interpolationRatio = Mathf.Min(m_InterpolationSpeed * Time.deltaTime, 1);
        //current player direction
        var playerDirection = m_Player.transform.position - transform.position;
        //goal player direction
        playerDirection.y = 0;
        playerDirection = (playerDirection.normalized) * m_MinDistance;
        var goalPlayerDirection = playerDirection - m_OffsetY;
        //goal rotation
        var goalRotation = Quaternion.LookRotation(goalPlayerDirection, Vector3.up);
        //goal position
        var goalPosition = m_Player.transform.position - goalPlayerDirection;

        // Here we use linear interpolation for smooth camera movement
        transform.position = m_EnableSmooth ? Vector3.Lerp(transform.position, goalPosition, interpolationRatio) : goalPosition;
        transform.rotation = m_EnableSmooth ? Quaternion.Lerp(transform.rotation, goalRotation, interpolationRatio) : goalRotation;
    }

}
