using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonScript : MonoBehaviour
{
    public void OnMenuButtonClicked()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
