using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI.Table;

public class MouseControlOnPause : MonoBehaviour
{
    public PauseMenu pauseMenu; // Reference to your PauseMenu script

    void Update()
    {
        if (pauseMenu != null)
        {
            if (PauseMenu.gameIsPaused)
            {
                // Show and unlock the cursor
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                // Hide and lock the cursor (FPS-style)
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        else
        {
            Debug.LogWarning("MouseControlOnPause: PauseMenu reference not set!");
        }
    }
}


