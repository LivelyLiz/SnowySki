using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New JumpParamters", menuName = "ScriptableObjects/JumpParameters", order = 1)]
public class JumpParameters : ScriptableObject
{
    public float JumpVelocity = 1f;
    public float FallMultiplier = 1.1f;

    public float Gravity = 1f;
}
