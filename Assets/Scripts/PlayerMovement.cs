using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
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
    public GameObject enemy;
    public RuntimeAnimatorController skeleton_animator;
    public Sprite skeleton_sprite;

    public RuntimeAnimatorController shade_animator;
    public Sprite shade_sprite;
    GameObject glob;
    GlobalControl globalcontrol;

    // Start is called before the first frame update
    void Start()
    {
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        glob = GameObject.Find("GlobalObject");
        globalcontrol = glob.GetComponent<GlobalControl>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Input.GetKeyDown("w"))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpPower), ForceMode2D.Force);
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
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Skeleton"))
        {
            enemy.GetComponent<SpriteRenderer>().sprite = skeleton_sprite;
            enemy.GetComponent<SpriteRenderer>().flipX = false;
            enemy.GetComponent<Animator>().runtimeAnimatorController = skeleton_animator;
            GameObject.Find("CombatManager").GetComponent<CombatManager>().StartCombat();
            StartCoroutine(Coroutine());
            print("Enemy Found");
        }
        if (collision.CompareTag("Shade"))
        {
            enemy.GetComponent<SpriteRenderer>().sprite = shade_sprite;
            enemy.GetComponent<SpriteRenderer>().flipX = true;
            enemy.GetComponent<Animator>().runtimeAnimatorController = shade_animator;
            GameObject.Find("CombatManager").GetComponent<CombatManager>().StartCombat();
            StartCoroutine(Coroutine());
            print("Enemy Found");
        }
        if(collision.CompareTag("Finish"))
        {
            Scene currScene = SceneManager.GetActiveScene();
            string sceneName = currScene.name;
            if(sceneName == "FirstStage")
            {
                globalcontrol.StageFinish(1);
            }
            if(sceneName == "SecondStage")
            {
                globalcontrol.StageFinish(2);
            }
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
        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}
