using System;
using System.Collections;
using System.Collections.Generic;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEditor;
using UnityEngine;

[Serializable]
public class KeyDict : SerializableDictionaryBase<string, KeyCode> { }

[Serializable]
public struct KeyboardLayout
{
    public string Name;
    public KeyDict ActionToKey;
}

[CreateAssetMenu(fileName = "New InputMapping", menuName = "ScriptableObjects/InputMapping", order = 1)]
public class InputMapping : ScriptableObject
{
    [HideInInspector]
    public KeyboardLayout CurrentLayout { get; private set; }
    public List<KeyboardLayout> Layouts = new List<KeyboardLayout>();

    private int _currLayout = 0;

    public void OnEnable()
    {
        if (Layouts.Count > 0)
        {
            CurrentLayout = Layouts[0];
        }
    }

    public void NextLayout()
    {
        _currLayout = (_currLayout + 1) % Layouts.Count;
        CurrentLayout = Layouts[_currLayout];
    }
}
