using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Rigidbody2D rigidBody;
    public Animator animator;

    Vector2 movement;

    public int maxHealth = 3000;
    public int currentHealth;
    public HealthBar healthBar;
    static public float Counter = 0;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

    }


    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + movement * moveSpeed * Time.fixedDeltaTime);
        TakeDamage(3);

        if (currentHealth <= 0)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CanBePickedUp"))
        {
            Item hitObject = collision.gameObject.GetComponent<Consumable>().item;
            if (hitObject != null)
            {
                print("it: " + hitObject.objectName);
                collision.gameObject.SetActive(false);
                currentHealth = maxHealth;
                Counter += 1;
            }
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(250);
        }

        if (collision.gameObject.CompareTag("Winner"))
        {
            FindObjectOfType<GameManager>().WinGame();
        }
    }
}

