using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class Core : MonoBehaviour
    {
        // referencje z edytora
        public TMP_InputField usernameInputField;
        public TMP_InputField apiUrlInputField;
        public Button startButton;
        public Button exitButton;

        // instancja core (singleton)
        private static Core INSTANCE;

        public static Core GetInstance()
        {
            if (!INSTANCE)
            {
                var gameObject = GameObject.FindGameObjectWithTag("LevelManager");
                INSTANCE = gameObject.GetComponent<Core>();
            }

            return INSTANCE;
        }

        /// <summary>
        /// W³¹cza lub wy³¹cza alert koloru czerwonego na polu wprowadzania nazwy u¿ytkownika.
        /// </summary>
        /// <param name="enabled">Oczekiwany stan</param>
        public void ToggleUsernameInputAlert(bool enabled)
        {
            if (enabled && usernameInputField.image.color != Color.red)
            {
                usernameInputField.image.color = Color.red;
            }
            else if(!enabled && usernameInputField.image.color == Color.red)
            {
                usernameInputField.image.color = new Color(0.0274509803921569f, 0.0274509803921569f, 0.0274509803921569f);
            }
        }

        /// <summary>
        /// W³¹cza lub wy³¹cza alert koloru czerwonego na polu wprowadzania URL API.
        /// </summary>
        /// <param name="enabled">Oczekiwany stan</param>
        public void ToggleApiUrlInputAlert(bool enabled)
        {
            if (enabled && apiUrlInputField.image.color != Color.red)
            {
                apiUrlInputField.image.color = Color.red;
            }
            else if (!enabled && apiUrlInputField.image.color == Color.red)
            {
                apiUrlInputField.image.color = new Color(0.0274509803921569f, 0.0274509803921569f, 0.0274509803921569f);
            }
        }
    }

}