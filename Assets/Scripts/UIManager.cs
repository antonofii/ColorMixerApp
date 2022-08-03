using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image cocktailImage;
    [SerializeField] private Text resultTitle;
    private LevelManager levelManager;

    [Inject]
    private void Construct(LevelManager levelManagerInstance)
    {
        levelManager = levelManagerInstance;
    }

    private void Start()
    {
        levelManager.onPercentageCalculated += SetMessage;
        levelManager.onColorSet += SetCocktailColor;
    }

    private void SetCocktailColor(Color color)
    {
        color.a = 1;
        cocktailImage.color = color;
    }

    private void SetMessage(string message)
    {
        resultTitle.text = message;
    }
}
