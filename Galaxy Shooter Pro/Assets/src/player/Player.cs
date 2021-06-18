using System;
using System.Collections;
using UnityEngine;
using vio.spaceshooter.player.weapon;
using vio.spaceshooter.spawnmanager;

namespace vio.spaceshooter.player
{
  public class Player : MonoBehaviour
  {
    private const float DEFAULT_PLAYER_SPEED = 7f;

    private const float MAX_Y_POS = 8f;
    private const float MIN_Y_POS = -0.8f;

    private const float MAX_X_POS = 10.00f;
    private const float MIN_X_POS = -9.75f;

    private float newX;
    private float newY;

    private FakePlayerBehaviourHandler fakePlayerBehaviourHandler;

    private PlayerMovementHandler playerMovementHandler;
    private PlayerActionsHandler playerActionHandler;

    SpawnManager spawnManager;

    [SerializeField]
    private GameObject laser;

    [SerializeField]
    private GameObject playerShieldsEffect;

    private PlayerWeapon weapon;
    private bool isShieldsActive = false;

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
      this.playerActionHandler = new PlayerActionsHandler(this);
      this.weapon = new LaserCannon(this, this.laser);
    }

    void Update()
    {
      this.playerMovementHandler.Update();
      this.playerActionHandler.Update();
    }

    public void Damage(GameObject source, int damageAmount)
    {
      if (this.isShieldsActive)
      {
        this.deactivateShields();
      } else
      {
        this.takeDamage(damageAmount);
      }
    }

    private void takeDamage(int damageAmount)
    {
      this.GetComponentInParent<Game>().LoseLife();
    }

    public void KillPlayer()
    {
      this.playerMovementHandler.Reset();
      this.OnPlayerDeath();
      this.gameObject.SetActive(false);
    }

    private void OnPlayerDeath()
    {
      spawnManager.stopSpawning();
    }

    public PlayerWeapon GetWeapon()
    {
      return this.weapon;
    }
    public void SetWeapon(PlayerWeapon weapon)
    {
      this.weapon = weapon;
    }

    public void BoostSpeed(float newSpeed)
    {
      this.playerMovementHandler.SetPlayerSpeed(newSpeed);
    }

    public void ResetSpeed()
    {
      this.playerMovementHandler.ResetPlayerSpeed();
    }

    public void EnableShields()
    {
      if (this.isShieldsActive) return;
      this.isShieldsActive = true;
      this.playerShieldsEffect.SetActive(true);
    }

    private void deactivateShields()
    {
      this.isShieldsActive = false;
      this.playerShieldsEffect.SetActive(false);
    }
  }

}