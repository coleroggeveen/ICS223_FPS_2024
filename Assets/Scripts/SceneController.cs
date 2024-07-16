using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] 
    private UIController ui;

    private int score = 0;

    [SerializeField]
    private Transform iguanaSpawnPt;

    [SerializeField] 
    private GameObject iguanaPrefab;

    [SerializeField]
    private GameObject enemyPrefab;

    private GameObject enemy;
    private int enemyCount = 4;
    private GameObject[] enemyArray;

    private GameObject iguana;
    private int iguanaCount = 8;
    private GameObject[] iguanaArray;

    private Vector3 spawnPoint = new Vector3(0, 0, 5);

    private int difficulty = 1;

    private void Start()
    {
        difficulty = GetDifficulty();

        ui.UpdateScore(score);

        // enemy array
        enemyArray = new GameObject[enemyCount];

        // iguana array
        iguanaArray = new GameObject[iguanaCount];

        // instatiate enemies
        // fill enemy array with enemies
        for (int i = 0; i <= enemyCount - 1 ; i++)
        {
            enemy = Instantiate(enemyPrefab) as GameObject;
            enemyArray[i] = enemy;
        }
        // instantiate iguanas
        // fill iguana array
        for (int y = 0; y <= iguanaCount - 1; y++)
        {
            iguana = Instantiate(iguanaPrefab) as GameObject;
            iguanaArray[y] = iguana;
            iguanaArray[y].transform.position = iguanaSpawnPt.position;
            float angle = Random.Range(0, 360);
            iguanaArray[y].transform.Rotate(0, angle, 0);
        }

        CurrentDifficulty(difficulty);
    }
    // Update is called once per frame
    void Update()
    {
        difficulty = GetDifficulty();

        for (int i = 0; i <= enemyCount - 1; i++)                        // temp commenting
        {
            if (enemyArray[i] == null)
            {

                enemyArray[i] = Instantiate(enemyPrefab) as GameObject;
                enemyArray[i].transform.position = spawnPoint;
                float angle = Random.Range(0, 360);
                enemyArray[i].transform.Rotate(0, angle, 0);
                CurrentDifficulty(difficulty);
            }
        }
    }

    private void Awake()
    {
        Messenger.AddListener(GameEvent.ENEMY_DEAD, OnEnemyDead);
        Messenger<int>.AddListener(GameEvent.DIFFICULTY_CHANGED, OnDifficultyChanged);
        Messenger.AddListener(GameEvent.PLAYER_DEAD, OnPlayerDead);
        Messenger.AddListener(GameEvent.RESTART_GAME, OnRestartGame);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_DEAD, OnEnemyDead);
        Messenger<int>.RemoveListener(GameEvent.DIFFICULTY_CHANGED, OnDifficultyChanged);
        Messenger.RemoveListener(GameEvent.PLAYER_DEAD, OnPlayerDead);
        Messenger.RemoveListener(GameEvent.RESTART_GAME, OnRestartGame);
    }

    private void OnEnemyDead()
    {
        score++;
        ui.UpdateScore(score);
    }

    private void OnDifficultyChanged(int newDifficulty)
    {
        Debug.Log("Scene.OnDifficultyChanged(" + newDifficulty + ")");
        for (int i = 0; i < enemyArray.Length; i++)
        {
            WanderingAI ai = enemyArray[i].GetComponent<WanderingAI>();
            ai.SetDifficulty(newDifficulty);
        }
    }

    public int GetDifficulty()
    {
        return PlayerPrefs.GetInt("difficulty", 1);
    }

    private void CurrentDifficulty(int difficulty)
    {
        OnDifficultyChanged(difficulty);
    }

    private void OnPlayerDead()
    {
        ui.ShowGameOverPopup();
    }

    public void OnRestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
