using System.ComponentModel;
using UnityEngine;
using Zenject;

public class GalaxyObjectInstaller : MonoInstaller
{
    [SerializeField] Transform sun;
    [Inject]
    GameSettings gameSettings;
    private void Start()
    {
        sun.localScale = Vector3.one * gameSettings.SizeScale * 109;
    }
    public override void InstallBindings()
    {
        Container.Bind<Transform>().WithId("Sun").FromInstance(sun).AsSingle();
        Container.Bind<SolarSystemItem>().AsSingle();
        Container.Bind<Planet>().AsSingle();
    }
}