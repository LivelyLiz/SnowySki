using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToCam : MonoBehaviour
{
    void Update()
    {
        Vector3 forward = Camera.main.transform.position - transform.position;
        forward.y = 0;
        transform.forward = -forward;
    }
}
