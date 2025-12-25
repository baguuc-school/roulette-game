using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MainScene
{
    public class StateManager : MonoBehaviour
    {
        private static StateManager INSTANCE;
        public string BASE_API_URL;

        public static StateManager GetInstance()
        {
            if(!INSTANCE)
            {
                var gameObject = GameObject.FindGameObjectWithTag("UIManager");
                INSTANCE = gameObject.GetComponent<StateManager>();
            }

            return INSTANCE;
        }

        public string Username { get; set; } = "";
    }
}
