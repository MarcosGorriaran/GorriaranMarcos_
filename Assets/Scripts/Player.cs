using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity, PlayerController.IPlayerActions
{
    PlayerController inputController;
    protected override void Awake()
    {
        base.Awake();
        inputController = new PlayerController();
        inputController.Player.SetCallbacks(this);
    }
    public void OnAiming(InputAction.CallbackContext context)
    {
        Debug.Log(context);
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnShooting(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
