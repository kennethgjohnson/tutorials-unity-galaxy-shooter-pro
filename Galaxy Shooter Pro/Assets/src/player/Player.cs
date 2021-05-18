using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; // MonoBehaviour, Vector3, Time, Input

namespace vio.spaceshooter.player
{
  public class Player : MonoBehaviour
  {
    private const float DEFAULT_PLAYER_SPEED = 15f;

    private const float MAX_Y_POS = 8f;
    private const float MIN_Y_POS = -0.8f;
    private const float MAX_X_POS = 10.00f;
    private const float MIN_X_POS = -9.75f;

    private float newX;
    private float newY;

    private FakePlayerBehaviourHandler fakePlayerBehaviourHandler;

    private PlayerMovementHandler playerMovementHandler;
    private PlayerActionsHandler playerActionHandler;

    private int health = 2;

    SpawnManager spawnManager;

    [SerializeField]
    private GameObject laser;
    // Start is called before the first frame update
    void Start()
    {
      this.transform.position = new Vector3(0, 0, 0);

      this.spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

      this.playerMovementHandler
        = new PlayerMovementHandler(
          this,
          new FakePlayerBehaviourHandler(
            GameObject.Find("Player_Fake").transform
          )
        );
      this.playerActionHandler = new PlayerActionsHandler(
        this,
        this.laser
      );      
    }

    // Update is called once per frame
    void Update()
    {
      this.playerMovementHandler.Update();
      this.playerActionHandler.Update();
    }

    public void Damage(GameObject source, int damageAmount)
    {
      this.health -= damageAmount;
      if (this.health<=0) {
        this.killPlayer();
      }
    }

    private void killPlayer()
    {
      this.playerMovementHandler.Reset();
      this.OnPlayerDeath();
      Destroy(this.gameObject);
    }

    private void OnPlayerDeath()
    {
      
      spawnManager.stopSpawning();
    }
  }

}