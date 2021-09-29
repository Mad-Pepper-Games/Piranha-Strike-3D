using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class CharacterSkinController : SerializedMonoBehaviour
{
    public void Start()
    {
        CharacterSkinManager.Instance.ChangeSkin(CharacterSkinManager.Instance.CurrentSkin);
    }

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
      SkinDictionary[localCurrentSkin].SetActive(false);
      SkinDictionary[CharacterSkinManager.Instance.CurrentSkin].SetActive(true);

        localCurrentSkin = CharacterSkinManager.Instance.CurrentSkin;
    }
}