using UI_System;
using UnityEngine;
using UnityEngine.Events;

public class BackReference : MonoBehaviour
{
    private UI_System.UI_System _uiSystem;
    private UI_Screen _uiScreen;

    public void SetUIReferences(UI_System.UI_System uiSys, UI_Screen uiScr)
    {
        SetUISystem(uiSys);
        SetUIScreen(uiScr);
    }

    public void SetUISystem(UI_System.UI_System uiSys)
    {
        _uiSystem = uiSys;
    }

    public void SetUIScreen(UI_Screen uiScr)
    {
        _uiScreen = uiScr;
    }

    public void GoBack()
    {
        if (_uiSystem != null && _uiScreen != null)
        {
            _uiSystem.SwitchScreen(_uiScreen);
        }
    }
}
