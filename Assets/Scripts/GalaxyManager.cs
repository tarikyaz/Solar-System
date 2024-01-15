using System;
using UnityEngine;

// Class responsible for managing the galaxy and its solar system items.
public class GalaxyManager : MonoBehaviour
{
    // Array to store the solar system items.
    public SolarSystemItem[] solarSystemItemsArray;

    // Event triggered on each fixed update of the galaxy.
    public static Action OnGalaxyTick { get; internal set; }

    // Called every fixed frame-rate frame.
    private void FixedUpdate()
    {
        // Invoke the OnGalaxyTick event if it's not null.
        OnGalaxyTick?.Invoke();
    }

    // Context menu function to set the solar system items array based on child components.
    [ContextMenu("SetItemsArray")]
    void SetItemsArray()
    {
        // Get all SolarSystemItem components from the children of the current game object.
        solarSystemItemsArray = GetComponentsInChildren<SolarSystemItem>();
    }
}
