using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Dialogue
{
    public string speakerName;
    public string dialogueLines;
}

public class DialogueComponent : MonoBehaviour
{
    [SerializeField] List<Dialogue> dialogues;

    public event Action<Dialogue> OnStartDialogue;

    //flag
    private bool isOnInteractBinded = false;

    private void OnEnable()
    {
        if(!isOnInteractBinded)
        {
            BindOnInteractEvent();
        }
    }

    private void OnDisable()
    {
        if(isOnInteractBinded)
        {
            UnbindOnInteractEvent();
        }
    }

    private void Start()
    {
        if (!isOnInteractBinded)
        {
            BindOnInteractEvent();
        }
    }

    void HandleOnInteract()
    {
        SFXManager.Instance.PlaySoundFXClip("DialogueStart", transform);
        var randomDialogeIndex = UnityEngine.Random.Range(0, dialogues.Count);

        OnStartDialogue?.Invoke(dialogues[randomDialogeIndex]);
    }

    void BindOnInteractEvent()
    {
        var interactbleComponent = GetComponent<InteractableComponent>();
        if (interactbleComponent)
        {
            interactbleComponent.OnInteract += HandleOnInteract;
            isOnInteractBinded = true;
        }
    }

    void UnbindOnInteractEvent()
    {
        var interactbleComponent = GetComponent<InteractableComponent>();
        if (interactbleComponent)
        {
            interactbleComponent.OnInteract -= HandleOnInteract;
            isOnInteractBinded = false;
        }
    }
}
