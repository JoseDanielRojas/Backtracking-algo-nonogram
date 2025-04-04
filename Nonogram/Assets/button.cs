using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class button : MonoBehaviour
{
    // Start is called before the first frame update
    public void ExitGame()
    {


        // reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // load the previous scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }
}
