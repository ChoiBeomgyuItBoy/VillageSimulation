using UnityEngine;

namespace ArtGallery.UI
{
    public class ToggleUI : MonoBehaviour
    {
        [SerializeField] KeyCode toggleInput;
        [SerializeField] GameObject uiContainer;

        void Start()
        {
            uiContainer.SetActive(false);
        }

        void Update()
        {
            if(Input.GetKeyDown(toggleInput))
            {
                Toggle();
            }
        }

        void Toggle()
        {
            uiContainer.SetActive(!uiContainer.activeSelf);
        }
    }
}
