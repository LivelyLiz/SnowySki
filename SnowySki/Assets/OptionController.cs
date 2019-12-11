using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionController : MonoBehaviour
{
    public DifficultyParameters DiffParams;
    public MinMaxSliderController DiffRatioSlider;

    [Space]
    public SpeedParameters DefaultSpeedParams;
    public SpeedParameters SpeedParams;

    public Slider SISecondsSlider;
    public Slider SIAmountSlider;
    public Slider SIMaxSpeedSlider;

    public void Awake()
    {
        DiffRatioSlider.AddValueChangeListener((minVal, maxVal) =>
        {
            DiffParams.SetValuesAsAccumulate(minVal, maxVal);
        });
    }

    public void Start()
    {
        float easy = DiffParams.ProbabilityEasy;
        float medium = DiffParams.ProbabilityMedium;
        
        DiffRatioSlider.MinSlider.value = easy;
        DiffRatioSlider.MaxSlider.value = easy+medium;

        SISecondsSlider.value = SpeedParams.IncreaseEverySec;
        SISecondsSlider.onValueChanged.AddListener((val) => SpeedParams.IncreaseEverySec = val);
        SIAmountSlider.value = SpeedParams.SpeedIncrement;
        SIAmountSlider.onValueChanged.AddListener((val) => SpeedParams.SpeedIncrement = val);
        SIMaxSpeedSlider.value = SpeedParams.MaximumSpeed;
        SIMaxSpeedSlider.onValueChanged.AddListener((val) => SpeedParams.MaximumSpeed = val);
    }
}
