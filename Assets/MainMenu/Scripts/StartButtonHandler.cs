using UnityEngine;

namespace MainMenu
{
    public class StartButtonHandler : MonoBehaviour
    {
        public void HandleClick()
        {
            StateManager stateManager = StateManager.GetInstance();

            if (Shared.Context.Username.Length == 0)
            {
                Color initialColor = stateManager.usernameInputField.image.color;
                stateManager.usernameInputField.image.color = Color.red;
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
            }
        }
    }
}