using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
public class CharacterSkinManager : Singleton<CharacterSkinManager>
{
    public UnityEvent OnSkinChange = new UnityEvent();

    public Skin CurrentSkin;

    [Button]
    public void ChangeSkin(Skin skin)
    {
        CurrentSkin = skin;
        OnSkinChange.Invoke();
    }
}
public enum Skin
{
    Default,
    Cook,
    Trashbin
}