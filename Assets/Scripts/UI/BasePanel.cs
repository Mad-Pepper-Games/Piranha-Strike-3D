using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using DG.Tweening;

public class BasePanel : MonoBehaviour
{
    public bool IsActive;

    private CanvasGroup canvasGroup;
    public CanvasGroup CanvasGroup { get { return (canvasGroup == null) ? canvasGroup = GetComponent<CanvasGroup>() : canvasGroup; } }

    [HideInInspector]
    public UnityEvent OnPanelActivated = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnPanelDeactivated = new UnityEvent();

    public PanelOpenAnimationType OpenPanelAnimationType;
    public PanelCloseAnimationType ClosePanelAnimationType;
    public float AnimationDuration = 1f;
    private void OnEnable()
    {
        PanelManager.Instance.Panels.Add(this);
    }

    private void OnDisable()
    {
        PanelManager.Instance.Panels.Remove(this);
    }
    
    [Button]
    public void Activate()
    {
        

        CanvasGroup.alpha = 1;

        if(OpenPanelAnimationType == PanelOpenAnimationType.Grow)
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, AnimationDuration).OnComplete(()=> {
                CanvasGroup.interactable = true;
                CanvasGroup.blocksRaycasts = true;

                if (!IsActive)
                    OnPanelActivated.Invoke();
                IsActive = true;

            });
        }else if(OpenPanelAnimationType == PanelOpenAnimationType.None)
        {
            CanvasGroup.interactable = true;
            CanvasGroup.blocksRaycasts = true;
            transform.localScale = Vector3.one;
            if (!IsActive)
                OnPanelActivated.Invoke();
            IsActive = true;

        }
    }

    [Button]
    public void Deactivate()
    {
        
        
        CanvasGroup.interactable = false;
        CanvasGroup.blocksRaycasts = false;

        if (ClosePanelAnimationType == PanelCloseAnimationType.Shrink)
        {
            transform.localScale = Vector3.one;
            transform.DOScale(Vector3.zero, AnimationDuration).OnComplete(() => {
                if (IsActive)
                    OnPanelDeactivated.Invoke();
                IsActive = false;
                CanvasGroup.alpha = 0;
            });
        }else if (ClosePanelAnimationType == PanelCloseAnimationType.None)
        {
            transform.localScale = Vector3.one;
            if (IsActive)
                OnPanelDeactivated.Invoke();
            IsActive = false;
            CanvasGroup.alpha = 0;
        }
    }
}
public enum PanelOpenAnimationType
{
    None,
    Grow
}
public enum PanelCloseAnimationType
{
    None,
    Shrink
}