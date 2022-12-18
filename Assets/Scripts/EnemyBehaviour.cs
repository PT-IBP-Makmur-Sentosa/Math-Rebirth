using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    float startingX;
    float speed = 1f;
    float range = 4f;
    int dir = -1;
    int face = -1;
    bool trigger = false;

    Vector2 move;
    public int level;
    [SerializeField] int levelMin;
    [SerializeField] int levelMax;
    // Start is called before the first frame update
    void Start()
    {
        startingX = gameObject.transform.position.x;
        level = UnityEngine.Random.Range(levelMin, levelMax);
        move = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (face == -1)
        {
            Quaternion rot = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
            transform.rotation = rot;
        }
        else if (face == 1)
        {
            Quaternion rot = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f);
            transform.rotation = rot;
        }
    }

    private void FixedUpdate()
    {
        if (!trigger)
        {
            move = Vector2.right * speed * Time.deltaTime * dir;
            transform.Translate(move);
            // print(move);
            if (transform.position.x < startingX - range)
            {
                dir = -1;
                face = 1;
            }
            else if (transform.position.x > startingX)
            {
                dir = -1;
                face = -1;
            }
        }
        else if(trigger)
        {
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.GetComponent<Animator>().SetBool("Triggered", true);
            trigger = true;
            print("A");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.GetComponent<Animator>().SetBool("Triggered", false);
            trigger = false;
            print("B");
        }
    }
}
