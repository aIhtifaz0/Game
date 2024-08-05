using UnityEngine;
using UnityEngine.SceneManagement;

public class GameObjectChecker : MonoBehaviour
{
    public GameObject objectToCheck; // The GameObject you want to check
    public string sceneToLoad;       // The name of the scene to load

    void Update()
    {
        // Check if the GameObject is destroyed
        if (objectToCheck == null)
        {
            // Load the new scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
