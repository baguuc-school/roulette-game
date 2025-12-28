using UnityEngine;

namespace MainMenu
{
    public class ApiUrlInputHandler : MonoBehaviour
    {
        // prywatny stan
        private Core core => Core.GetInstance();

        public void HandleEndEdit(string input)
        {
            Shared.Context.BaseApiUrl = input.TrimEnd('/');
        }

        public void HandleSelect(string selection)
        {
            if (TouchScreenKeyboard.isSupported)
            {
                TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
            }
            core.ToggleApiUrlInputAlert(false);
        }
    }
}