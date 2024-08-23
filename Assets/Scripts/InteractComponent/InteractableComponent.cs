using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableComponent : MonoBehaviour, IInteractable
{
    [Header("InteractPrompt Config")]
    [SerializeField] Vector2 promptOffset;

    public event Action<GameObject> OnAbleToInteract;
    public event Action OnInteract;

    public void ShowInteractPrompt(GameObject interactor, GameObject interactUIPrompt)
    {
        interactUIPrompt.gameObject.SetActive(true);
        interactUIPrompt.transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.TransformPoint((Vector3)promptOffset));
        OnAbleToInteract?.Invoke(interactor);
    }

    public virtual void Interact()
    {
        OnInteract?.Invoke();
    }


}
