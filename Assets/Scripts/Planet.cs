using UnityEngine;
using Zenject;

// Planet class inherits from SolarSystemItem
public class Planet : SolarSystemItem
{
    // Serialized field for an array of Moon objects
    [SerializeField] Moon[] moonsArray;

    // Zenject injection for the sun's transform with an identifier "Sun"
    [Inject(Id = "Sun")]
    Transform sun;

    // Zenject injection for the sun's size with an identifier "SunSize"
    [Inject(Id = "SunSize")]
    float sunSize;

    // Start method is called on the frame when a script is enabled
    private void Start()
    {
        // Initialize the planet with the sun's size and position
        Init(sunSize, sun.position);

        // Check if there are any moons in the array
        if (moonsArray.Length > 0)
        {
            // Loop through each moon in the array
            foreach (var moon in moonsArray)
            {
                // Initialize each moon with the planet's scale magnitude and position
                moon.Init(transform.localScale.magnitude, transform.position);
            }
        }
    }

    // OnEnable is called when the object becomes enabled and active
    private void OnEnable()
    {
        // Subscribe to the GalaxyManager's OnGalaxyTick event
        GalaxyManager.OnGalaxyTick += OnGalaxyTickHandler;
    }

    // OnDisable is called when the object becomes disabled
    private void OnDisable()
    {
        // Unsubscribe from the GalaxyManager's OnGalaxyTick event
        GalaxyManager.OnGalaxyTick -= OnGalaxyTickHandler;
    }

    // Event handler for the OnGalaxyTick event
    private void OnGalaxyTickHandler()
    {
        // Orbit around the sun's position
        Orbit(sun.position);

        // Orbit moons around the planet
        OrbitMoons();
    }

    // Method to orbit all moons around the planet
    void OrbitMoons()
    {
        // Loop through each moon in the array
        for (int i = 0; i < moonsArray.Length; i++)
        {
            // Orbit the current moon around the planet's position
            moonsArray[i].Orbit(transform.position);
        }
    }
}
