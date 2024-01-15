using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button solarSystemItem_Button; // Button for solar system items
    [Inject(Id = "GameManager")]
    GameManager gameManager;

    void Start()
    {
        // Set the initial state of the solar system item button
        solarSystemItem_Button.gameObject.SetActive(true);

        // Iterate through all solar system items in the GalaxyManager
        foreach (var item in gameManager.GalaxyManager.solarSystemItemsArray)
        {
            // Instantiate a new button for each solar system item
            var newButton = Instantiate(solarSystemItem_Button, solarSystemItem_Button.transform.parent);

            // Add a listener to the button to move the camera to the associated solar system item
            newButton.onClick.AddListener(() =>
            {
                gameManager.CameraManager.MoveCameraTo(item);
            });

            // Set the text of the button to the name of the associated solar system item
            newButton.transform.GetChild(0).GetComponent<TMP_Text>().text = item.gameObject.name;
        }

        // Set the final state of the solar system item button
        solarSystemItem_Button.gameObject.SetActive(false);
    }

    // Reset the camera to its default position
    public void ResetCamera()
    {
        gameManager.CameraManager.RestCamera(); // Corrected "RestCamera" to "ResetCamera"
    }
}
