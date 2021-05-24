using System;
using System.Collections;
using UnityEngine;

namespace vio.spaceshooter.spawnmanager
{
  public class SpawnManager : MonoBehaviour
  {
    [SerializeField]
    private GameObject enemyPrefab;
    
    [SerializeField]
    private GameObject tripleShotPowerupPrefab;

    [SerializeField]
    private GameObject speedPowerupPrefab;

    [SerializeField]
    private GameObject shieldPowerupPrefab;

    private const float MAX_Y_POS = 12f;
    private const float MAX_X_POS = 10.00f;
    private const float MIN_X_POS = -9.75f;

    private const float MAX_ENEMY_SPAWN_SPEED = 2f;
    private const float MIN_ENEMY_SPAWN_SPEED = 0.5f;

    private const float MAX_POWERUP_SPAWN_SPEED = 10f;
    private const float MIN_POWERUP_SPAWN_SPEED = 7f;


    GameObject enemyContainer;
    GameObject powerupContainer;

    private bool isSpawningActive = false;

    void Start()
    {
      //Setting this after starting the co-routine doesnt work.
      this.startSpawning(); 

      this.initEnemySpawning();
      this.initPowerupsSpawning();
    }

    private void initEnemySpawning()
    {
      this.enemyContainer = new GameObject();
      this.enemyContainer.name = "Enemies";
      this.enemyContainer.transform.parent = this.transform;
      StartCoroutine(SpawnEnemiesRoutine());
    }

    private void initPowerupsSpawning()
    {
      this.powerupContainer = new GameObject();
      this.powerupContainer.name = "Powerups";
      this.powerupContainer.transform.parent = this.transform;
      StartCoroutine(SpawnPowerupsRoutine());
    }

    IEnumerator SpawnEnemiesRoutine()
    {
      while (this.isSpawningActive)
      {
        this.spawnPrefabTop(this.enemyPrefab, this.enemyContainer);
        yield return new WaitForSeconds(UnityEngine.Random.Range(MIN_ENEMY_SPAWN_SPEED, MAX_ENEMY_SPAWN_SPEED));
      }
    }

    private void spawnPrefabTop(GameObject prefab, GameObject container)
    {
      GameObject spawnedInstance = MonoBehaviour.Instantiate(
                  prefab,
                  getRandomTopOfScreenPositionVector(),
                  Quaternion.identity
                );
      spawnedInstance.transform.parent = container.transform;
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
        GameObject powerupPrefab;
        switch (powerupNumber)
        {
          case 0:
            powerupPrefab = tripleShotPowerupPrefab;
              break;

          case 1:
            powerupPrefab = speedPowerupPrefab;
              break;
          case 2:
            powerupPrefab = shieldPowerupPrefab;
              break;
          default:
            powerupPrefab = null;
            break;
        }
        if (powerupPrefab != null) {
          this.spawnPrefabTop(powerupPrefab, this.powerupContainer);
        }
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
  }

}