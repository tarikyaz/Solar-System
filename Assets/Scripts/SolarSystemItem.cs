using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SolarSystemItem : MonoBehaviour
{

    [Inject]
    GameSettings gameSettings;
    [Inject(Id = "SunSize")]
    float sunScale;

    [SerializeField] float size = 1;
    [SerializeField] float distanceFromOrbitCecnter = 1;
    [SerializeField] float orbitSpeed = 1;
    [SerializeField] LineRenderer line;

    float angel = 0;
    float currnetDistance => sunScale * .5f + distanceFromOrbitCecnter * gameSettings.DistanceScale;

    public void Init(Vector2 centerPos)
    {
        // setting local scale
        transform.localScale = Vector3.one * gameSettings.SizeScale * size;
        var newPosition = centerPos + new Vector2(currnetDistance, 0);

        // setting init pos
        transform.position = newPosition;
        angel = UnityEngine.Random.Range(0.01f, 3.00f);

        // setting orbit
        CreateFilledCircle();

    }

    public void Orbit(Vector2 center)
    {
        Vector2 pos = new Vector2(Mathf.Sin(angel), Mathf.Cos(angel));
        pos *= currnetDistance;
        transform.position = center + pos;
        angel += Time.deltaTime * orbitSpeed * gameSettings.TimeScale;

    }
    void CreateFilledCircle()
    {
        line.positionCount = 0;
        int resolution = Mathf.FloorToInt(currnetDistance * 3);
        resolution = Mathf.Min(50, resolution);
        line.positionCount = resolution;
        // Iterate over the range of angles to create points around the circle
        var points = new List<Vector3>();
        for (float i = angel; i < resolution + angel; i++)
        {
            float v = 2 * Mathf.PI * i / resolution;

            Vector3 pos = new Vector2(Mathf.Cos(v) * currnetDistance, Mathf.Sin(v) * currnetDistance);

            points.Add(pos);
        }
        line.SetPositions(points.ToArray());
    }
}
