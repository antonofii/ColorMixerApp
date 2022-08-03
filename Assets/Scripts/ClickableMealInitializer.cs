using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ClickableMealInitializer : MonoBehaviour
{
    [SerializeField] List<ClickableMeal> meals;
    private Action<Color> onMealChoosen;
    private ColorMixer colorMixer;

    [Inject]
    private void Construct(ColorMixer colorMixerInstance)
    {
        colorMixer = colorMixerInstance;
    } 

    private void Start()
    {
        onMealChoosen = OnMealChoosen;
        InitializeMeals();
    }

    private void OnEnable()
    {
        colorMixer.onColorsMixed += RespawnAllMeals;
    }

    private void OnDisable()
    {
        colorMixer.onColorsMixed -= RespawnAllMeals;
    }

    private void InitializeMeals()
    {
        foreach (var meal in meals)
            meal.Initialize(onMealChoosen);
    }

    private void OnMealChoosen(Color mealColor)
    {
        colorMixer.AddColor(mealColor);
        colorMixer.OpenMixer();
    }

    private void RespawnAllMeals() 
    {
        foreach (var meal in meals)
        {
            meal.ResetPosition();
            meal.ResetScale();
            meal.gameObject.SetActive(true);
        }
    }
}
