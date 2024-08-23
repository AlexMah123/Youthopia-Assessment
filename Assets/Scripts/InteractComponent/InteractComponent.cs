using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IInteractable
{
    void ShowInteractPrompt(GameObject interactor, GameObject interactUIPrompt);

    void Interact();
}

[RequireComponent(typeof(CircleCollider2D))]
public class InteractComponent : MonoBehaviour
{
    [Header("Interact Configs")]
    [SerializeField] GameObject interactUIPrompt;
    [SerializeField] CircleCollider2D interactCollider;
    [SerializeField] LayerMask interactableLayer;
    [SerializeField] LayerMask obstacleLayer;

    private GameObject closestInteractableObj = null;
    private List<GameObject> interactables = new();

    private void OnEnable()
    {
        InputManager.OnInteract += HandleOnInteract;
    }

    private void OnDisable()
    {
        InputManager.OnInteract -= HandleOnInteract;
    }

    private void Awake()
    {
        if(interactUIPrompt == null)
        {
            throw new MissingReferenceException("InteractUIPrompt not assigned");
        }
    }

    private void Update()
    {
        if(closestInteractableObj)
        {
            closestInteractableObj.GetComponent<IInteractable>().ShowInteractPrompt(gameObject, interactUIPrompt);
        }
        else
        {
            if (interactUIPrompt.activeSelf)
            {
                interactUIPrompt.gameObject.SetActive(false);
            }
        }
    }

    private void HandleOnInteract()
    {
        if (closestInteractableObj != null)
        {
            closestInteractableObj.GetComponent<IInteractable>().Interact();
        }
    }

    private void SetClosestInteractable()
    {
        //early return
        if (interactables.Count <= 0)
        {
            closestInteractableObj = null;
            return;
        }

        //cache
        float closestDistance = Mathf.Infinity;

        foreach (GameObject interactableObj in interactables)
        {
            //check if any obstacle between them.
            RaycastHit2D obstacleHit = Physics2D.Raycast(transform.position,
                                                        interactableObj.transform.position - transform.position,
                                                        Vector2.Distance(transform.position, interactableObj.transform.position),
                                                        obstacleLayer);

            //if hit an obstacle, skip
            if (obstacleHit.collider != null) continue;

            float distance = Vector2.Distance(transform.position, interactableObj.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestInteractableObj = interactableObj;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractable interactable = collision.GetComponent<IInteractable>();

        if (interactable != null)
        {
            interactables.Add(collision.gameObject);
            SetClosestInteractable();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IInteractable interactable = collision.GetComponent<IInteractable>();

        if (interactable != null)
        {
            interactables.Remove(collision.gameObject);
            SetClosestInteractable();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        Gizmos.DrawSphere((Vector2)transform.position, interactCollider.radius);
    }
}
