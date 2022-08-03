using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class ColorMixer : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject mixerLid;
    [SerializeField] private GameObject cylinder;
    [SerializeField] private MaterialColorController mixerColor;
    [SerializeField] private Color defaultMixerColor;
    private List<Color> addedColors = new();
    private AudioManager audioManager;
    private LevelManager levelManager;
    private bool mixerWorking;
    private bool isOpened;
    private Color mixedColor;

    public Action onColorsMixed;

    [Inject]
    private void Construct(AudioManager audioManagerInstance, LevelManager levelManagerInstance)
    {
        audioManager = audioManagerInstance;
        levelManager = levelManagerInstance;
    }

    public void AddColor(Color color)
    {
        if (!addedColors.Contains(color))
            addedColors.Add(color);
    }

    public void ResetMixerColors()
    {
        addedColors.Clear();
    }

    public void OpenMixer()
    {
        if (isOpened)
            return;

        isOpened = true;
        Sequence mixerLidAnimation = DOTween.Sequence();
        mixerLidAnimation.Append(mixerLid.transform.DOJump(mixerLid.transform.position, 0.5f, 1, 2));
        mixerLidAnimation.onComplete += () => isOpened = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        StartMixing();
    }

    private void StartMixing()
    {
        if (addedColors.Count < 2 && !mixerWorking)
        {
            audioManager.PlaySound(SoundType.BadResult);
            return;
        }
        else
        {
            StartCoroutine(StartMixer());
        }
    }

    private IEnumerator StartMixer()
    {
        mixerWorking = true;
        audioManager.PlaySound(SoundType.Blender);
        MixColors();

        AnimateMixerBody();
        AnimateCylinder();
        yield return new WaitForSeconds(2);

        onColorsMixed?.Invoke();
        mixerColor.SetColor(mixedColor);
        mixerWorking = false;
        levelManager.onColorReady.Invoke(mixedColor);
        ResetMixerColors();
    }

    private void MixColors()
    {
        mixedColor = new Color(0, 0, 0, 1);
        mixerColor.SetColor(mixedColor);

        foreach (Color color in addedColors)
            mixedColor += color;

        mixedColor /= addedColors.Count;
        mixedColor.a = 255;
    }

    private void AnimateMixerBody()
    {
        Sequence mixerAnimation = DOTween.Sequence();
        mixerAnimation.Append(transform.DOMove(new Vector3(0.860000014f, 0.474000007f, -7.46799994f), 0.2f));
        mixerAnimation.Append(transform.DOMove(new Vector3(0.840000014f, 0.474000007f, -7.46799994f), 0.2f));
        mixerAnimation.Append(transform.DOMove(new Vector3(0.860000014f, 0.474000007f, -7.46799994f), 0.2f));
        mixerAnimation.Append(transform.DOMove(new Vector3(0.840000014f, 0.474000007f, -7.46799994f), 0.2f));
        mixerAnimation.Append(transform.DOMove(new Vector3(0.860000014f, 0.474000007f, -7.46799994f), 0.2f));
        mixerAnimation.Append(transform.DOMove(new Vector3(0.840000014f, 0.474000007f, -7.46799994f), 0.2f));
        mixerAnimation.Append(transform.DOMove(new Vector3(0.870000014f, 0.474000007f, -7.46799994f), 0.2f));
        mixerAnimation.Append(transform.DOMove(new Vector3(0.840000014f, 0.474000007f, -7.46799994f), 0.2f));
        mixerAnimation.Append(transform.DOMove(new Vector3(0.880000014f, 0.474000007f, -7.46799994f), 0.2f));
        mixerAnimation.Append(transform.DOMove(new Vector3(0.860000014f, 0.474000007f, -7.46799994f), 0.2f));
    }

    private void AnimateCylinder()
    {
        Sequence cylinderAnimation = DOTween.Sequence();
        cylinderAnimation.Append(cylinder.transform.DOScaleY(0.56f, 0.2f));
        cylinderAnimation.Append(cylinder.transform.DOScaleY(0.46f, 0.2f));
        cylinderAnimation.Append(cylinder.transform.DOScaleY(0.56f, 0.2f));
        cylinderAnimation.Append(cylinder.transform.DOScaleY(0.56f, 0.2f));
        cylinderAnimation.Append(cylinder.transform.DOScaleY(0.46f, 0.2f));
        cylinderAnimation.Append(cylinder.transform.DOScaleY(0.56f, 0.2f));
        cylinderAnimation.Append(cylinder.transform.DOScaleY(0.46f, 0.2f));
        cylinderAnimation.Append(cylinder.transform.DOScaleY(0.56f, 0.2f));
        cylinderAnimation.Append(cylinder.transform.DOScaleY(0.46f, 0.2f));
        cylinderAnimation.Append(cylinder.transform.DOScaleY(0.56f, 0.2f));
    }
}
