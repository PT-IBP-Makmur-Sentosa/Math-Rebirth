using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using TMPro;

public class EnemyBehaviour : MonoBehaviour
{
    
    float startingX;
    public float speed = 1f;
    public float leftRange = 4f;
    public float rightRange = 0f;
    int dir = -1;
    int face = -1;
    bool trigger = false;

    Vector2 move;
    public int level;
    public AnimatorController m_anim;
    [SerializeField] int levelMin;
    [SerializeField] int levelMax;
    public GameObject spot;
    public GameObject infoText;
    public TMP_Text level_txt;
    // Start is called before the first frame update
    void Start()
    {
        startingX = gameObject.transform.position.x;
        level = UnityEngine.Random.Range(levelMin, levelMax);
        move = new Vector2(0, 0);
        level_txt.text = "lvl. " + level;
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
            if (transform.position.x < startingX - leftRange)
            {
                dir = -1;
                face = 1;
            }
            else if (transform.position.x > startingX + rightRange)
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
            spot.SetActive(true);
            infoText.SetActive(false);
            trigger = true;
            print("A");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spot.SetActive(false);
            infoText.SetActive(true);
            trigger = false;
            print("B");
        }
    }

    public void startIdle()
    {
        GetComponent<Animator>().Play("idle");
    }

    public void startWalk()
    {
        GetComponent<Animator>().Play("walk");
    }
}
