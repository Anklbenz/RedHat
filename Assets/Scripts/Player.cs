using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class Player : MonoBehaviour
{
    public static Player singleton { get; private set; }

    [SerializeField]
    private float playerSpeed = 5f;
    [SerializeField]
    private Joystick joystick;
    private Rigidbody2D _rb;
    private Animator animator;
    private Vector2 movePosition;
    private bool isDead = false;

    public delegate void OnCoinTake();
    public event OnCoinTake onGemTake;


    private void Awake()
    {
        singleton = this;
        _rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        DontDestroyOnLoad(this.gameObject);
    }
    private void Update()
    {

        //movePosition.x = Input.GetAxisRaw("Horizontal");
        //movePosition.y = Input.GetAxisRaw("Vertical");
        if (joystick)
        {
            movePosition.x = joystick.Horizontal;
            movePosition.y = joystick.Vertical;
        }
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            _rb.MovePosition(_rb.position + movePosition * playerSpeed * Time.fixedDeltaTime);
            animator.SetFloat("Horizontal", movePosition.x);
            animator.SetFloat("Vertical", movePosition.y);
            animator.SetFloat("Speed", movePosition.sqrMagnitude);
        }
    }

    public void OnGemTake()
    {
        onGemTake?.Invoke();
    }

    public void OnTakeDamage()
    {
        isDead = true;
        animator.SetBool("isDead", true);
        SceneManager.LoadScene("DeadScreen");
        
    }
}
