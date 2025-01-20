using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMovement : MonoBehaviour
{
    [SerializeField]
    float _speed;
    [SerializeField]
    Vector2 _direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        GetComponent<Rigidbody2D>().velocity = _direction.normalized * _speed;
    }
}
