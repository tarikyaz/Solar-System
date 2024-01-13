using System.Collections;
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

    float angel = 0;
    float currnetDistance => sunScale * .5f + distanceFromOrbitCecnter * gameSettings.DistanceScale;

    public void SetStartingPos(Vector2 centerPos)
    {
        transform.localScale = Vector3.one * gameSettings.SizeScale * size;
        var newPosition = centerPos + new Vector2(currnetDistance, 0);
        transform.position = newPosition;
        angel = UnityEngine.Random.Range(0.01f, 3.00f);
    }

    public void Orbit(Vector2 center)
    {
        Vector2 pos = new Vector2(Mathf.Sin(angel), Mathf.Cos(angel));
        pos*= currnetDistance;
        transform.position = center + pos;
        angel += Time.deltaTime * orbitSpeed* gameSettings.TimeScale;
    }

}
