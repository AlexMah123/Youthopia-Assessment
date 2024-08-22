using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerComponent : MonoBehaviour
{
    [SerializeField] Transform playerToFollow;

    private void Update()
    {
        gameObject.transform.position = new(playerToFollow.position.x, playerToFollow.position.y, transform.position.z);
    }
}
