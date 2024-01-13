using System.ComponentModel;
using UnityEngine;
using Zenject;

public class GalaxyObjectInstaller : MonoInstaller
{
    [SerializeField] SpriteRenderer sun;
    [SerializeField] Camera cam;
    [Inject]
    GameSettings gameSettings;
    
    public override void InstallBindings()
    {
        float sunScale = 109 * gameSettings.SizeScale;
        sun.transform.localScale = sunScale * Vector3.one;
        cam.orthographicSize = sunScale + 40 * gameSettings.DistanceScale;
        cam.transform.position = sun.transform.position + new Vector3(sun.bounds.max.x, 0, -5);
        Container.Bind<Transform>().WithId("Sun").FromInstance(sun.transform).AsSingle();
        Container.Bind<float>().WithId("SunSize").FromInstance(sun.bounds.size.x).AsSingle();
        Container.Bind<SolarSystemItem>().AsSingle();
        Container.Bind<Planet>().AsSingle();
    }
}