using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SpeedParamters", menuName = "ScriptableObjects/SpeedParameters", order = 1)]
public class SpeedParameters : ScriptableObject
{
    public float InitialSpeed = 1;

    public float IncreaseEverySec = 5;
    public float SpeedIncrement = 0.5f;

    public float CurrentSpeed;

    public float MaximumSpeed = 10;

    void Awake()
    {
        Reset();
    }

    void OnEnable()
    {
        Reset();
    }

    void OnDestroy()
    {
        Reset();
    }

    public void Reset()
    {
        CurrentSpeed = InitialSpeed;
    }
}
