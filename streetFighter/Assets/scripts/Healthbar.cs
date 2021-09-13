﻿using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    //met la vie du joueur au max
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    //modifie la vie du joueur en fonction de la variable reçue
    public void takeDamage(int health)
    {
        slider.value -= health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}