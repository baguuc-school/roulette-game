using UnityEngine;

public class StartButtonHandler : MonoBehaviour
{
    public void HandleClick()
    {
        StateManager stateManager = StateManager.GetInstance();

        if (stateManager.Username.Length == 0)
        {
            Color initialColor = stateManager.usernameInputField.image.color;
            stateManager.usernameInputField.image.color = Color.red;

            return;
        } else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
        }
    }
}
