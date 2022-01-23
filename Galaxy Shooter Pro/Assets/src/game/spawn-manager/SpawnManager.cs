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

    private const float MAX_Y_POS = 12f;
    private const float MAX_X_POS = 10.00f;
    private const float MIN_X_POS = -9.75f;

    private const float MAX_ENEMY_SPAWN_SPEED = 2f;
    private const float MIN_ENEMY_SPAWN_SPEED = 0.5f;

    private const float MAX_POWERUP_SPAWN_SPEED = 10f;
    private const float MIN_POWERUP_SPAWN_SPEED = 7f;

    private int difficultyLevel = 0;

    GameObject enemyContainer;
    GameObject powerupContainer;

    IEnumerator enemySpawner;
    IEnumerator powerupSpawner;

    private bool isSpawningActive = false;

    void Start()
    {
      //Setting this after starting the co-routine doesnt work.
      this.startSpawning();
      this.initEnemySpawning();
      this.initPowerupsSpawning();
    }

    private void createEnemyContainer()
    {
      this.enemyContainer = new GameObject();
      this.enemyContainer.name = "Enemies";
      this.enemyContainer.transform.parent = this.transform;
    }

    private void initEnemySpawning()
    {
      this.createEnemyContainer();
      this.enemySpawner = SpawnEnemiesRoutine();
      StartCoroutine(this.enemySpawner);
    }

    private void initPowerupsSpawning()
    {
      this.powerupContainer = new GameObject();
      this.powerupContainer.name = "Powerups";
      this.powerupContainer.transform.parent = this.transform;
      this.powerupSpawner = SpawnPowerupsRoutine();
      StartCoroutine(this.powerupSpawner);
    }

    IEnumerator SpawnEnemiesRoutine()
    {
      while (this.isSpawningActive)
      {
        if (this.difficultyLevel == 3) {
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
        } else {
          GameObject enemyInstance=this.spawnPrefabTop(this.enemyPrefab, this.enemyContainer);
          if (UnityEngine.Random.Range(1, 10) > 3) {
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

    private static Vector3 getRandomTopOfScreenPositionVector()
    {
      return new Vector3(UnityEngine.Random.Range(MIN_X_POS, MAX_X_POS), MAX_Y_POS);
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

    public void stopSpawning()
    {
      this.isSpawningActive = false;
    }
    public void startSpawning()
    {
      this.isSpawningActive = true;
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
  }
}