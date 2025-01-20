using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float _length, _height;
    private Vector2 _startPos;
    [SerializeField]
    public Camera MyCamera;
    [SerializeField]
    public float ParallaxEffect;
    void Start()
    {
        _startPos = transform.position;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
        _height = GetComponent<SpriteRenderer>().bounds.size.y;
        if(MyCamera == null)
        {
            MyCamera = Camera.main;
        }

    }
    void Update()
    {
        Vector2 temp = MyCamera.transform.position * (1f - ParallaxEffect);
        Vector2 dist = (MyCamera.transform.position * ParallaxEffect);

        transform.position = new Vector3(_startPos.x + dist.x, _startPos.y + dist.y, transform.position.z);

        float changeX = _startPos.x;
        float changeY = _startPos.y;
        if (temp.x > _startPos.x + _length)
            changeX += _length;
        else if (temp.x < _startPos.x - _length)
            changeX -= _length;

        if (temp.y > _startPos.y + _height)
            changeY += _height;
        else if (temp.y < _startPos.y - _height)
            changeY -= _height;

        _startPos = new Vector2(changeX, changeY);
    }
}
