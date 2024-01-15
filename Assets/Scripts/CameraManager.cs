using DG.Tweening;
using System.Collections;
using UnityEngine;
using Zenject;

public class CameraManager : MonoBehaviour
{
    // Dependency injection using Zenject
    [Inject] private GameSettings gameSettings;
    [Inject(Id = "GameManager")] private GameManager gameManager;

    // Coroutine for camera movement
    private Coroutine cameraCoroutine;

    // Lambda property for getting a random solar system item
    private SolarSystemItem randomItem =>
        gameManager.GalaxyManager.solarSystemItemsArray[UnityEngine.Random.Range(0, gameManager.GalaxyManager.solarSystemItemsArray.Length)];

    // Initial camera position and size
    private Vector3 camInitPos;
    private float camInitSize;

    // Injected camera
    [Inject(Id = "Cam")] private Camera cam;

    // DOTween sequence for camera animations
    private Sequence sequence;

    // Start is called before the first frame update
    void Start()
    {
        // Store initial camera position and size
        camInitPos = cam.transform.position;
        camInitSize = cam.orthographicSize;
    }

    // Method to activate random camera movement
    void ActivateRandomCameraMoving()
    {
        if (cameraCoroutine != null)
        {
            StopCoroutine(cameraCoroutine);
        }
        cameraCoroutine = StartCoroutine(RandomCameraMoving());
    }

    // Public method to move the camera to a specific solar system item
    public void MoveCameraTo(SolarSystemItem solarSystemItem)
    {
        if (cameraCoroutine != null)
        {
            StopCoroutine(cameraCoroutine);
        }
        cameraCoroutine = StartCoroutine(MovingCameraToItem(solarSystemItem));
    }

    // Public method to reset the camera to its default position
    public void RestCamera()
    {
        if (cameraCoroutine != null)
        {
            StopCoroutine(cameraCoroutine);
        }
        cameraCoroutine = StartCoroutine(RestingCamera());
    }

    // DOTween sequence for returning the camera to its default position
    private Sequence ReturnToCamDefaultPos(float duration)
    {
        Time.timeScale = 1;
        sequence?.Kill(); // Kill the existing sequence if it exists
        sequence = DOTween.Sequence();

        // Detach camera from any parent
        cam.transform.SetParent(null);

        // Insert animation to move the camera to its initial position and size
        sequence.Insert(0, cam.transform.DOMove(camInitPos, duration * .5f));
        sequence.Insert(0, cam.DOOrthoSize(camInitSize, duration * .5f));

        return sequence;
    }

    // DOTween sequence for moving the camera to a random galaxy item
    private Sequence MoveCameraToRandomGalaxyItem(float duration)
    {
        Time.timeScale = 0;

        // Get a random target solar system item
        SolarSystemItem target = randomItem;

        // Move the camera to the selected galaxy item
        return MoveCameraToGalaxyItem(duration, target);
    }

    // DOTween sequence for moving the camera to a specific galaxy item
    private Sequence MoveCameraToGalaxyItem(float duration, SolarSystemItem target)
    {
        sequence?.Kill(); // Kill the existing sequence if it exists
        sequence = DOTween.Sequence();

        // Handle onPause event to kill the sequence
        sequence.onPause = () => { sequence.Kill(); };

        // Calculate the target size based on the item's scale and game settings
        float sizeTarget = target.transform.localScale.x * gameSettings.SizeScale * 1.5f;

        // Attach camera to the target item
        cam.transform.SetParent(target.transform);

        // Animation sequence to move the camera to the target item
        sequence.SetUpdate(true);
        sequence.Append(cam.transform.DOLocalMove(Vector3.forward * camInitPos.z * target.transform.localScale.magnitude, duration * .5f));
        sequence.Append(cam.DOOrthoSize(sizeTarget, duration * .5f));

        // Callback when the animation completes
        sequence.OnComplete(() => { Time.timeScale = 1; });

        return sequence;
    }

    // Coroutine for moving the camera to a specific item
    IEnumerator MovingCameraToItem(SolarSystemItem solarSystemItem)
    {
        yield return MoveCameraToGalaxyItem(3, solarSystemItem).WaitForCompletion();
        yield return new WaitForSeconds(10);
        yield return ReturnToCamDefaultPos(3).WaitForCompletion();
    }

    // Coroutine for random camera movement
    IEnumerator RandomCameraMoving()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(6.5f, 25.5f));
            yield return MoveCameraToRandomGalaxyItem(3).WaitForCompletion();
            yield return new WaitForSeconds(10);
            yield return ReturnToCamDefaultPos(3).WaitForCompletion();
        }
    }

    // Coroutine for resting the camera
    IEnumerator RestingCamera()
    {
        yield return ReturnToCamDefaultPos(2).WaitForCompletion();
    }
}
