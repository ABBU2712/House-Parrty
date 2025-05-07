using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneToggle : MonoBehaviour
{
    private string houseSceneName = "HouseTest"; 
    private string racingSceneName = "Racinggame";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetButton("js2"))
        {
            string currentScene = SceneManager.GetActiveScene().name;

            if (currentScene == houseSceneName)
            {
                SceneManager.LoadScene(racingSceneName);
            }
            else if (currentScene == racingSceneName)
            {
                SceneManager.LoadScene(houseSceneName);
            }
        }
    }
}
