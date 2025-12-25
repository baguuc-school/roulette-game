using UnityEngine;

namespace MainMenu
{
    public class ExitButtonHandler : MonoBehaviour
    {
        public void HandleClick()
        {
            Application.Quit();
        }
    }
}