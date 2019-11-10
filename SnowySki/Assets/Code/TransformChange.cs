using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UniRx;

public class TransformChange : MonoBehaviour
{
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    private Vector3 _initialScale;

    public Vector3 PositionDelta { get; private set; }
    public Vector3 ScaleDelta { get; private set; }
    /// <summary>
    /// Delta * transform.rotation = rotation+delta
    /// </summary>
    public Quaternion RotationDelta { get; private set; }

    void Start()
    {
        _initialPosition = transform.localPosition;
        _initialRotation = transform.rotation;
        _initialScale = transform.lossyScale;
        MainThreadDispatcher.StartUpdateMicroCoroutine(coUpdatePositionDelta()); //instead of call StartCoroutine
    }

    IEnumerator coUpdatePositionDelta()
    {
        while (true)
        {
            PositionDelta = transform.localPosition - _initialPosition;
            ScaleDelta = transform.lossyScale - _initialScale;
            RotationDelta = transform.rotation * Quaternion.Inverse(_initialRotation);
            yield return null;
        }
    }
}
