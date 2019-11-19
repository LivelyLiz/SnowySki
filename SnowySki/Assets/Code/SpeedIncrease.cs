using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedIncrease : MonoBehaviour
{
    public SpeedParameters SpeedParams;

    public bool IncrementSmooth = true;
    public float SmoothIncrementTime = 0.0f;

    private float _startTime = 0;
    private float _timeSinceLastIncrease = 0;

    private void OnEnable()
    {
        _startTime = 0;
        _timeSinceLastIncrease = 0;

        if (IncrementSmooth && SmoothIncrementTime == 0.0f)
        {
            SmoothIncrementTime = SpeedParams.IncreaseEverySec / 10.0f;
        }
    }

    public void Update()
    {
        _timeSinceLastIncrease += Time.deltaTime;
        if (_timeSinceLastIncrease > SpeedParams.IncreaseEverySec)
        {
            _timeSinceLastIncrease = 0;

            if (IncrementSmooth)
            {
                StartCoroutine(coIncreaseSpeedSmooth(SmoothIncrementTime));
            }
            else
            {
                SpeedParams.CurrentSpeed += SpeedParams.SpeedIncrement;
                if (SpeedParams.CurrentSpeed >= SpeedParams.MaximumSpeed)
                {
                    SpeedParams.CurrentSpeed = SpeedParams.MaximumSpeed;
                }
            }
        }
    }

    IEnumerator coIncreaseSpeedSmooth(float smoothingTime)
    {
        float increase = 0;
        float validIncrease = Mathf.Min(SpeedParams.MaximumSpeed - SpeedParams.CurrentSpeed, SpeedParams.SpeedIncrement);

        while (increase < validIncrease)
        {
            float add = SpeedParams.SpeedIncrement / smoothingTime * Time.deltaTime;
            increase += add;
            SpeedParams.CurrentSpeed += add;
            yield return null;
        }
    }
}
