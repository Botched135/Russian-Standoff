using UnityEngine;
using System.Collections;

namespace RussianStandOff
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _GM;
        [SerializeField]
        private GameObject playerPrefab;
        [SerializeField]
        private Transform[] spawnPoints;
        private bool gameStarted;
        // Use this for initialization

        public static bool isActive {
            get
            {
                return _GM != null;
            }
        }
        public static GameManager GM
        {
            get
            {
                if (_GM == null)
                {
                    _GM = Object.FindObjectOfType(typeof(GameManager)) as GameManager;
                    if (_GM == null) {
                        GameObject go = new GameObject("GameManager");
                        DontDestroyOnLoad(go);
                        _GM = go.AddComponent<GameManager>();
                    }
                }
                return _GM;
            }
        }
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void SpawnPlayers(int i) //spawn All active players
        {
            Vector3 spawnPosition = spawnPoints[i].position;

            GameObject obj = Instantiate(playerPrefab, spawnPosition, playerPrefab.transform.rotation) as GameObject;
            obj.GetComponent<Player>().playerNum = i;
        }

        public void RespawnPlayer(Player player, float time = 0) //respawn player without delay
        {
            if (time == 0)
            {

            }
            else
                StartCoroutine(RespawnPlayerWithDelay(player, time));

        }
        public IEnumerator RespawnPlayerWithDelay(Player player, float time)
        {
            player.gameObject.SetActive(false);
            Vector2 spawnPosition = spawnPoints[Random.Range(0,spawnPoints.Length)].position;
            yield return new WaitForSeconds(2f);
            player.gameObject.transform.position = spawnPosition;
            player.gameObject.SetActive(true);
        }

    }
}
