using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace RussianStandOff
{
    public class GUIManager : MonoBehaviour
    {
        private GameObject[] _playersObj = new GameObject[4];
        private Shooting[] players = new Shooting[4];
        // Use this for initialization
        void Start()
        {
            _playersObj = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < _playersObj.Length; i++)
            {
                players[i] = _playersObj[i].GetComponent<Shooting>();
            }

        }


        void onGUI()
        {
            Debug.Log("YO");
            GUI.Box(new Rect(10, 10, 100, 100), "Stalin Score: " + players[0]._score);

        }
    }
}
