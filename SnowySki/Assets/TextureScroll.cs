using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScroll : MonoBehaviour
{
    public MeshRenderer MeshRend;
    public Vector2 ScrollDir;
    public SpeedParameters SpeedParams;

    void Start()
    {
        if (MeshRend == null)
        {
            MeshRend = GetComponent<MeshRenderer>();
        }
    }

    void Update()
    {
        MeshRend.material.mainTextureOffset += ScrollDir * SpeedParams.CurrentSpeed;
    }
}
