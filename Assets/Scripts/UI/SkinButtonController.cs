using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using TMPro;
public class SkinButtonController : MonoBehaviour
{
    private Button button;
    public Button Button { get{ return (button == null) ? button = GetComponent<Button>() : button; } }

    public Skin Skin;
    public Image DeactiveImage;
    [OnValueChanged("ChangeActiveState")]
    public bool IsActive;
    public string IDKey;
    public int CoinValue = 500;
    public TextMeshProUGUI SkinText;
    private void Start()
    {
        if(PlayerPrefs.GetInt(IDKey) == 1)
        {
            IsActive = true;
            SkinText.SetText("UNLOCKED");
        }
    }

    private void OnEnable()
    {
        Button.onClick.AddListener(SetSkin);    
    }

    private void OnDisable()
    {
        Button.onClick.RemoveListener(SetSkin);
    }

    public void ChangeActiveState()
    {
        if(IsActive)
            DeactiveImage.gameObject.SetActive(false);
    }

    public void SetSkin()
    {
        if(IsActive)
            CharacterSkinManager.Instance.ChangeSkin(Skin);
        else
        {
            if (PlayerPrefs.GetInt("Coin") > CoinValue)
            {
                PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - CoinValue);
                IsActive = true;
                SkinText.SetText("UNLOCKED");
                CharacterSkinManager.Instance.ChangeSkin(Skin);
                PlayerPrefs.SetInt(IDKey, 1);
            }
        }          
    }
}
