using UnityEngine;
using Zenject;

public class Planet : SolarSystemItem
{
    [SerializeField] Moon[] moonsArray;

    private void Start()
    {
        Init(Vector2.zero);
        if (moonsArray.Length > 0)
        {

            foreach (var moon in moonsArray)
            {
                Init(transform.position);
            }
        }
    }

    private void OnEnable()
    {
        GameManager.OnGalaxyTick += OnGalaxyTikHandler;
    }
    private void OnDisable()
    {
        GameManager.OnGalaxyTick -= OnGalaxyTikHandler;
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
