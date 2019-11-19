using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class KeepGlobalScale : MonoBehaviour
{
    private Vector3 _globalScale;

    void Awake()
    {
        _globalScale = transform.lossyScale;
    }

    void Start()
    {
        transform
            .ObserveEveryValueChanged(x => x.lossyScale)
            .DistinctUntilChanged()
            .Subscribe(_ =>
            {
                transform.localScale = Vector3.one;

                Vector3 adjustedScale = Vector3.one;
                adjustedScale.Scale(_globalScale);
                Debug.Log(divideVectors(_globalScale, transform.lossyScale));
                adjustedScale.Scale( divideVectors(_globalScale, transform.lossyScale));

                transform.localScale = adjustedScale;
            })
            .AddTo(this);
    }

    private Vector3 divideVectors(Vector3 nominator, Vector3 denominator)
    {
        return new Vector3(nominator.x/denominator.x, nominator.y/denominator.y, nominator.z / denominator.z);
    }
}
