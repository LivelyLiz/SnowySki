using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New MaterialProperties", menuName = "ScriptableObjects/MaterialProperties", order = 1)]
public class InstancedMaterialProperties : ScriptableObject
{
    public Vector2 TextureIndex;
    public Color BaseColor;
}
