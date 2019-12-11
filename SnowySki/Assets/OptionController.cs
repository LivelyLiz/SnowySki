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

        SISecondsSlider.value = DefaultSpeedParams.IncreaseEverySec;
        SISecondsSlider.onValueChanged.AddListener((val) => SpeedParams.IncreaseEverySec = val);
        SIAmountSlider.value = DefaultSpeedParams.SpeedIncrement;
        SIAmountSlider.onValueChanged.AddListener((val) => SpeedParams.SpeedIncrement = val);
        SIMaxSpeedSlider.value = DefaultSpeedParams.MaximumSpeed;
        SIMaxSpeedSlider.onValueChanged.AddListener((val) => SpeedParams.MaximumSpeed = val);
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
