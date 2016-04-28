using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour {
    private int selectedMenu;
    private bool[] player = new bool[4];
    private enum MenuStates
    {
        PressStart,
        MainMenu,
        AddingPlayers,
        Option,

    }
    private MenuStates _currentState;
	
	void Start () {
        for (int i = 0; i < player.Length; i++)
        {
            player[i] = false;
        }
        _currentState = MenuStates.PressStart;
        selectedMenu = 0;
        
	}
	void Update () {
        if(_currentState == MenuStates.PressStart)
        {
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("XboxOne_StartButton") || Input.GetButtonDown("XboxOne_AButton"))
            {
                _currentState = MenuStates.MainMenu;
            }
        }
        else if (_currentState == MenuStates.MainMenu)
        {
            if((float)Input.GetAxis("XboxOne_Y_Axis_Left") == 1 || (float)Input.GetAxis("XboxOne_Y_Axis_Left") == -1){
                selectedMenu++;
            }
            if (selectedMenu % 2 == 0) {
                if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("XboxOne_StartButton") || Input.GetButtonDown("XboxOne_AButton"))
                {
                    _currentState = MenuStates.AddingPlayers;
                }
            }
            else if(selectedMenu %2 == 1)
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
        else if(_currentState == MenuStates.AddingPlayers)
        {
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("XboxOne_YButton"))
            {
                ActivatePlayers(player);
                SceneManager.LoadScene(1, LoadSceneMode.Single);
                //insert function so that it knows how many player that should be loaded
            }
            if (Input.GetButtonDown("XboxOne_AButton"))
            {
                player[0] = !player[0];
                //activate particles or smt
            }
            if (Input.GetButtonDown("XboxTwo_AButton"))
            {
                player[1] = !player[1];
            }
            if (Input.GetButtonDown("XboxThree_AButton"))
            {
                player[2] = !player[2];
            }
            if (Input.GetButtonDown("XboxFour_AButton"))
            {
                player[3] = !player[3];
            }
            if (Input.GetKeyDown(KeyCode.B) || Input.GetButtonDown("XboxOne_BButton"))
            {
                _currentState = MenuStates.MainMenu;
            }
        }
	}
    void ActivatePlayers(bool[] players)
    {

    }
}
