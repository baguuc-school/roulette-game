using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class StateManager : MonoBehaviour
    {
        private static StateManager INSTANCE;

        public TMP_InputField usernameInputField;
        public Button startButton;
        public Button exitButton;

        public static StateManager GetInstance()
        {
            if (!INSTANCE)
            {
                var gameObject = GameObject.FindGameObjectWithTag("UIManager");
                INSTANCE = gameObject.GetComponent<StateManager>();
            }

            return INSTANCE;
        }
    }

}