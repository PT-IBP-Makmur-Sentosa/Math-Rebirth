using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    public float runSpeed = 0.2f;
    public float jumpPower = 0.2f;
    float horizontalMove = 0f;

    bool hurt = false;
    bool death = false;
    private bool trigger = false;
    public GameObject CombatScene;
    public GameObject[] enemys;
    [SerializeField] CinemachineVirtualCamera walk_cam;
    [SerializeField] CinemachineVirtualCamera combat_cam;
    public Animator cm_cam1;
    

    // Start is called before the first frame update
    void Start()
    {
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Input.GetKeyDown("w"))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpPower), ForceMode2D.Force);
        }
        if(trigger)
        {  
            CameraSwitch.swithcam(combat_cam);
            foreach (GameObject enemy in enemys)
            {
                enemy.SetActive(false);
            }
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
        if(trigger)
        {
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Enemy"))
        {
            
            StartCoroutine(Coroutine());
            gameObject.GetComponent<Animator>().SetBool("hurt", true);
            hurt = true;
            print("hurt");
            gameObject.GetComponent<Animator>().SetBool("death", true);
            death = true;
            print("death");
            print("Enemy Found");
        }
        
    }
    private void OnEnable()
    {
        CameraSwitch.register(walk_cam);
        CameraSwitch.register(combat_cam);
    }
    private void OnDisable()
    {
        CameraSwitch.register(walk_cam);
        CameraSwitch.register(combat_cam);
    }
    IEnumerator Coroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        cm_cam1.SetBool("enter", true);
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSecondsRealtime(2);
        trigger = true;
        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}
