using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject pauseMenu;
    public PauseButton pauseButtonScript;  // Referensi ke script PauseButton

    // Membuat variabel isPaused non-static agar dapat diakses dari luar kelas ini
    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        // Pastikan menu pause tersembunyi saat permainan dimulai
        pauseMenu.SetActive(false);
        // Set isPaused ke false saat permainan dimulai
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Jika tombol Escape ditekan
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // Jika sedang di SoundScreen, kembali ke mainUIPanel
            if (pauseButtonScript != null && pauseButtonScript.soundScreenPanel.activeSelf)
            {
                pauseButtonScript.BackToMainUI();
            }
            else
            {
                // Toggle menu pause
                TogglePauseMenu();
            }
        }
    }

    // Fungsi untuk menampilkan atau menyembunyikan menu pause dan mengatur waktu permainan
    void TogglePauseMenu()
    {
        // Jika permainan sedang di-pause, lanjutkan permainan
        if(isPaused)
        {
            ResumeGame();
        }
        // Jika permainan tidak di-pause, pause permainan
        else
        {
            PauseGame();
        }
    }

    // Fungsi untuk mem-pause permainan
    public void PauseGame()
    {
        // Tampilkan menu pause
        pauseMenu.SetActive(true);
        // Hentikan waktu permainan
        Time.timeScale = 0f;
        // Set isPaused ke true
        isPaused = true;
    }

    // Fungsi untuk melanjutkan permainan
    public void ResumeGame()
    {
        // Sembunyikan menu pause
        pauseMenu.SetActive(false);
        // Lanjutkan waktu permainan
        Time.timeScale = 1f;
        // Set isPaused ke false
        isPaused = false;
    }
}
