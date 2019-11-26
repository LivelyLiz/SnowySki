using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUpdater : MonoBehaviour, IResetable
{
    public ScoreObject Score;

    public void Update()
    {
        Score.CurrentScore += Time.deltaTime;
    }

    public void Reset()
    {
        Score.CurrentScore = 0;
    }
}
