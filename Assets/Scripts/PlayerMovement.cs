using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float runSpeed = 0.1f;
    public float jumpPower = 400f;
    float horizontalMove = 0f;

    private bool trigger = false;
    //public GameObject CombatScene;
    public GameObject[] enemys;
    [SerializeField] CinemachineVirtualCamera walk_cam;
    [SerializeField] CinemachineVirtualCamera combat_cam;
    public Animator cm_cam1;
    public GameObject canvas_scroll;
    public GameObject tutorial_background;
    public GameObject move_tutorial;
    public GameObject inventory_tutorial;
    public GameObject combat_tutorial;


    // Start is called before the first frame update
    void Start()
    {
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        tutorial_background = GameObject.Find("TutorialBg");
        move_tutorial = GameObject.Find("MoveTutorial");
        inventory_tutorial = GameObject.Find("InventoryTutorial");
        combat_tutorial = GameObject.Find("inCombatTutorial");

        tutorial_background.SetActive(true);
        move_tutorial.SetActive(true);
        inventory_tutorial.SetActive(false);
        combat_tutorial.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("d") || Input.GetKeyDown("a") )
        {
            tutorial_background.SetActive(false);
            move_tutorial.SetActive(false);
            inventory_tutorial.SetActive(false);
        }

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

    
        if (Input.GetKeyDown("w"))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpPower), ForceMode2D.Force);
            tutorial_background.SetActive(false);
            move_tutorial.SetActive(false);
            inventory_tutorial.SetActive(false);
        }

        if (trigger)
        {
            canvas_scroll.SetActive(false);
            CameraSwitch.swithcam(combat_cam);
            foreach (GameObject enemy in enemys)
            {
                enemy.SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (horizontalMove < 0)
        {
            Quaternion rot = gameObject.transform.rotation;
            rot = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f);
            gameObject.transform.rotation = rot;
        }
        else if (horizontalMove > 0)
        {
            Quaternion rot = gameObject.transform.rotation;
            rot = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
            gameObject.transform.rotation = rot;
        }

        Vector2 pos = gameObject.transform.position;
        pos += new Vector2(horizontalMove, 0.0f);
        gameObject.transform.position = pos;
        if (trigger)
        {

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            GameObject.Find("CombatManager").GetComponent<CombatManager>().StartCombat();
            StartCoroutine(Coroutine());
            print("Enemy Found");
        }

        if(collision.gameObject.name == "Soul"){
            print("Soul collected");
            collision.gameObject.SetActive(false);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            trigger = false;
            cm_cam1.SetBool("enter", false);
        }
    }

    private void OnEnable()
    {
        CameraSwitch.register(walk_cam);
        CameraSwitch.register(combat_cam);
    }
    private void OnDisable()
    {
        CameraSwitch.unregister(walk_cam);
        CameraSwitch.unregister(combat_cam);
    }
    IEnumerator Coroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        cm_cam1.SetBool("enter", true);
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSecondsRealtime(1.6f);
        trigger = true;
        tutorial_background.SetActive(true);
        combat_tutorial.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        tutorial_background.SetActive(false);
        combat_tutorial.SetActive(false);
        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}
