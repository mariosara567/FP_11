using UnityEngine;

public class EscapeInput : MonoBehaviour
{
    public GameObject optionsPanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleOptionsPanel();

            if (optionsPanel.activeSelf)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    private void ToggleOptionsPanel()
    {
        optionsPanel.SetActive(!optionsPanel.activeSelf);
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        // Tambahkan kode lain yang ingin Anda jalankan saat permainan dijeda
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        // Tambahkan kode lain yang ingin Anda jalankan saat permainan dilanjutkan
    }
}
