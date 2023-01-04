using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform objectToFollow;

    float offset = -0.7f;
    
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        
    }
    void Update()
    {
        transform.position = new Vector3(objectToFollow.position.x , objectToFollow.position.y/2 + offset, transform.position.z);
    }
}
