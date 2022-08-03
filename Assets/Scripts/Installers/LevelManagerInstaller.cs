using UnityEngine;
using Zenject;

public class LevelManagerInstaller : MonoInstaller
{
    [SerializeField] private LevelManager levelManager;

    public override void InstallBindings()
    {
        Container.Bind<LevelManager>().FromInstance(levelManager).AsSingle().NonLazy();
    }
}