using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Player : Entity, PlayerController.IAvatarActions, IMove
{
    
    public static Player instance { private set; get; }
    const string MovingParameterName = "Walking";
    const string DeathTriggerName = "Dead";
    public event Action<int> scoreValueChanged;
    [SerializeField]
    CrosshairTopDown crosshair;
    PlayerController inputController;
    private Rigidbody2D physicBody;
    [SerializeField]
    float speed;
    int scorePoints = 0;

    protected override void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
        base.Awake();
        inputController = new PlayerController();
        inputController.Avatar.SetCallbacks(this);
        physicBody = GetComponent<Rigidbody2D>();
        crosshair.onFire += OnFire;
       
    }
    public void GivePoints(uint points)
    {
        scorePoints += Convert.ToInt32(points);
        scoreValueChanged.Invoke(scorePoints);
    }
    public bool PayWithPoints(uint points)
    {
        int estimatedValue = scorePoints - Convert.ToInt32(points);

        if(estimatedValue < 0)
        {
            return false;
        }
        scorePoints = estimatedValue;
        scoreValueChanged.Invoke(scorePoints);
        return true;
    }
    protected override void OnDeath()
    {
        animator.SetTrigger(DeathTriggerName);
        inputController.Disable();
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
        if (!context.canceled)
        {
            animator.SetBool(MovingParameterName, true);
            SetAnimationDirection(context.ReadValue<Vector2>().x, context.ReadValue<Vector2>().y);
        }
        else
        {
            animator.SetBool(MovingParameterName,false);
        }
        
    }

    
}
