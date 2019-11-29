using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class ShowLayout : MonoBehaviour
{
    public InputMapping JumpKeys;

    public Text LayoutName;
    public Text Top;
    public Text Middle;
    public Text Bottom;

    public void Start()
    {
        Observable.EveryUpdate().Select(_ => JumpKeys.CurrentLayout).DistinctUntilChanged()
            .Subscribe(layout => showLayout(layout)).AddTo(this);
    }

    public void ShowNextLayout()
    {
        JumpKeys.NextLayout();
    }

    private void showLayout(KeyboardLayout layout)
    {
        LayoutName.text = layout.Name;
        Top.text = layout.ActionToKey["top"].ToString();
        Middle.text = layout.ActionToKey["middle"].ToString();

        if (layout.Name.Equals("NEO"))
        {
            Bottom.text = "Ü";
        }
        else
        {
            Bottom.text = layout.ActionToKey["bottom"].ToString();
        }
    }
}
