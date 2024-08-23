using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnMobile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

#if !UNITY_EDITOR
        bool isOnMobile = Application.isMobilePlatform;
        bool isOnWebGLMobile = isOnMobile && Application.platform == RuntimePlatform.WebGLPlayer;
        
        gameObject.SetActive(isOnMobile || isOnWebGLMobile);
#endif
    }
}
