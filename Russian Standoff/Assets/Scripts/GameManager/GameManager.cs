using UnityEngine;
using System.Collections;

namespace RussianStandOff
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _GM;

        private bool gameStarted;
        // Use this for initialization
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void SpawnPlayers(int amountOfPlayers) //spawn All active players
        {

        }

        public void RespawnPlayer(Player player, float time = 0) //respawn player without delay
        {

        }
        public IEnumerator RespawnPlayerWithDelay(Player player, float time)
        {
            player.gameObject.SetActive(false);
            Vector2 spawnPosition = new Vector2(0, 0);
            yield return new WaitForSeconds(2f);
            player.gameObject.transform.position = spawnPosition;
            player.gameObject.SetActive(true);
        }
    }
}
