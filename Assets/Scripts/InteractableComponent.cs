using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableComponent : MonoBehaviour, IInteractable
{
    [Header("InteractPrompt Config")]
    [SerializeField] Vector2 promptOffset;

    public void ShowInteractPrompt(GameObject interactUIPrompt)
    {
        interactUIPrompt.gameObject.SetActive(true);
        interactUIPrompt.transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.TransformPoint((Vector3)promptOffset));
    }

    public virtual void Interact()
    {
        Debug.Log("Being Interacted");
    }


}
