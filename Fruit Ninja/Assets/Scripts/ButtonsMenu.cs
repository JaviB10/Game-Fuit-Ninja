using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsMenu : MonoBehaviour
{
    public GameObject sound;
    public GameObject[] images;
    private bool isActivated = false;

    public void Exit()
    {
        Application.Quit();
    }

    public void ControllerSound()
    {
        isActivated = !isActivated;

        if (isActivated)
        {
            sound.SetActive(false);
            images[0].SetActive(false);
            images[1].SetActive(true);
        }
        else
        {
            sound.SetActive(true);
            images[0].SetActive(true);
            images[1].SetActive(false);
        }
    }
}
