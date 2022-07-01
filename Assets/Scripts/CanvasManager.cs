using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [Header("Player")]
    public PlayerCharacter player;

    [Header("Sliders")]
    public Slider healthBar;
    public Slider staminaBar;
    public Slider hungerBar;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = player.health;
        staminaBar.value = player.stamina;
        hungerBar.value = player.hunger;
    }
}
