using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossHp : MonoBehaviour
{

    [SerializeField] private Slider slider;
    //This is a slider for the Boss's HP bar
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }    
    public void SetHealth(int health)
    {
        slider.value = health;
    }

  
}
