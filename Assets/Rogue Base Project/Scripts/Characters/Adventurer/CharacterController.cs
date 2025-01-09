using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class CharacterController : MonoBehaviour  // Fixed typo in class name
{
    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    float moveSpeed;

    [SerializeField]
    PlayerControls playerControls;
    private InputAction move;
    private InputAction jump;
    // private InputAction fire;

    [SerializeField]
    GameObject Bullet;

    [SerializeField]
    public int health = 3;

    [SerializeField]
    List<Image> liveImages;

    [SerializeField]
    float cooldownTime;

    float hurttimer;
    SpriteRenderer sprite;

    [SerializeField]
    float iframeTime;

    float cooldown;

    [SerializeField]
    private int currentScore;
    bool canTakeDamage = true;

    BoxCollider2D boxCollider;

    Vector2 moveDirection = Vector2.zero;

    private void Awake()
    {
        playerControls = new PlayerControls();
        sprite = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        // Initialize input system
        playerControls.Enable();
    }

    private void OnEnable()
    {
        if (playerControls == null)
            playerControls = new PlayerControls();


        jump = playerControls.Player.Jump;
        move = playerControls.Player.Move;
        move.Enable();
        jump.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        //  fire.Disable();
        jump.Disable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //  rb.gravityScale = 1f;  // Restore gravity
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;  // Only freeze rotation
    }

    // private void Fire(InputAction.CallbackContext context)
    // {
    //     if (cooldown <= 0)
    //     {
    //         Instantiate(Bullet, gameObject.transform.position, Quaternion.identity);
    //         cooldown = cooldownTime;
    //     }
    // }

    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        Debug.Log(moveDirection);
        moveDirection = new Vector2(move.ReadValue<Vector2>().x, 0); // Only use horizontal input

        for (int i = 0; i < liveImages.Count; i++)
        {
            if (i < health)
            {
                liveImages[i].enabled = true;
            }
            else
            {
                liveImages[i].enabled = false;
            }
        }

        if (health <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }

        if (hurttimer > 0)
        {
            hurttimer -= Time.deltaTime;

            if (hurttimer <= 0)
            {
                sprite.color = Color.white;
                hurttimer = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        float rotationSpeed = 5f;

        if (moveDirection == new Vector2(-1, 0))
        {
            float targetAngle = 12f;
            float currentAngle = transform.eulerAngles.z;
            float newAngle = Mathf.LerpAngle(
                currentAngle,
                targetAngle,
                rotationSpeed * Time.deltaTime
            );
            transform.rotation = Quaternion.Euler(0, 0, newAngle);
        }

        if (moveDirection == new Vector2(1, 0))
        {
            float targetAngle = -12f;
            float currentAngle = transform.eulerAngles.z;
            float newAngle = Mathf.LerpAngle(
                currentAngle,
                targetAngle,
                rotationSpeed * Time.deltaTime
            );
            transform.rotation = Quaternion.Euler(0, 0, newAngle);
        }

        if (moveDirection == new Vector2(0, 0))
        {
            float targetAngle = 0f;
            float currentAngle = transform.eulerAngles.z;
            float newAngle = Mathf.LerpAngle(
                currentAngle,
                targetAngle,
                rotationSpeed * Time.deltaTime
            );
            transform.rotation = Quaternion.Euler(0, 0, newAngle);
        }

        // Preserve vertical velocity while applying horizontal movement
        float currentYVelocity = rb.linearVelocity.y;
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, currentYVelocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }


}