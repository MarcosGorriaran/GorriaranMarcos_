using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Player : Entity, PlayerController.IAvatarActions, IMove
{
    [SerializeField]
    CrosshairTopDown crosshair;
    PlayerController inputController;
    private Rigidbody2D physicBody;
    [SerializeField]
    float speed;

    protected override void Awake()
    {
        base.Awake();
        inputController = new PlayerController();
        inputController.Avatar.SetCallbacks(this);
        physicBody = GetComponent<Rigidbody2D>();
        crosshair.onFire += OnFire;
    }
    void OnEnable()
    {
        inputController.Enable();
    }
    void OnDisable()
    {
        inputController.Disable();
    }
    public void Move(Vector2 target)
    {
        physicBody.velocity = target.normalized * speed;
    }
    public void OnFire(Vector2 targetPos)
    {
        weapon.Fire(transform.position, targetPos, gameObject);
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Move(context.ReadValue<Vector2>());
    }

    
}
