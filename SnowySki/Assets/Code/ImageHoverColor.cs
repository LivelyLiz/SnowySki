using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageHoverColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image _img;
    private Color _originalColor;

    public Color HoverColor;

    public void Start()
    {
        _img = GetComponent<Image>();
        _originalColor = _img.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _img.color = HoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _img.color = _originalColor;
    }
}
