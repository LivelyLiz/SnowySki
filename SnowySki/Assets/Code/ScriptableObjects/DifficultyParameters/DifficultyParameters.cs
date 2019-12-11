using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DiffcultyParameters", menuName = "ScriptableObjects/DiffcultyParameters", order = 1)]
public class DifficultyParameters : ScriptableObject
{
    [SerializeField]
    private float _probEasy_default;
    [SerializeField]
    private float _probMedium_default;

    private float _probEasy;
    private float _probMedium;

    void OnEnable()
    {
        RevertToDefault();
        Debug.Log(ProbabilityEasy);
        Debug.Log(ProbabilityMedium);
        Debug.Log(ProbabilityHard);
    }

    public float ProbabilityEasy
    {
        get { return _probEasy; }
        set
        {
            makeConsistent(value, _probMedium);
        }
    }

    public float ProbabilityMedium
    {
        get { return _probMedium; }
        set { makeConsistent(_probEasy, value);}
    }

    public float ProbabilityHard
    {
        get { return 1 - _probEasy - _probMedium; }
    }

    public void SetValuesAsAccumulate(float probEasy, float probMiddle)
    {
        if (probEasy > probMiddle)
        {
            probMiddle = probEasy;
        }
        makeConsistent(probEasy, probMiddle - probEasy);
    }

    public void RevertToDefault()
    {
        makeConsistent(_probEasy_default, _probMedium_default);
    }

    private void makeConsistent(float probEasy, float probMiddle)
    {
        if (probEasy > 1)
        {
            _probEasy = 1;
            _probMedium = 0;
            return;
        }

        if (probMiddle > 1)
        {
            _probEasy = 0;
            _probMedium = 1;
            return;
        }

        if (probEasy < 0)
        {
            probEasy = 0;
        }

        if (probMiddle < 0)
        {
            probMiddle = 0;
        }
        
        _probEasy = probEasy;
        if (probEasy + probMiddle > 1)
        {
            _probMedium = 1 - _probEasy;
        }
        else
        {
            _probMedium = probMiddle;
        }
    }
}
