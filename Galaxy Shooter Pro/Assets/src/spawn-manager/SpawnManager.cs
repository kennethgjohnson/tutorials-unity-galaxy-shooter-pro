using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
  [SerializeField]
  private GameObject enemy;

  private const float MAX_Y_POS = 12f;
  private const float MAX_X_POS = 10.00f;
  private const float MIN_X_POS = -9.75f;

  private const float ENEMY_SPAWN_SPEED = 1f;
  IEnumerator spawnRoutine;
  GameObject enemyContainer;

  private bool isSpawningActive = false;

  void Start()
    {
    this.enemyContainer = new GameObject();
    this.enemyContainer.name = "Enemies";
    this.enemyContainer.transform.parent = this.transform;
    
    this.spawnRoutine = SpawnRoutine();
    this.startSpawning(); //Setting this after starting the co-routine doesnt work.
    StartCoroutine(this.spawnRoutine);
  }

  IEnumerator SpawnRoutine()
  {
    while (this.isSpawningActive)
    {
      GameObject enemy=MonoBehaviour.Instantiate(
                this.enemy, 
                new Vector3(UnityEngine.Random.Range(MIN_X_POS, MAX_X_POS), MAX_Y_POS),
                Quaternion.identity
              );
      enemy.transform.parent = this.enemyContainer.transform;
      yield return new WaitForSeconds(ENEMY_SPAWN_SPEED);
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
