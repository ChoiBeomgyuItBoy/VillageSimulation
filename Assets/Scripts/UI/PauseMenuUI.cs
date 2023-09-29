using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ArtGallery.Core;

namespace ArtGallery.UI
{
    public class PauseMenuUI : MonoBehaviour
    {
        [SerializeField] GameObject pauseContainer;
        [SerializeField] Button restartButton;
        [SerializeField] Button quitButton;
        Clock clock;

        void Awake()
        {
            clock = FindObjectOfType<Clock>();
        }

        void Start()
        {
            restartButton.onClick.AddListener(RestartScene);
            quitButton.onClick.AddListener(Quit);
        }

        void OnEnable()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }

        void OnDisable()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = clock.GetTimeScale();
        }

        void RestartScene()
        {
            SceneManager.LoadScene(0);
        }

        void Quit()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }   
}
