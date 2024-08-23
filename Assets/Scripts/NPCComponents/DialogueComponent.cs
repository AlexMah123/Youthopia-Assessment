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

    private void OnEnable()
    {
        GetComponent<InteractableComponent>().OnInteract += HandleOnInteract;
    }

    private void OnDisable()
    {
        GetComponent<InteractableComponent>().OnInteract -= HandleOnInteract;
    }

    void HandleOnInteract()
    {
        var randomDialogeIndex = UnityEngine.Random.Range(0, dialogues.Count);

        OnStartDialogue?.Invoke(dialogues[randomDialogeIndex]);
    }
}
