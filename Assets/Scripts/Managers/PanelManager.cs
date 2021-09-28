using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : Singleton<PanelManager>
{
    public List<BasePanel> Panels = new List<BasePanel>();
}
