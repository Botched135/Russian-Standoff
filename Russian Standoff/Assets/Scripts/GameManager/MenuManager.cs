using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour {

    private enum MenuStates
    {
        PressStart,
        MainMenu,
        AddingPlayers,
        Option,

    }
    private MenuStates _currentState;
	// Use this for initialization
	void Start () {
        _currentState = MenuStates.PressStart;
        
	}
	
	// Update is called once per frame
	void Update () {
        if(_currentState == MenuStates.PressStart)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _currentState = MenuStates.MainMenu;
            }
        }
        else if (_currentState == MenuStates.MainMenu)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _currentState = MenuStates.AddingPlayers;
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                _currentState = MenuStates.PressStart;
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                Application.Quit();
            }
        }
        else if(_currentState == MenuStates.AddingPlayers)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SceneManager.LoadScene(1);
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                _currentState = MenuStates.MainMenu;
            }
        }
	}
}
