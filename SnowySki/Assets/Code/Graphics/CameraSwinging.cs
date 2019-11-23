using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CameraSwinging : MonoBehaviour
{
    public float Amplitude;
    public float Frequency;
    public Axis MovementAxis;

    private Vector3 _basePos;

    void Awake()
    {
        _basePos = transform.position;
    }

    void Update()
    {
        float dirScale = Mathf.Sin(Time.time*Frequency) * Amplitude;
        transform.position = _basePos + getMovementVector() * dirScale;
    }

    Vector3 getMovementVector()
    {
        switch (MovementAxis)
        {
            case Axis.None:
                return Vector3.zero;
            case Axis.X:
                return transform.right;
            case Axis.Y:
                return transform.up;
            case Axis.Z:
                return transform.forward;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
