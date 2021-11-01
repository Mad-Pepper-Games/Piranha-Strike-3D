using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tabtale.TTPlugins;
public class CLIKManager : Singleton<CLIKManager>
{
    private void Awake()
    {
        TTPCore.Setup();
    }
}
