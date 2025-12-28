using Shared;
using UnityEngine;

namespace MainMenu
{
    public class StartButtonHandler : MonoBehaviour
    {
        public void HandleClick()
        {
            Core core = Core.GetInstance();

            // nazwa u¿ytkownika lub adres url api nie zosta³a wprowadzona a nie mo¿e byæ pusta
            // wiêc poinformuj u¿ytkownika przez zmianê koloru pola na czerwony
            bool usernameEntered = Context.Username != null && Context.Username.Length > 0;
            bool apiUrlEntered = Context.BaseApiUrl != null && Context.BaseApiUrl.Length > 0;

            if (!usernameEntered)
                core.ToggleUsernameInputAlert(true);

            if (!apiUrlEntered)
                core.ToggleApiUrlInputAlert(true);

            // kontynuuj tylko jeœli wszystkie pola zosta³y poprawnie wype³nione
            if (usernameEntered && apiUrlEntered)
                UnityEngine.SceneManagement.SceneManager.LoadScene("ListMenu");
        }
    }
}