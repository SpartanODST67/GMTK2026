using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Pauser : MonoBehaviour
{
    public UnityEvent onPause;
    public UnityEvent onUnPause;
    bool isPaused = false;

    [SerializeField] PlayerInput input;
    InputAction pauseAction;

    void Awake()
    {
        pauseAction = input.actions["Pause"];
    }

    void OnEnable()
    {
        pauseAction.started += PauseInput;
    }

    void OnDisable()
    {
        pauseAction.started -= PauseInput;
    }

    public void PauseInput(InputAction.CallbackContext context)
    {
        isPaused = !isPaused;
        if(isPaused)
            Pause();
        else
            UnPause();
    }

    public void Pause()
    {
        Time.timeScale = 0;
        onPause.Invoke();
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        onUnPause.Invoke();
    }
}
