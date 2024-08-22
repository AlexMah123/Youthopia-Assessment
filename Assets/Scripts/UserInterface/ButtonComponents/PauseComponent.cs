using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PauseComponent : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    void Start()
    {
        if(button == null)
        {
            throw new MissingReferenceException("Button ref is not assigned");
        }

        button.onClick.AddListener(() =>
        { 
            if(TimeManager.isTimePaused)
            {
                TimeManager.ResumeTime();
            }
            else
            {
                TimeManager.PauseTime();
            }
        });
    }

}
