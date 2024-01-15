using UnityEngine;
using Zenject;

// CreateAssetMenu attribute allows you to create an installer asset in the Unity editor
[CreateAssetMenu(fileName = "SolarSystemSettingsInstaller", menuName = "Installers/SolarSystemSettingsInstaller")]

// Installer class responsible for configuring and binding dependencies for Solar System settings
public class SolarSystemSettingsInstaller : ScriptableObjectInstaller<SolarSystemSettingsInstaller>
{
    // Reference to the GameSettings asset to be injected
    [SerializeField] private GameSettings gameSettings;

    // Reference to the SolarSystemValues asset to be injected
    [SerializeField] private SolarSystemValues solarSystemValues;

    // InstallBindings method where dependency bindings are defined
    public override void InstallBindings()
    {
        // Bind GameSettings to the container, using the provided instance, as a single instance, and non-lazy
        Container.BindInterfacesAndSelfTo<GameSettings>().FromInstance(gameSettings).AsSingle().NonLazy();

        // Bind SolarSystemValues to the container, using the provided instance, as a single instance, and non-lazy
        Container.BindInterfacesAndSelfTo<SolarSystemValues>().FromInstance(solarSystemValues).AsSingle().NonLazy();
    }
}
