using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Enemy : InteractriveObject
{
    [SerializeField]
    private float _minSpeed = 0.5f;
    [SerializeField]
    private float _maxSpeed = 4f;
    [SerializeField]
    private float _minScale = 0.8f;
    [SerializeField]
    private float _maxScale = 1.3f;

    private float _speed = 3f;
    private float _stopDistance = 0.5f;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRendenrer;
    private bool isHitPlayer = false;

    private void Awake()
    {
        float randomScale = Random.Range(_minScale, _maxScale);
        transform.localScale = new Vector3(randomScale, randomScale, 1);
        _speed = Random.Range(_minSpeed, _maxSpeed);
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRendenrer = GetComponent<SpriteRenderer>();
    }


    private void FixedUpdate()
    {
        if (!isHitPlayer)
            Move(Player.singleton.transform.position);
        else
            _rigidbody2D.velocity = Vector2.zero;
    }

    public void Move(Vector3 point)
    {
        if ((transform.localPosition - point).sqrMagnitude >= _stopDistance * _stopDistance)
        {
            var dir = (point - transform.localPosition).normalized;
            _rigidbody2D.velocity = dir * _speed;
            _spriteRendenrer.flipX = _rigidbody2D.velocity.x < 0 ? true : false;

        }
        else
        {
            _rigidbody2D.velocity = Vector2.zero;
        }
    }

    public override void Interaction()
    {
        DontDestroyOnLoad(this.gameObject);
        isHitPlayer = true;
        Player.singleton.OnTakeDamage();
    }
}
