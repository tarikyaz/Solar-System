using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SolarSystemItem : MonoBehaviour
{
    // Enum representing different types of solar system items
    [SerializeField]
    SolarSystemItemEnum ItemType;

    // Injected GameSettings dependency
    [Inject]
    GameSettings gameSettings;

    // Injected SolarSystemValues dependency
    [Inject]
    SolarSystemValues solarSystemValues;

    // Current settings for the solar system item
    SolarSystemItemSettings currentSettings;

    // LineRenderer for displaying the orbit path
    [SerializeField] LineRenderer line;

    // Angle of the solar system item in orbit
    float angle = 0;

    // Current distance from the center of the orbit
    float currentDistance;

    // Initialization method for setting up the solar system item
    public virtual void Init(float centerSize, Vector2 centerPos)
    {
        // Retrieve settings for the current item type from SolarSystemValues
        currentSettings = solarSystemValues.GetSettings(ItemType);

        // Calculate the current distance based on center size, distance scale, and item settings
        currentDistance = centerSize * 0.5f + currentSettings.DistanceFromOrbitCenter * gameSettings.DistanceScale;

        // Set the local scale of the solar system item
        transform.localScale = Vector3.one * gameSettings.SizeScale * currentSettings.Size;

        // Calculate the new position based on the center position and current distance
        var newPosition = centerPos + new Vector2(currentDistance, 0);

        // Set the initial position of the solar system item
        transform.position = newPosition;

        // Set a random initial angle for variety in orbit positions
        angle = UnityEngine.Random.Range(0.01f, 3.00f);

        // Create the orbit path
        CreateFilledCircle();
    }

    // Method to update the position of the solar system item in orbit
    public void Orbit(Vector2 center)
    {
        // Calculate the new position based on the current angle and distance
        Vector2 pos = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * currentDistance;

        // Set the new position of the solar system item
        transform.position = center + pos;

        // Update the angle for the next frame based on orbit speed and time scale
        angle += Time.deltaTime * currentSettings.OrbitSpeed * gameSettings.TimeScale;
    }

    // Method to create a filled circle representing the orbit path
    void CreateFilledCircle()
    {
        // Check if the LineRenderer is enabled before proceeding
        if (line.enabled)
        {
            // Clear existing positions in the LineRenderer
            line.positionCount = 0;

            // Calculate the number of points based on the resolution and current distance
            int resolution = Mathf.FloorToInt(currentDistance * 3);
            line.positionCount = resolution;

            // Iterate over the range of angles to create points around the circle
            var points = new List<Vector3>();
            for (float i = angle; i < resolution + angle; i++)
            {
                // Calculate the position in polar coordinates
                float v = 2 * Mathf.PI * i / resolution;
                Vector3 pos = new Vector2(Mathf.Cos(v) * currentDistance, Mathf.Sin(v) * currentDistance);

                // Add the position to the list of points
                points.Add(pos);
            }

            // Set the positions in the LineRenderer
            line.SetPositions(points.ToArray());
        }
    }
}
