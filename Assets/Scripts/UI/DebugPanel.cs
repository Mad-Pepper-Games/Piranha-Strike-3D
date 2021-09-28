using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugPanel : BasePanel
{
    public Button DebugButton;
    public BasePanel DebugSettingsPanel;

    private bool IsActivated;
    private void OnEnable()
    {
        DebugButton.onClick.AddListener(ActivateDebugs);
    }
    private void OnDisable()
    {
        DebugButton.onClick.RemoveListener(ActivateDebugs);
    }

    private void ActivateDebugs()
    {
        IsActivated = !IsActivated;
        if (IsActivated)
            DebugSettingsPanel.Activate();
        else
            DebugSettingsPanel.Deactivate();
    }
}
