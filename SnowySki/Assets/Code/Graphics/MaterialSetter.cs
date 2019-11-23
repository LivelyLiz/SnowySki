using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class MaterialSetter : MonoBehaviour
{
    public InstancedMaterialProperties InstMatProp;
    private MeshRenderer _meshRenderer;
    private MaterialPropertyBlock _matPropBlock;

    void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _matPropBlock = new MaterialPropertyBlock();
    }

    void Update()
    {
        _matPropBlock.SetColor("_Color", InstMatProp.BaseColor);
        _matPropBlock.SetVector("_TextureIndex", InstMatProp.TextureIndex);
        _meshRenderer.SetPropertyBlock(_matPropBlock);
    }
}
