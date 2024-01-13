
using UnityEngine;
using Zenject;
[CreateAssetMenu(fileName = "SolarSystemSettingsInstaller", menuName = "Installers/SolarSystemSettingsInstaller")]

public class SolarSystemSettingsInstaller : ScriptableObjectInstaller<SolarSystemSettingsInstaller>
{
    [SerializeField] private GameSettings gameSettings;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameSettings>().FromInstance(gameSettings).AsSingle().NonLazy();
    }
}
