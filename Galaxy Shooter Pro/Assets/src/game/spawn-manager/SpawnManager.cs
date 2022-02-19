using System;
using System.Collections;
using UnityEngine;
using vio.spaceshooter.game.enemy;

namespace vio.spaceshooter.game.spawnmanager
{
  public class SpawnManager : MonoBehaviour
  {
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private GameObject[] powerupPrefabs;

    [SerializeField]
    private GameObject astroidPrefab;

    private const float MAX_Y_POS = 12f;
    private const float MIN_Y_POS = -2.5f;
    private const float MAX_X_POS = 10.55f;
    private const float MIN_X_POS = -10.55f;

    private const float MAX_ENEMY_SPAWN_SPEED = 2f;
    private const float MIN_ENEMY_SPAWN_SPEED = 0.5f;

    private const ushort MAX_ENEMIES = 20;

    private const float MAX_POWERUP_SPAWN_SPEED = 10f;
    private const float MIN_POWERUP_SPAWN_SPEED = 7f;

    private const ushort MAX_ASTROIDS = 2;

    private const float MAX_ASTROID_SPAWN_SPEED = 10f;
    private const float MIN_ASTROID_SPAWN_SPEED = 2f;

    private int difficultyLevel = 0;

    GameObject astroidContainer;

    GameObject enemyContainer;
    GameObject powerupContainer;

    IEnumerator enemySpawner;
    IEnumerator powerupSpawner;
    IEnumerator astroidSpawner;

    private bool isSpawningActive = false;

    void Start()
    {

      this.startSpawning(); // Setting this after starting the co-routine doesnt work.
      this.initEnemySpawning();
      this.initPowerupsSpawning();
      this.initAstroidSpawning();
    }

    public void startSpawning()
    {
      this.isSpawningActive = true;
    }

    private void initEnemySpawning()
    {
      this.enemyContainer = this.createContainer("Enemies");
      this.enemySpawner = SpawnEnemiesRoutine();
      StartCoroutine(this.enemySpawner);
    }

    private GameObject createContainer(string tagName)
    {
      GameObject enemyContainer = new GameObject();
      enemyContainer.name = tagName;
      enemyContainer.transform.parent = this.transform;
      return enemyContainer;
    }

    IEnumerator SpawnEnemiesRoutine()
    {
      while (this.isSpawningActive)
      {
        if (this.enemyContainer.transform.childCount < MAX_ENEMIES)
        {
          if (this.difficultyLevel == 3)
          {
            GameObject enemyInstance = this.spawnPrefabTop(this.enemyPrefab, this.enemyContainer);
            if (UnityEngine.Random.Range(1, 10) > 3)
            {
              enemyInstance.GetComponent<Enemy>().SetDifficultyLevel(this.difficultyLevel);
            }
            enemyInstance = this.spawnPrefabTop(this.enemyPrefab, this.enemyContainer);
            if (UnityEngine.Random.Range(1, 10) > 3)
            {
              enemyInstance.GetComponent<Enemy>().SetDifficultyLevel(this.difficultyLevel);
            }
          }
          else
          {
            GameObject enemyInstance = this.spawnPrefabTop(this.enemyPrefab, this.enemyContainer);
            if (UnityEngine.Random.Range(1, 10) > 3)
            {
              enemyInstance.GetComponent<Enemy>().SetDifficultyLevel(this.difficultyLevel);
            }
          }
          //yield return new WaitForSeconds(MAX_ENEMY_SPAWN_SPEED*5000);
          if (this.difficultyLevel == 0)
          {
            yield return new WaitForSeconds(UnityEngine.Random.Range(MIN_ENEMY_SPAWN_SPEED, MAX_ENEMY_SPAWN_SPEED));
          }
          if (this.difficultyLevel == 1)
          {
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.25f, MAX_ENEMY_SPAWN_SPEED));
          }
          if (this.difficultyLevel == 2)
          {
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.25f, 1f));
          }
          if (this.difficultyLevel == 3)
          {
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.25f, 0.8f));
          }
        }
        else
        {
          yield return new WaitForSeconds(0.25f);
        }
      }
    }

    private GameObject spawnPrefabTop(GameObject prefab, GameObject container)
    {
      GameObject spawnedInstance = MonoBehaviour.Instantiate(
                  prefab,
                  getRandomTopOfScreenPositionVector(),
                  Quaternion.identity
                );
      spawnedInstance.transform.parent = container.transform;
      return spawnedInstance;
    }
    private Vector3 getRandomTopOfScreenPositionVector()
    {
      Vector3 position;
      do
      {
        position = new Vector3(UnityEngine.Random.Range(MIN_X_POS, MAX_X_POS), MAX_Y_POS);
      } while (this.isCollidingWithEnemy(position));
      return position;
    }

    private bool isCollidingWithEnemy(Vector3 position)
    {
      foreach (Enemy enemy in this.enemyContainer.GetComponentsInChildren<Enemy>())
      {
        if (enemy.transform.position.y > 10)
        {
          float distance = Vector3.Distance(enemy.transform.position, position);
          //Debug.Log("distance:" + distance);
          if ((distance < 2) && (distance > -2))
          {
            //Debug.Log("collision detected");
            return true;
          }
        }

      }
      return false;
    }

    private void initPowerupsSpawning()
    {
      this.powerupContainer = this.createContainer("Powerups");
      this.powerupSpawner = SpawnPowerupsRoutine();
      StartCoroutine(this.powerupSpawner);
    }

    IEnumerator SpawnPowerupsRoutine()
    {
      yield return new WaitForSeconds(UnityEngine.Random.Range(MIN_POWERUP_SPAWN_SPEED, MAX_POWERUP_SPAWN_SPEED));
      while (this.isSpawningActive)
      {
        int powerupNumber = UnityEngine.Random.Range(0, 3);
        GameObject powerupPrefab = powerupPrefabs[powerupNumber];
        this.spawnPrefabTop(powerupPrefab, this.powerupContainer);
        yield return new WaitForSeconds(UnityEngine.Random.Range(MIN_POWERUP_SPAWN_SPEED, MAX_POWERUP_SPAWN_SPEED));
      }
    }

    private void initAstroidSpawning()
    {
      this.astroidContainer = this.createContainer("Astroids");
      this.astroidSpawner = SpawnAstroidsRoutine();
      StartCoroutine(this.astroidSpawner);
    }

    IEnumerator SpawnAstroidsRoutine()
    {
      yield return new WaitForSeconds(UnityEngine.Random.Range(MIN_ASTROID_SPAWN_SPEED, MAX_ASTROID_SPAWN_SPEED));
      while (this.isSpawningActive)
      {
        if ((this.astroidContainer.transform.childCount < MAX_ASTROIDS) && (this.difficultyLevel > 1))
        {
          this.spawnPrefabTop(this.astroidPrefab, this.astroidContainer);
        }
        yield return new WaitForSeconds(UnityEngine.Random.Range(MIN_ASTROID_SPAWN_SPEED, MAX_ASTROID_SPAWN_SPEED));
      }
    }

    public void stopSpawning()
    {
      this.isSpawningActive = false;
    }

    public void Reset()
    {
      StopCoroutine(this.enemySpawner);
      StopCoroutine(this.powerupSpawner);
      Destroy(this.enemyContainer.gameObject);
      this.Start();
    }

    public void SetDifficulty(int difficultyLevel)
    {
      this.difficultyLevel = difficultyLevel;
    }

    void Update()
    {
      this.moveEnemiesBellowCutOfPointUp();
    }

    private void moveEnemiesBellowCutOfPointUp()
    {
      foreach (Enemy enemyBeingMoved in this.enemyContainer.GetComponentsInChildren<Enemy>())
      {
        if (enemyBeingMoved.transform.position.y < MIN_Y_POS)
        {
          this.moveEnemyToRandomXLocationAtTop(enemyBeingMoved);
        }
      }
    }
    private void moveEnemyToRandomXLocationAtTop(Enemy enemy)
    {
      enemy.transform.position = getRandomTopOfScreenPositionVector();
      enemy.RandomizeCyclePosition();
    }
  }
}