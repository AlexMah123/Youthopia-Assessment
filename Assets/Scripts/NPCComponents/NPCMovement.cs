using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class NPCMovement : MonoBehaviour
{
    private const string isMoving = "isMoving";

    [Header("Movement Config")]
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 characterMovement;

    //components
    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    private void ProcessMovement()
    {

    }

    private void HandleMovement()
    {
        rb.velocity = characterMovement * moveSpeed;

        if (characterMovement.x != 0 || characterMovement.y != 0)
        {
            transform.localScale = new(Mathf.Sign(characterMovement.x), 1, 1);
            animator.SetBool(isMoving, true);
        }
        else
        {
            animator.SetBool(isMoving, false);
        }
    }
}
