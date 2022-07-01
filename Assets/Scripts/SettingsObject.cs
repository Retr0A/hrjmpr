using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Settings Object", menuName = "Settings Object")]
public class SettingsObject : ScriptableObject
{
    [Header("Effects")]
    public bool shouldUsePostProcess;

    [Header("Shadows")]
    public bool shouldUseShadows;

    [Header("Display")]
    public bool shouldUseFullscreen;
    public bool shouldShowFPS;
}
