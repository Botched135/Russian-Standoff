using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace RussianStandOff
{
    public class GUIManager : MonoBehaviour
    {
        private GameObject[] _playersObj = new GameObject[4];
        private Player[] players = new Player[4];
        // Use this for initialization
        void Start()
        {
            _playersObj = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < _playersObj.Length; i++)
            {
                players[i] = _playersObj[i].GetComponent<Player>();
            }

        }


        void onGUI()
        {

        }
    }
}
