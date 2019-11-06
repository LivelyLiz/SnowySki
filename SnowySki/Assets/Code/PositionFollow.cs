using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionFollow : MonoBehaviour
{
    [SerializeField]
    protected Transform _parent;
    protected Vector3 _localOffset = Vector3.zero;

    public void Start()
    {
        calculateOffset();
    }

    public void Update()
    {
        transform.position = _parent.position + _localOffset;
    }

    public Transform GetParent()
    {
        return _parent;
    }

    public void SetParent(Transform parent)
    {
        _parent = parent;
        calculateOffset();
    }

    protected void calculateOffset()
    {
        if (_parent != null)
        {
            _localOffset = this.transform.position - _parent.position;
        }
    }
}
