using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class Level
{
    public LevelType LevelType;
    [ValueDropdown("LevelNames")]
    public string LevelID;

#if UNITY_EDITOR
	public List<string> LevelNames
	{
		get
		{
			List<string> levelNames = new List<string>();
			for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
			{
				var level = EditorBuildSettings.scenes[i];

				if (level.path.Contains("Level") || level.path.Contains("Test"))
				{

					int slash = level.path.LastIndexOf('/');
					string name = level.path.Substring(slash + 1);
					name = name.Replace(".unity", string.Empty);
					levelNames.Add(name);
				}
			}
			return levelNames;
		}
	}
#endif
	public Color BackgroundColor = new Color(150,200,255);
}

public enum LevelType
{
    Default
}