using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonController : MonoBehaviour
{
    public static MenuButtonController Instance;

    public int index;
    [SerializeField] bool keyDown;
    [SerializeField] int maxIndex;
    public AudioSource audioSource;
    public Button[] menuButtons; // Array untuk menampung tombol-tombol menu

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            if (!keyDown)
            {
                if (Input.GetAxis("Vertical") < 0)
                {
                    if (index < maxIndex)
                    {
                        index++;
                    }
                    else
                    {
                        index = 0;
                    }
                }
                else if (Input.GetAxis("Vertical") > 0)
                {
                    if (index > 0)
                    {
                        index--;
                    }
                    else
                    {
                        index = maxIndex;
                    }
                }
            }
        }

        // Tambahkan logika lain jika diperlukan untuk mengatur navigasi keyboard
    }

    public void SelectButton(int buttonIndex)
    {
        index = buttonIndex;
        // Tambahkan logika untuk mengeksekusi tindakan tombol jika diperlukan
        audioSource.Play();
        Debug.Log("Button " + buttonIndex + " clicked!");
        // Misalnya, kamu bisa memanggil method lain berdasarkan index
    }
}
