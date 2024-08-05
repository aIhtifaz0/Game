using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    public GameObject soundScreenPanel;  // Referensi ke panel UI untuk SoundScreen
    public GameObject mainUIPanel;       // Referensi ke panel UI utama yang akan disembunyikan
    public GameObject pauseButton;       // Referensi ke tombol Pause
    public GameObject resumeButton;      // Referensi ke tombol Resume
    public GameObject exitButton;        // Referensi ke tombol Exit

    private bool isPaused = false;       // Status Pause Game

    void Update()
    {
        // Periksa apakah mouse diklik
        if (Input.GetMouseButtonDown(0)) // 0 adalah tombol kiri mouse
        {
            // Membuat ray dari posisi klik mouse
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Melakukan raycast dan memeriksa apakah mengenai objek
            if (Physics.Raycast(ray, out hit))
            {
                // Memeriksa nama objek yang terkena raycast
                if (hit.transform.name == "PauseButton")
                {
                    PauseGame();
                }
                else if (hit.transform.name == "ResumeButton")
                {
                    ResumeGame();
                }
                else if (hit.transform.name == "SettingButton")
                {
                    ShowSoundScreen();
                }
                else if (hit.transform.name == "ExitButton")
                {
                    ExitToMainMenu();
                }
            }
        }

        // Periksa apakah tombol Esc ditekan
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMainUI();
        }
    }

    public void LoadStartMenu()
    {
        // Memuat scene StartMenu dan mengatur kembali timeScale menjadi normal
        SceneManager.LoadScene("StartMenu");
        Time.timeScale = 1f;
    }

    public void ShowSoundScreen()
    {
        // Menampilkan panel SoundScreen dan menyembunyikan panel UI utama
        if (soundScreenPanel != null && mainUIPanel != null)
        {
            soundScreenPanel.SetActive(true);
            mainUIPanel.SetActive(false);
        }
    }

    public void BackToMainUI()
    {
        // Menyembunyikan panel SoundScreen dan menampilkan panel UI utama
        if (soundScreenPanel != null && mainUIPanel != null)
        {
            soundScreenPanel.SetActive(false);
            mainUIPanel.SetActive(true);
        }
    }

    public void PauseGame()
    {
        if (!isPaused)
        {
            Time.timeScale = 0; // Menghentikan waktu permainan
            isPaused = true;
            pauseButton.SetActive(false); // Menyembunyikan tombol Pause
            resumeButton.SetActive(true); // Menampilkan tombol Resume
            exitButton.SetActive(true); // Menampilkan tombol Exit
        }
    }

    public void ResumeGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1; // Melanjutkan waktu permainan
            isPaused = false;
            pauseButton.SetActive(true); // Menampilkan tombol Pause
            resumeButton.SetActive(false); // Menyembunyikan tombol Resume
            exitButton.SetActive(false); // Menyembunyikan tombol Exit
        }
    }

    public void ExitToMainMenu()
    {
        // Kembali ke scene Main Menu dan mengatur kembali timeScale menjadi normal
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
}
