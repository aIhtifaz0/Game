using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    CapsuleCollider2D playerbc;
    public string sceneName;
    public Vector3 spawnPoint;
    SceneManagement scenemanagement;
    public bool doors = true;

    public SavePos savepos;
    public GameObject arrow;
    public Animator arrowanim;

    public GameObject player;
    private Animator playerAnim;
    public GameObject panel;
    public Animator panelanim;
    // Start is called before the first frame update
    void Start()
    {
        playerbc = GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider2D>();
        scenemanagement = GameObject.FindWithTag("SceneManagement").GetComponent<SceneManagement>();
        arrowanim = arrow.GetComponent<Animator>();
        playerAnim = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (playerbc.IsTouching(GetComponent<BoxCollider2D>()))
        // {
        //     Debug.Log("Touched");
        //     savepos.startpoint = spawnPoint;
        //     scenemanagement.GoTo(sceneName);

        // }

        panelanim = panel.GetComponent<Animator>();

        if (Input.GetKeyDown(KeyCode.E) && playerbc.IsTouching(GetComponent<BoxCollider2D>()) && doors)
        {
            Invoke("wait", 0.35f);
            playerAnim.SetTrigger("Open");
            
            Invoke("TP", 1f);
        }

        if (playerbc.IsTouching(GetComponent<BoxCollider2D>()) && !doors)
        {
            panelanim.SetTrigger("end");
            Invoke("TP", 0.5f);
        }

        if(arrow == null)
        {
            ;
        }

    }

    void wait()
    {
        panelanim.SetTrigger("end");
    }
    void TP()
    {
        
        savepos.startpoint = spawnPoint;
        scenemanagement.GoTo(sceneName);
        //SceneManager.LoadScene(sceneName);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && doors)
        {
            arrow.SetActive(true);

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && doors)
        {

            arrowanim.SetTrigger("Exit");
            Invoke("exit", 0.5f); 
        }
    }

    void exit()
    {
        arrow.SetActive(false);
    }
}
