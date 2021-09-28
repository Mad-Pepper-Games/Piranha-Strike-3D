using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class CharacterSkinController : SerializedMonoBehaviour
{   
    public Dictionary<Skin, GameObject> SkinDictionary = new Dictionary<Skin, GameObject>();
    Skin localCurrentSkin = Skin.Default;
    private void OnEnable()
    {
        CharacterSkinManager.Instance.OnSkinChange.AddListener(ChangeSkin);
    }

    private void OnDisable()
    {
        CharacterSkinManager.Instance.OnSkinChange.RemoveListener(ChangeSkin);
    }

    public void ChangeSkin()
    {
        if(localCurrentSkin != Skin.Default)
            SkinDictionary[localCurrentSkin].SetActive(false);

        if (CharacterSkinManager.Instance.CurrentSkin != Skin.Default)
            SkinDictionary[CharacterSkinManager.Instance.CurrentSkin].SetActive(true);

        localCurrentSkin = CharacterSkinManager.Instance.CurrentSkin;
    }
}