﻿using UnityEngine;
using vio.spaceshooter.game.player.weapon;

namespace vio.spaceshooter.game.player
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

    
    [SerializeField]
    private GameObject laser;

    [SerializeField]
    private GameObject playerShieldsEffect;

    private PlayerWeapon weapon;
    private bool isShieldsActive = false;
    private bool isExploding = false;

    void Start()
    {
      this.playerMovementHandler
        = new PlayerMovementHandler(
          this,
          new FakePlayerBehaviourHandler(
            GameObject.Find("Player_Fake").transform
          )
        );
      this.playerActionHandler = new PlayerActionsHandler(this);
      this.weapon = new LaserCannon(this, this.laser);
      this.transform.position = new Vector3(0, 0, 0);
    }

    public void ResetPlayer()
    {
      this.weapon = new LaserCannon(this, this.laser);
      this.transform.position = new Vector3(0, 0, 0);
      this.ResetSpeed();
      this.deactivateShields();
      this.gameObject.SetActive(true);
    }

    void Update()
    {
      if (!this.isExploding)
      {
        this.playerMovementHandler.Update();
        this.playerActionHandler.Update();
      }
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
      for(int i=1; i<=damageAmount; i++) { 
        this.GetComponentInParent<Game>().LoseLife();
      }
    }

    public void KillPlayer()
    {
      this.playerMovementHandler.Reset();
      this.OnPlayerDeath();
    }

    private void OnPlayerDeath()
    {
      //this.speed = this.speed * UnityEngine.Random.Range(0f, 0.25f);
      //We begin exloding.
      this.GetComponent<PolygonCollider2D>().enabled = false;
      this.GetComponent<Animator>().SetTrigger("OnPlayerDeath");
      this.isExploding = true;
    }

    private void OnPlayerDeathComplete()
    {
      //We are done exloding.
      this.GetComponent<PolygonCollider2D>().enabled = true;
      this.isExploding = false;
      this.gameObject.SetActive(false);
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