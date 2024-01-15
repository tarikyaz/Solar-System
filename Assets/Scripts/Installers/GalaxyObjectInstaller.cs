using UnityEngine;
using Zenject;

public class GalaxyObjectInstaller : MonoInstaller
{
    // Serialized fields to expose in the Unity Editor
    [SerializeField] SpriteRenderer sun;
    [SerializeField] Camera cam;
    [SerializeField] GameManager gameManager;

    // Injecting the GameSettings using Zenject
    [Inject]
    GameSettings gameSettings;

    // This method is called when the Zenject container is setting up bindings.
    public override void InstallBindings()
    {
        // Calculate the scaled size of the sun based on the game settings
        float sunScale = 109 * gameSettings.SizeScale;

        // Set the scaled size of the sun
        sun.transform.localScale = sunScale * Vector3.one;

        // Calculate the orthographic size of the camera based on the scaled sun size and distance scale
        cam.orthographicSize = sunScale + 40 * gameSettings.DistanceScale;

        // Position the camera behind the sun for a good view
        cam.transform.position = sun.transform.position + new Vector3(sun.bounds.max.x, 0, -5);

        // Bind the sun's transform to the Zenject container with the identifier "Sun"
        Container.Bind<Transform>().WithId("Sun").FromInstance(sun.transform).AsSingle();

        // Bind the size of the sun to the Zenject container with the identifier "SunSize"
        Container.Bind<float>().WithId("SunSize").FromInstance(sun.bounds.size.x).AsSingle();

        // Bind the camera to the Zenject container with the identifier "Cam"
        Container.Bind<Camera>().WithId("Cam").FromInstance(cam).AsSingle();

        // Bind the game manager to the Zenject container with the identifier "GameManager"
        Container.Bind<GameManager>().WithId("GameManager").FromInstance(gameManager).AsSingle();
    }
}
