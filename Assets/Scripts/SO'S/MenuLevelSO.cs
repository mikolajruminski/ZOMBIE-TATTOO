using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MenuLevelSO : ScriptableObject
{
    public int unitySceneIndex;

    public string menuSceneName;

    [TextArea(3, 10)]
    public string menuSceneDescription;
    public string enemiesInTheLevel;
    public int numberOfRounds;
    public Sprite levelImage;
}
