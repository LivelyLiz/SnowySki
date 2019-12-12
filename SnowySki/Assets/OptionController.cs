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
        ApplyDifficulty(DiffParams);

        SISecondsSlider.onValueChanged.AddListener((val) => SpeedParams.IncreaseEverySec = val);
        SISecondsSlider.value = DefaultSpeedParams.IncreaseEverySec;

        SIAmountSlider.onValueChanged.AddListener((val) => SpeedParams.SpeedIncrement = val);
        SIAmountSlider.value = DefaultSpeedParams.SpeedIncrement;
        
        SIMaxSpeedSlider.onValueChanged.AddListener((val) => SpeedParams.MaximumSpeed = val);
        SIMaxSpeedSlider.value = DefaultSpeedParams.MaximumSpeed;
    }

    public void ApplyDifficulty(DifficultyParameters diffParams)
    {
        //need this because setting the min slider adjusts the Medium probability
        float easy = diffParams.ProbabilityEasy;
        float medium = easy + diffParams.ProbabilityMedium;

        DiffRatioSlider.MaxSlider.value = medium;
        DiffRatioSlider.MinSlider.value = easy;
        DiffRatioSlider.MaxSlider.value = medium;
    }
}
