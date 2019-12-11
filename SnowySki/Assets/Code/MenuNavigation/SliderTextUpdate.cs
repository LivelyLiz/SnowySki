using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTextUpdate : MonoBehaviour
{
    public Text SliderValText;
    public Text SliderMinText;
    public Text SliderMaxText;

    public Slider Slider;
    public string NumberStyle = "n1";

    public void Start()
    {
        SliderMinText.text = Slider.minValue.ToString(NumberStyle);
        SliderMaxText.text = Slider.maxValue.ToString(NumberStyle);
        Slider.onValueChanged.AddListener((val) =>
        {
            SliderValText.text = val.ToString(NumberStyle);
        });
    }
}
