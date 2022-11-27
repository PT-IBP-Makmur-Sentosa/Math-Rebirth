using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float runSpeed = 0.2f;
    public float jumpPower = 0.2f;
    float horizontalMove = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Input.GetKeyDown("w"))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpPower), ForceMode2D.Force);
        }
    }

    private void FixedUpdate()
    {
        if(horizontalMove < 0)
        {
            Quaternion rot = gameObject.transform.rotation;
            rot = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f);
            gameObject.transform.rotation = rot;
        }
        else if(horizontalMove > 0)
        {
            Quaternion rot = gameObject.transform.rotation;
            rot = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
            gameObject.transform.rotation = rot;
        }

        Vector2 pos = gameObject.transform.position;
        pos += new Vector2(horizontalMove, 0.0f);
        gameObject.transform.position = pos;
    }
}
