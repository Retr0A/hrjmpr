using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Graphics Settings")]
    public SettingsObject settingsObject;
    public GameObject postProcessObject;

    // Start is called before the first frame update
    void Start()
    {
        postProcessObject.SetActive(settingsObject.shouldUsePostProcess);
        QualitySettings.shadows = settingsObject.shouldUseShadows ? ShadowQuality.All : ShadowQuality.Disable;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
