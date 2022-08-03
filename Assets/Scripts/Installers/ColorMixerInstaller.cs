using UnityEngine;
using Zenject;

public class ColorMixerInstaller : MonoInstaller
{
    [SerializeField] private ColorMixer colorMixer;
    public override void InstallBindings()
    {
        Container.Bind<ColorMixer>().FromInstance(colorMixer).AsSingle().NonLazy();
    }
}