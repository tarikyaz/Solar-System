using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{

    public static Action OnGalaxyTick { get; internal set; }
    [Inject(Id = "Cam")]
    Camera cam;
    [Inject]
    GameSettings gameSettings;
    Coroutine cameraCoroutine;
    SolarSystemItem[] solarSystemItemsArray;
    SolarSystemItem randomItem => solarSystemItemsArray[UnityEngine.Random.Range(0, solarSystemItemsArray.Length)];
    Vector3 camInitPos;
    float camInitSize;
    private void FixedUpdate()
    {
        OnGalaxyTick?.Invoke();
    }
    private void Start()
    {
        camInitPos = cam.transform.position;
        camInitSize = cam.orthographicSize;
        solarSystemItemsArray = GetComponentsInChildren<SolarSystemItem>();
        ActivateCamera();
    }
    void ActivateCamera()
    {
        if (cameraCoroutine != null)
        {
            StopCoroutine(cameraCoroutine);
        }
        cameraCoroutine = StartCoroutine(CameraMovemnt());
    }
    IEnumerator CameraMovemnt()
    {
        while (true)
        {

            yield return new WaitForSeconds(/*UnityEngine.Random.Range(6.5f, 25.5f)*/1);
            yield return MoveCameraToRandomGalaxyItem(3).WaitForCompletion();
            yield return new WaitForSeconds(10);
            yield return ReturnToCamDefaultPos(3).WaitForCompletion();
        }
    }
    private Sequence ReturnToCamDefaultPos(float duration)
    {
        Time.timeScale = 1;
        Sequence sequence = DOTween.Sequence();
        cam.transform.SetParent(null);
        sequence.Insert(0, cam.transform.DOMove(camInitPos, duration * .5f));
        sequence.Insert(0, cam.DOOrthoSize(camInitSize, duration * .5f));
        return sequence;
    }
    private Sequence MoveCameraToRandomGalaxyItem(float duration)
    {
        Time.timeScale = 0;
        var target = randomItem;
        Sequence sequence = DOTween.Sequence();
        sequence.SetUpdate(true);
        cam.transform.SetParent(target.transform);
        sequence.Insert(0, cam.transform.DOLocalMove(Vector3.forward * camInitPos.z * target.transform.localScale.magnitude, duration * .5f));
        float sizeTarget = target.transform.localScale.x * gameSettings.SizeScale * 1.5f;
        sequence.Insert(0, cam.DOOrthoSize(sizeTarget, duration * .5f));
        sequence.OnComplete(() =>
        {
            Time.timeScale = 1;
        }); 
        return sequence;
    }
}
