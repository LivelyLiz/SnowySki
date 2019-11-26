using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EnvironmentMove : MonoBehaviour, IResetable
{
    public SpeedParameters SpeedParams;
    public Axis MoveAxis;
    public Transform MoveTransform;
    public bool InvertDirection;

    void Update()
    {
        transform.Translate( (InvertDirection ? -1 : 1) * getGlobalMovementVector() * Time.smoothDeltaTime * SpeedParams.CurrentSpeed);
    }

    public void Reset()
    {
        transform.position = Vector3.zero;
    }

    Vector3 getGlobalMovementVector()
    {
        switch (MoveAxis)
        {
            case Axis.None:
                return Vector3.zero;
            case Axis.X:
                return MoveTransform.right;
            case Axis.Y:
                return MoveTransform.up;
            case Axis.Z:
                return MoveTransform.forward;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
