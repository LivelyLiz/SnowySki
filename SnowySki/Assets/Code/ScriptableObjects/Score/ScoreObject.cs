using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ScoreObject", menuName = "ScriptableObjects/ScoreObject", order = 1)]
public class ScoreObject : ScriptableObject
{
    public float CurrentScore;
    public float WinScore;

    public void Reset()
    {
        CurrentScore = 0;
    }
}
