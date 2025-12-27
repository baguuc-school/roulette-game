using Shared;
using Shared.Models;
using UnityEngine;
using UnityEngine.UI;

namespace ListMenu
{
    public class Core : MonoBehaviour
    {
        // referencje z edytora
        public GameObject listButtonPrefab;
        public GameObject canvas;

        // instancja core (singleton)
        private static Core INSTANCE;

        public static Core GetInstance()
        {
            if(!INSTANCE)
            {
                var gameObject = GameObject.FindGameObjectWithTag("LevelManager");
                INSTANCE = gameObject.GetComponent<Core>();
            }

            return INSTANCE;
        }

        public void RenderRouletteList()
        {
            int currY = 100;
            foreach (RouletteWithoutItems entry in Context.RouletteList)
            {
                GameObject button = Instantiate(listButtonPrefab, transform);
                button.GetComponentInChildren<TMPro.TMP_Text>().text = entry.name;
                button.GetComponent<Button>().onClick.AddListener(() => {
                    Context.SelectedRouletteId = entry.Id;
                    UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
                });

                button.transform.localPosition = new Vector3(40, currY, 0);
                button.transform.SetParent(canvas.transform, true);

                currY -= 60;
            }
        }
    }
}
