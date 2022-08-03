using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Color> targetColors;
    [SerializeField] private float correctMatchPercentage;
    [SerializeField] private List<Color> colors;
    private Color targetColor;
    private int levelCounter;
    private float result;
    private AudioManager audioManager;

    public event Action onLevelFailed;
    public event Action onLevelChanged;
    public Action<Color> onColorReady;
    public Action<Color> onColorSet;
    public event Action<string> onPercentageCalculated;

    [Inject]
    private void Construct(AudioManager audioManagerInstance)
    {
        audioManager = audioManagerInstance;   
    }

    private void Start()
    {
        LoadNextLevel();
        onColorReady = CheckMatch;
        onLevelChanged = LoadNextLevel;
    }

    private void CheckMatch(Color mixedColor)
    {
        colors.Add(mixedColor);
        float rDifference = Math.Abs(mixedColor.r - targetColor.r);
        float gDifference = Math.Abs(mixedColor.g - targetColor.g);
        float bDifference = Math.Abs(mixedColor.b - targetColor.b);
        float sumDifference = rDifference + gDifference + bDifference;
        result = sumDifference * 100 / 3;
        if (result <= 100 - correctMatchPercentage)
        {
            onLevelChanged?.Invoke();
            audioManager.PlaySound(SoundType.GoodResult);
            onPercentageCalculated?.Invoke((int)(100 - result) + "% - Win");
        }
        else
        {
            onLevelFailed?.Invoke();
            audioManager.PlaySound(SoundType.BadResult);
            onPercentageCalculated?.Invoke((int)(100 - result) + "% - Loose");
        }
        
    }

    private void LoadNextLevel()
    {
        if (levelCounter == targetColors.Count)
            levelCounter = 0;

        targetColor = targetColors[levelCounter];
        levelCounter += 1;
        onColorSet?.Invoke(targetColor);
    }
}
