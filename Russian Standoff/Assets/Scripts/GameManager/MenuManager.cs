using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace RussianStandOff {
    public class MenuManager : MonoBehaviour {
        private bool selectedMenu;
        private float timer;
        private bool[] triggers = new bool[4];
        public GameObject[] Menus = new GameObject[4];
        public GameObject[] ActivePlayer = new GameObject[4];

       
        public enum MenuStates
        {
            PressStart,
            MainMenu,
            AddingPlayers,
            InGame,

        }
        public MenuStates _currentState;

        void Start() {

            foreach (GameObject obj in ActivePlayer)
            {
                obj.SetActive(false);
            }
            for (int i = 0; i < triggers.Length; i++)
            {
                triggers[i] = false;

            }
            for (int i = 1; i < Menus.Length; i++)
            {
                Menus[i].SetActive(false);
            }
            _currentState = MenuStates.PressStart;

            GameManager.GM.joinedPlayer = new bool[4];


            timer = 0;
            selectedMenu = false;

        }
        void Update() {

            timer += Time.deltaTime;
            if (_currentState == MenuStates.PressStart)
            {
                Menus[0].SetActive(true);
                if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("XboxOne_StartButton") || Input.GetButtonDown("XboxOne_AButton"))
                {
                    Menus[0].SetActive(false);
                   
                    
                    _currentState = MenuStates.MainMenu;
                }
            }
            else if (_currentState == MenuStates.MainMenu)
            {
                if (timer > 0.5f && ((float)Input.GetAxis("XboxOne_Y_Axis_Left") == 1 || (float)Input.GetAxis("XboxOne_Y_Axis_Left") == -1)) {
                    timer = 0;
                    selectedMenu = !selectedMenu;
                    
                }
                if (!selectedMenu) {
                    Menus[2].SetActive(false);
                    Menus[1].SetActive(true);
                    if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("XboxOne_StartButton") || Input.GetButtonDown("XboxOne_AButton"))
                    {
                        Menus[1].SetActive(false);
                        Menus[3].SetActive(true);
                        _currentState = MenuStates.AddingPlayers;

                    }
                }
                else if (selectedMenu)
                {
                    Menus[1].SetActive(false);
                    Menus[2].SetActive(true);
                    if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("XboxOne_StartButton") || Input.GetButtonDown("XboxOne_AButton"))
                    {
                        Application.Quit();
                    }

                }
                else if (Input.GetKeyDown(KeyCode.B) || Input.GetButtonDown("XboxOne_BButton"))
                {
                    Menus[1].SetActive(false);
                    Menus[2].SetActive(false);
                    _currentState = MenuStates.PressStart;
                }

            }
            else if (_currentState == MenuStates.AddingPlayers)
            {
                
                if (Input.GetButtonDown("XboxOne_AButton") || Input.GetKeyDown(KeyCode.Alpha1))
                {
                    GameManager.GM.joinedPlayer[0] = !GameManager.GM.joinedPlayer[0];
                    triggers[0] = !triggers[0];
                    ActivePlayer[0].SetActive(triggers[0]);
                    
                }
                if (Input.GetButtonDown("XboxTwo_AButton") || Input.GetKeyDown(KeyCode.Alpha2))
                {
                    GameManager.GM.joinedPlayer[1] = !GameManager.GM.joinedPlayer[1];
                    triggers[1] = !triggers[1];
                    ActivePlayer[1].SetActive(triggers[1]);
                }
                if (Input.GetButtonDown("XboxThree_AButton") || Input.GetKeyDown(KeyCode.Alpha3))
                {
                    GameManager.GM.joinedPlayer[2] = !GameManager.GM.joinedPlayer[2];
                    triggers[2] = !triggers[2];
                    ActivePlayer[2].SetActive(triggers[2]);
                }
                if (Input.GetButtonDown("XboxFour_AButton") || Input.GetKeyDown(KeyCode.Alpha4))
                {
                    GameManager.GM.joinedPlayer[3] = !GameManager.GM.joinedPlayer[3];
                    triggers[3] = !triggers[3];
                    ActivePlayer[3].SetActive(triggers[3]);
                }
                if (Input.GetKeyDown(KeyCode.B) || Input.GetButtonDown("XboxOne_BButton"))
                {
                    _currentState = MenuStates.MainMenu;
                }
            }
        }
    }
}
