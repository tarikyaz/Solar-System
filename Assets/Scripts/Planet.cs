using UnityEngine;
using Zenject;

public class Planet : SolarSystemItem
{   
    [SerializeField] Moon[] moonsArray;
    [Inject(Id = "Sun")]
    readonly Transform sun;

    private void Start()
    {
        SetStartingPos(Vector2.zero);
        if (moonsArray.Length > 0)
        {

            foreach (var moon in moonsArray)
            {
                SetStartingPos(transform.position);
            }
        }
    }

    private void OnEnable()
    {
        GalaxyManager.OnGlaxyTik += OnGalaxyTikHandler;
    }
    private void OnDisable()
    {
        GalaxyManager.OnGlaxyTik -= OnGalaxyTikHandler;
    }

    private void OnGalaxyTikHandler()
    {
        Orbit(Vector2.zero);
        OrbitMoons();
    }

    void OrbitMoons()
    {
        for (int i = 0; i < moonsArray.Length; i++)
        {

            moonsArray[i].Orbit(transform.position);
        }
    }


}
