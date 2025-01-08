using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrosshairTopDown : MonoBehaviour, PlayerController.IShootingActions
{
    PlayerController inputActions;
    Coroutine fireHeldAction;
    public event Action<Vector2> onFire;
    void Awake()
    {
        inputActions = new PlayerController();
        inputActions.Shooting.SetCallbacks(this);
    }
    void OnEnable()
    {
        inputActions.Enable();
        Cursor.visible = false;
    }
    void OnDisable()
    {
        inputActions.Disable();
        Cursor.visible = true;
    }
    void Update()
    {
        //I would have used the new input system but the method is only called when there is an update on the position
        //and since the camera has to follow the character it made the crosshair stay in place while the cursor was moving with
        //the camera on the world.
        Vector2 cursorToWorldVector = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
        transform.position = cursorToWorldVector;
    }
    
    public void OnShooting(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            fireHeldAction = StartCoroutine(OnShootingHold());
        }
        if (context.canceled)
        {
            StopCoroutine(fireHeldAction);
        }
    }
    /**
     *<summary>
     *  This method was created because the new input system does not run
     *  the method in a loop if the mouse button is being held, only being
     *  called once so this method is run when the action is being performed
     *  and stoped when the context of the button returns that the button has been
     *  released
     *</summary> 
     */
    private IEnumerator OnShootingHold()
    {
        while (true)
        {
            onFire.Invoke(transform.position);
            yield return new WaitForEndOfFrame();
        }
    }
}
