using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Cycle : MonoBehaviour, IResetable
{
    public List<SnapPoints> CycleList;

    [Space]
    public Transform PositionCheckTransform;
    public Axis PositionCheckAxis;
    public bool CheckIfGreater;

    private int _currSegment = 0;

    public void Update()
    {
        if (doesSegmentCycleCondition(CycleList[_currSegment]))
        {
            CycleList[_currSegment].StartSnapPoint.position = CycleList[(_currSegment - 1 + CycleList.Count)%CycleList.Count].EndSnapPoint.position;
            _currSegment = (_currSegment+1)%CycleList.Count;
        }
    }

    public void Reset()
    {
        _currSegment = 0;
        CycleList[_currSegment].StartSnapPoint.position = transform.position;

        for (int i = 1; i < CycleList.Count; i++)
        {
            CycleList[i].StartSnapPoint.position = CycleList[i - 1].EndSnapPoint.position;
        }
    }

    private bool doesSegmentCycleCondition(SnapPoints sp)
    {
        Vector3 currSegStartPos = sp.EndSnapPoint.position;
        Vector3 transfPos = PositionCheckTransform.InverseTransformPoint(currSegStartPos);

        switch (PositionCheckAxis)
        {
            case Axis.None:
                return transfPos == Vector3.zero;
                break;
            case Axis.X:
                return transfPos.x > 0 == CheckIfGreater;
                break;
            case Axis.Y:
                return transfPos.y > 0 == CheckIfGreater;
                break;
            case Axis.Z:
                return transfPos.z > 0 == CheckIfGreater;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
