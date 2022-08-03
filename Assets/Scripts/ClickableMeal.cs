using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class ClickableMeal : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Color mealColor;
    private Action<Color> onClick;
    private Vector3 initialPosition;
    private Vector3 initialScale;
    private bool isChoosen;

    private AudioManager audioManager;

    [Inject]
    private void Construct(AudioManager audioManagerInstance)
    {
        audioManager = audioManagerInstance;
    }

    private void Start()
    {
        initialPosition = gameObject.transform.position;
        initialScale = gameObject.transform.localScale;
        isChoosen = false;
    }

    public void Initialize(Action<Color> onClick)
    {
        this.onClick = onClick;
    }

    public void ResetPosition()
    {
        gameObject.transform.position = initialPosition;
    }

    public void ResetScale()
    {
        gameObject.transform.localScale = initialScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isChoosen)
            return;

        isChoosen = true;
        audioManager.PlaySound(SoundType.Choose);
        onClick?.Invoke(mealColor);
        JumpToMixer();
        DecreaseScale();
    }


    private void JumpToMixer()
    {
        Sequence mealAnimation = DOTween.Sequence();
        mealAnimation.Append(transform.DOJump(new Vector3(0.860000014f, 1.11199999f, -7.46799994f), 0.2f, 1, 1));
        mealAnimation.Append(transform.DOJump(new Vector3(0.860000014f, 0.611199999f, -7.46799994f), 0.2f, 1, 1));
        mealAnimation.onComplete += OnAnimationComplete;
    }

    private void DecreaseScale()
    {
        Sequence mealAnimation = DOTween.Sequence();
        mealAnimation.Append(transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 1));
    }

    private void OnAnimationComplete()
    {
        isChoosen = false;
        gameObject.SetActive(false);
    }
}
