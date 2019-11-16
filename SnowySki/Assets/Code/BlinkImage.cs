using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BlinkImage : MonoBehaviour
{
    public Image Image1;
    public Image Image2;

    public float Interval;

    public void OnEnable()
    {
        if (Image1 == null || Image2 == null)
        {
            return;
        }

        StartCoroutine(blink());
    }

    public void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator blink()
    {
        WaitForSeconds wfs = new WaitForSeconds(Interval);
        Image1.enabled = true;
        Image2.enabled = false;
        while (true)
        {
            Image1.enabled = !Image1.enabled;
            Image2.enabled = !Image2.enabled;
            yield return wfs;
        }
    }
}
