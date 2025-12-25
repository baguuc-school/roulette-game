using UnityEngine;

namespace MainMenu
{
    public class UsernameInputHandler : MonoBehaviour
    {
        public void HandleEndEdit(string input)
        {
            StateManager state = StateManager.GetInstance();
            Shared.Context.Username = input;
        }

        public void HandleSelect(string selection)
        {
            StateManager state = StateManager.GetInstance();

            if (state.usernameInputField.image.color == Color.red)
            {
                state.usernameInputField.image.color = new Color(0.02f, 0.02f, 0.02f);
            }
        }
    }
}