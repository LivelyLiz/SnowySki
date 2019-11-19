using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ScaleBySpeed : MonoBehaviour
{
    public SpeedParameters SpeedParams;

    public float ScaleRatio = 1;

    public bool ScaleX = true;
    public bool ScaleY = true;
    public bool ScaleZ = true;

    public void OnEnable()
    {
        transform.localScale = new Vector3(ScaleX ? 1 : 0, ScaleY ? 1 : 0, ScaleZ ? 1 : 0) * SpeedParams.CurrentSpeed * ScaleRatio
                               + new Vector3(!ScaleX ? 1 : 0, !ScaleY ? 1 : 0, !ScaleZ ? 1 : 0);
    }
}
