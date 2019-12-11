using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinMaxSliderController : MonoBehaviour
{
    public Slider MinSlider;
    public Slider MaxSlider;

    public RectTransform MinSliderHandle;
    public RectTransform MaxSliderHandle;

    public Text MinValue;
    public Text MaxValue;

    [SerializeField]
    public ValueStruct OverallMin;
    [SerializeField]
    public ValueStruct OverallMax;

    public RectTransform Fill;

    public Slider.Direction SlideDirection = Slider.Direction.BottomToTop;

    public delegate void ValueChangeHandler(float minVal, float maxVal);

    private event ValueChangeHandler m_onValueChange;

	// Use this for initialization
	void Start ()
	{
	    MinSlider.maxValue = OverallMax.Value;
	    MaxSlider.maxValue = OverallMax.Value;

	    MinSlider.minValue = OverallMin.Value;
	    MaxSlider.minValue = OverallMin.Value;

	    MaxSlider.value = OverallMax.Value;
	    MinSlider.value = OverallMin.Value;

        OverallMin.ValueText.text = OverallMin.Value.ToString();
        OverallMax.ValueText.text = OverallMax.Value.ToString();

	    MinValue.text = MinSlider.value.ToString();
        MaxValue.text = MaxSlider.value.ToString();
    }

    public void AdjustFill()
    {
        switch (SlideDirection)
        {
            case Slider.Direction.LeftToRight:
                Fill.anchorMin = new Vector2(MinSlider.normalizedValue, 0);
                break;
            case Slider.Direction.RightToLeft:
                Fill.anchorMin = new Vector2(1-MinSlider.normalizedValue, 0);
                break;
            case Slider.Direction.BottomToTop:
                Fill.anchorMin = new Vector2(0, MinSlider.normalizedValue);
                break;
            case Slider.Direction.TopToBottom:
                Fill.anchorMin = new Vector2(0, 1- MinSlider.normalizedValue);
                break;
            default:
                break;
        }
        
    }

    public void AdjustAnchor()
    {
        switch (SlideDirection)
        {
            case Slider.Direction.LeftToRight:
                MinSliderHandle.anchorMin = new Vector2(MinSlider.normalizedValue, 0);
                MinSliderHandle.anchorMax = new Vector2(MinSlider.normalizedValue, 1);
                break;
            case Slider.Direction.RightToLeft:
                break;
            case Slider.Direction.BottomToTop:
                MinSliderHandle.anchorMin = new Vector2(0, MinSlider.normalizedValue);
                MinSliderHandle.anchorMax = new Vector2(1, MinSlider.normalizedValue);
                break;
            case Slider.Direction.TopToBottom:
                break;
            default:
                break;
        }
    }

    // adjust the value of the min slider
    // it cannot have a higher value as the max slider
    // triggers the value changed event
    public void AdjustMinValue()
    {
        float val = MinSlider.value;

        if (val > MaxSlider.value)
        {
            val = MaxSlider.value;
            MinSlider.value = val;
        }

        AdjustAnchor();
        AdjustFill();

        if (MinValue != null)
        {
            MinValue.text = val.ToString();
        }

        if (m_onValueChange != null)
        {
            m_onValueChange.Invoke(MinSlider.value, MaxSlider.value);
        }
    }

    // adjust the value of the max slider
    // it cannot have a smaller value as the min slider
    // triggers the value changed event
    public void AdjustMaxValue()
    {
        float val = MaxSlider.value;

        if (val < MinSlider.value)
        {
            val = MinSlider.value;
            MaxSlider.value = val;
        }

        AdjustAnchor();
        AdjustFill();

        if (MaxValue != null)
        {
            MaxValue.text = val.ToString();
        }

        if (m_onValueChange != null)
        {
            m_onValueChange.Invoke(MinSlider.value, MaxSlider.value);
        }
    }

    // set the value in the min and max slider and the textfields
    public void SetOverallMinMax(float min, float max)
    {
        OverallMin.Value = min;
        OverallMax.Value = max;

        MinSlider.maxValue = OverallMax.Value;
        MaxSlider.maxValue = OverallMax.Value;

        MinSlider.minValue = OverallMin.Value;
        MaxSlider.minValue = OverallMin.Value;

        MaxSlider.value = OverallMax.Value;
        MinSlider.value = OverallMin.Value;

        OverallMin.ValueText.text = OverallMin.Value.ToString();
        OverallMax.ValueText.text = OverallMax.Value.ToString();

        MinValue.text = MinSlider.value.ToString();
        MaxValue.text = MaxSlider.value.ToString();
    }

    //set how the fill of the slider should look like
    public void SetFillColorScale(Texture tex)
    {
        if (tex == null)
        {
            Fill.GetComponentInChildren<Image>().sprite = null;
        }
        else
        {
            Fill.GetComponentInChildren<Image>().sprite = Sprite.Create((Texture2D)tex, new Rect(Vector2.zero, new Vector2(tex.width, tex.height)), new Vector2(0.5f, 0.5f));
        }
    }

    //add a listener to the event which is triggered if the value of the min or max slider changes
    public void AddValueChangeListener(ValueChangeHandler handlerFunction)
    {
        m_onValueChange += handlerFunction;
    }
}

[Serializable]
public struct ValueStruct
{
    public float Value;
    public Text ValueText;
}