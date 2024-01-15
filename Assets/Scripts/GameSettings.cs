using System;
using System.Linq;
using UnityEngine;

// Serializable class for game settings
[Serializable]
public class GameSettings
{
    // Scaling factors for time, distance, and size
    public float TimeScale = 1, DistanceScale = 1, SizeScale = 1;
}

// Serializable class for solar system values
[Serializable]
public class SolarSystemValues
{
    // Array of settings for individual solar system items
    [SerializeField] SolarSystemItemSettings[] settings;

    // Get settings for a specific solar system item
    public SolarSystemItemSettings GetSettings(SolarSystemItemEnum systemItem)
    {
        // Filter settings based on the provided system item
        var newSettings = settings.Where(x => x.systemItem == systemItem).ToArray();

        // Check if settings were found
        if (newSettings.Length > 0)
        {
            // Return the first set of settings (assuming there's only one)
            return newSettings[0];
        }
        else
        {
            // Log an error if settings were not found
            Debug.LogError($"Item name: {systemItem} not found!");
            return null;
        }
    }
}

// Serializable class for individual solar system item settings
[Serializable]
public class SolarSystemItemSettings
{
    // Enum representing a solar system item
    public SolarSystemItemEnum systemItem;

    // Size, distance from orbit center, and orbit speed settings
    public float Size = 1;
    public float DistanceFromOrbitCenter = 1;
    public float OrbitSpeed = 1;
}

// Enum representing different solar system items
public enum SolarSystemItemEnum
{
    None, Mercury, Venus, Earth, Mars, Jupiter, Saturn, Neptune, Pluto, Moon, Uranus
}
