using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vio.spaceshooter.player
{
  class PlayerActionsHandler
  {
    private const float LASER_STARTING_OFFSET = 1.05f;
    private float rateOfFire = 300f;
    private float nextFireTime = 0f;
    private readonly Player player;
    private GameObject laser;
    public PlayerActionsHandler(Player player,GameObject laser)
    {
      this.player = player;
      this.laser = laser;
    }

    // Update is called once per frame
    public void Update()
    {

      if (isPlayerFiring() && this.isReadToFire())
      {
        makeLaserGameObject();
        setNextFireTimeCooldown();
      }
    }

    private static bool isPlayerFiring()
    {
      return Input.GetKey(KeyCode.Space);
    }
    private bool isReadToFire()
    {
      return Time.time >= this.nextFireTime;
    }

    private void makeLaserGameObject()
    {
      MonoBehaviour.Instantiate(
                this.laser,
                this.player.transform.position + Vector3.up * LASER_STARTING_OFFSET,
                Quaternion.identity
              );
    }

    private void setNextFireTimeCooldown()
    {
      this.nextFireTime = Time.time + this.calculateTimeBetweenBullets();
    }

    private float calculateTimeBetweenBullets()
    {
      return (60 / rateOfFire);
    }

  
  }
}
