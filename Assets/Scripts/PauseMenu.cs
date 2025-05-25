using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] InputAction pause;
    [SerializeField] Canvas pauseCanvas;

    bool paused = false;

    private void OnEnable()
    {
        pause.Enable();
        pause.performed += TogglePauseInput;
    }

    private void OnDisable()
    {
        pause.Disable();
        pause.performed -= TogglePauseInput;
    }


    private void TogglePauseInput(InputAction.CallbackContext context) => TogglePause();

    public void TogglePause()
    {
        paused = !paused;

        Time.timeScale = paused ? 0 : 1;

        pauseCanvas.enabled = paused;
    }
}
