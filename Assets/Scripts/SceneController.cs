using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    private GameObject enemy;
    private int enemyCount = 4;
    private GameObject[] enemyArray;
    private Vector3 spawnPoint = new Vector3(0, 0, 5);

    private void Start()
    {
        // array
        enemyArray = new GameObject[enemyCount];

        // fill array with enemies
        for (int i = 0; i <= enemyCount - 1 ; i++)
        {
            enemy = Instantiate(enemyPrefab) as GameObject;
            enemyArray[i] = enemy;
        }
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i <= enemyCount - 1; i++)
        {
            if (enemyArray[i] == null)
            {
                enemyArray[i] = Instantiate(enemyPrefab) as GameObject;
                enemyArray[i].transform.position = spawnPoint;
                float angle = Random.Range(0, 360);
                enemyArray[i].transform.Rotate(0, angle, 0);
            }
        }

        //if (enemy == null)
        //{
         //   enemy = Instantiate(enemyPrefab) as GameObject;
         //   enemy.transform.position = spawnPoint;
          //  float angle = Random.Range(0, 360);
          //  enemy.transform.Rotate(0, angle, 0);
        //}
    }
}
