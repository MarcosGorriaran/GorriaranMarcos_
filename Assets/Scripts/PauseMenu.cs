using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Canvas))]
public class PauseGame : MonoBehaviour, PlayerController.IMenuActions
{
    public static PauseGame instance;
    private PlayerController inputActions;
    Canvas menu;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance.gameObject);
        }
        inputActions = new PlayerController();
        inputActions.Menu.SetCallbacks(this);
    }
    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
    private void OnDestroy()
    {
        Time.timeScale = 1.0f;
    }
    private void Start()
    {
        menu = GetComponent<Canvas>();
    }
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (Time.timeScale == 0)
            {
                Player.instance.enabled = true;
                CrosshairTopDown.instance.enabled = true;
                Time.timeScale = 1;
                menu.enabled = false;
            }
            else
            {
                Player.instance.enabled = false;
                CrosshairTopDown.instance.enabled = false;
                Time.timeScale = 0;
                menu.enabled = true;
            }
        }
        
    }
}
