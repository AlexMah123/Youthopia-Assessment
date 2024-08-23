using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue Configs")]
    [SerializeField] GameObject dialogueBox;
    [SerializeField] TextMeshProUGUI speakerText;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] float dialogueDisplayTime = 2f;

    Coroutine dialogueCoroutine = null;

    private void Awake()
    {
        dialogueBox.SetActive(false);
    }

    private void OnEnable()
    {
        DialogueComponent[] dialogueComponents = FindObjectsOfType<DialogueComponent>(includeInactive: true);

        foreach(var component in dialogueComponents)
        {
            component.OnStartDialogue += HandleOnStartDialogue;
        }
    }

    private void OnDisable()
    {
        DialogueComponent[] dialogueComponents = FindObjectsOfType<DialogueComponent>(includeInactive: true);

        foreach (var component in dialogueComponents)
        {
            component.OnStartDialogue -= HandleOnStartDialogue;
        }
    }

    public void HandleOnStartDialogue(Dialogue dialogue)
    {
        if (dialogueCoroutine != null)
        {
            StopCoroutine(dialogueCoroutine);
        }

        dialogueBox.SetActive(true);
        speakerText.text = dialogue.speakerName;
        dialogueText.text = dialogue.dialogueLines;

        dialogueCoroutine = StartCoroutine(CloseDialogueBox());
    }

    IEnumerator CloseDialogueBox()
    {
        yield return new WaitForSeconds(dialogueDisplayTime);

        dialogueBox.SetActive(false);
        dialogueCoroutine = null;
    }
}
