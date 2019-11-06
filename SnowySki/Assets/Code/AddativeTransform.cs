using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class AddativeTransform : MonoBehaviour
{
    public Transform SourceTransform;
    public TransformChange TransformDelta;

    public bool AddPosition;
    public bool AddRotation;
    public bool AddScale;

    private IDisposable _positionSub;
    private IDisposable _rotationSub;
    private IDisposable _scaleSub;

    public void OnEnable()
    {
        makePositionSub();
        makeRotationSub();
        makeScaleSub();
    }

    public void OnDisable()
    {
        disposeSubs();
    }

    private void disposeSubs()
    {
        if (_positionSub != null)
        {
            _positionSub.Dispose();
        }
        if (_rotationSub != null)
        {
            _rotationSub.Dispose();
        }
        if (_scaleSub != null)
        {
            _scaleSub.Dispose();
        }
    }

    private void makePositionSub()
    {
        if (_positionSub != null)
        {
            _positionSub.Dispose();
        }

        _positionSub = TransformDelta
            .ObserveEveryValueChanged(td =>
            {
                if (td != null && AddPosition)
                {
                    return td.PositionDelta;
                }
                else
                {
                    return Vector3.zero;
                }
            })
            .Subscribe(posD =>
            {
                this.transform.position += transform.rotation *
                                               (Vector3.Scale(
                                                   SourceTransform != null ? SourceTransform.lossyScale : Vector3.one,
                                                   posD));
            })
            .AddTo(this);
    }

    private void makeRotationSub()
    {
        if (_rotationSub != null)
        {
            _rotationSub.Dispose();
        }

        _rotationSub = TransformDelta
            .ObserveEveryValueChanged(td =>
            {
                if (td != null && AddRotation)
                {
                    return td.RotationDelta;
                }
                else
                {
                    return Quaternion.identity;
                }
            })
            .Subscribe(rotD => this.transform.rotation = rotD * transform.rotation)
            .AddTo(this);
    }

    private void makeScaleSub()
    {
        if (_scaleSub != null)
        {
            _scaleSub.Dispose();
        }

        _scaleSub = TransformDelta
            .ObserveEveryValueChanged(td =>
            {
                if (td != null && AddScale)
                {
                    return td.ScaleDelta;
                }
                else
                {
                    return Vector3.one;
                }
            })
            .Subscribe(scaleD => this.transform.localScale = Vector3.Scale(this.transform.localScale, scaleD))
            .AddTo(this);
    }
}
