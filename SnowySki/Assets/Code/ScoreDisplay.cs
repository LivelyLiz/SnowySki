using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class ScoreDisplay : MonoBehaviour
{
    public ScoreObject Score;
    public Text ScoreText;

    public void Start()
    {
        Observable.EveryUpdate().Select(_ => Score.CurrentScore).Subscribe(score => ScoreText.text = score.ToString("n0")).AddTo(this);
    }
}
