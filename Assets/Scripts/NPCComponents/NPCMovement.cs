using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class NPCMovement : MonoBehaviour
{
    private const string isMoving = "isMoving";

    [Header("Movement Config")]
    [SerializeField] private float waitTimeAtPath = 4f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private List<GameObject> paths;

    private GameObject currentPathObj;
    private int currentPathCount = 0;
    private Vector2 characterMovement;

    //components
    private Rigidbody2D rb;
    private Animator animator;


    private void OnEnable()
    {
        GetComponent<InteractableComponent>().OnAbleToInteract += HandleOnAbleToInteract;
    }

    private void OnDisable()
    {
        GetComponent<InteractableComponent>().OnAbleToInteract -= HandleOnAbleToInteract;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(ProcessMovement());
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleOnAbleToInteract(GameObject interactor)
    {
        rb.velocity = Vector2.zero;

        Vector2 direction = (interactor.transform.position - transform.position).normalized;

        transform.localScale = new(Mathf.Sign(direction.x), 1, 1);

        if (animator.GetBool(isMoving))
        {
            animator.SetBool(isMoving, false);
        }
    }

    private IEnumerator ProcessMovement()
    {
        //loop infinitely
        while(true)
        {
            currentPathObj = paths[currentPathCount];

            while (Vector2.Distance(transform.position, currentPathObj.transform.position) > 0.1f)
            {
                Vector2 direction = (currentPathObj.transform.position - transform.position).normalized;
                characterMovement = direction;

                yield return null;
            }

            characterMovement = Vector2.zero;

            yield return new WaitForSeconds(waitTimeAtPath);

            currentPathCount++;
            if (currentPathCount >= paths.Count)
            {
                currentPathCount = 0;
            }
        }
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
