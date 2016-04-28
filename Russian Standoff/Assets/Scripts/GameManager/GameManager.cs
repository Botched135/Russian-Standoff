using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace RussianStandOff
{
    public class GameManager : MonoBehaviour
    {
        private MenuManager menuManager;
        private static GameManager _GM;
        [SerializeField]
        private GameObject playerPrefab;
        [SerializeField]
        private Transform[] spawnPoints;
        private bool gameStarted;
        public bool[] joinedPlayer;

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
            menuManager = GameObject.FindGameObjectWithTag("EditorOnly").GetComponent<MenuManager>();
            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {
            if (menuManager._currentState == MenuManager.MenuStates.AddingPlayers && Input.GetButtonDown("XboxOne_YButton"))
            {
                menuManager._currentState = MenuManager.MenuStates.InGame;
                SceneManager.LoadScene(1);
                StartCoroutine(intialSpawning());
            }
        }

        // Update is called once per frame
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

        IEnumerator intialSpawning()
        {
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < 4; i++)
            {
                if (joinedPlayer[i]) {
                    SpawnPlayers(i);
                }
            }
        }

    }
}
