using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{

    float startingX;
    public float speed = 1f;
    public float leftRange = 4f;
    public float rightRange = 0f;
    int dir = -1;

    Vector2 move;

    void Start()
    {
        startingX = gameObject.transform.position.x;
        move = new Vector2(0, 0);
    }
    void FixedUpdate()
    {
        move = Vector2.right * speed * Time.deltaTime * dir;
        transform.Translate(move);
        // print(move);
        if (transform.position.x < startingX - leftRange)
        {
            dir = 1;
        }
        else if (transform.position.x > startingX + rightRange)
        {
            dir = -1;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}

