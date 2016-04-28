using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace RussianStandOff {
    public class MenuManager : MonoBehaviour {
        private int selectedMenu;
        public enum MenuStates
        {
            PressStart,
            MainMenu,
            AddingPlayers,
            InGame,

        }
        public MenuStates _currentState;

        void Start() {
            
            _currentState = MenuStates.PressStart;

            GameManager.GM.joinedPlayer = new bool[4];

            
            
            selectedMenu = 0;

        }
        void Update() {
            Debug.Log(_currentState);
            if (_currentState == MenuStates.PressStart)
            {
                if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("XboxOne_StartButton") || Input.GetButtonDown("XboxOne_AButton"))
                {
                    
                    _currentState = MenuStates.MainMenu;
                }
            }
            else if (_currentState == MenuStates.MainMenu)
            {
                if ((float)Input.GetAxis("XboxOne_Y_Axis_Left") == 1 || (float)Input.GetAxis("XboxOne_Y_Axis_Left") == -1) {
                    selectedMenu++;
                }
                if (selectedMenu % 2 == 0) {
                    if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("XboxOne_StartButton") || Input.GetButtonDown("XboxOne_AButton"))
                    {
                        Debug.Log(_currentState);
                        _currentState = MenuStates.AddingPlayers;

                    }
                }
                else if (selectedMenu % 2 == 1)
                {
                    if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("XboxOne_StartButton") || Input.GetButtonDown("XboxOne_AButton"))
                    {
                        Application.Quit();
                    }

                }
                else if (Input.GetKeyDown(KeyCode.B) || Input.GetButtonDown("XboxOne_BButton"))
                {
                    _currentState = MenuStates.PressStart;
                }

            }
            else if (_currentState == MenuStates.AddingPlayers)
            {
                
                if (Input.GetButtonDown("XboxOne_AButton"))
                {
                    GameManager.GM.joinedPlayer[0] = !GameManager.GM.joinedPlayer[0];
                }
                if (Input.GetButtonDown("XboxTwo_AButton"))
                {
                    GameManager.GM.joinedPlayer[1] = !GameManager.GM.joinedPlayer[1];
                }
                if (Input.GetButtonDown("XboxThree_AButton"))
                {
                    GameManager.GM.joinedPlayer[2] = !GameManager.GM.joinedPlayer[2];
                }
                if (Input.GetButtonDown("XboxFour_AButton"))
                {
                    GameManager.GM.joinedPlayer[3] = !GameManager.GM.joinedPlayer[3];
                }
                if (Input.GetKeyDown(KeyCode.B) || Input.GetButtonDown("XboxOne_BButton"))
                {
                    _currentState = MenuStates.MainMenu;
                }
            }
        }
    }
}
