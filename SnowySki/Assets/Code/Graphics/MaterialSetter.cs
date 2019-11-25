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
        _meshRenderer.GetPropertyBlock(_matPropBlock);
    }

    void Start()
    {
        _matPropBlock.SetColor("_Color", InstMatProp.BaseColor);
        _matPropBlock.SetVector("_TextureIndex", new Vector4(InstMatProp.TextureIndex.x, InstMatProp.TextureIndex.y, 0.0f, 0.0f));
        _meshRenderer.SetPropertyBlock(_matPropBlock);
    }

    void Update()
    {
        /*_matPropBlock.SetColor("_Color", InstMatProp.BaseColor);
        _matPropBlock.SetVector("_TextureIndex", InstMatProp.TextureIndex);
        _meshRenderer.SetPropertyBlock(_matPropBlock);*/
    }
}
