using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using vio.spaceshooter.player;
using vio.spaceshooter.player.weapon;

namespace vio.spaceshooter.powerups {
  public class TripleShotPowerup : MonoBehaviour
  {
    [SerializeField]
    private GameObject weaponPrefab;

    private const float DEFAULT_SPEED = 5f;
    private const float MIN_Y_POS = -2.5f;

    void Update()
    {
      this.applyMovement();
      if (this.isOutOfBoundry())
      {
        this.removeObject();
      }
    }

    private void applyMovement()
    {
      this.transform.Translate(
                  Vector3.down
                  * this.getPosibleDistanceMovedSinceLastFrame()
              );
    }

    private float getPosibleDistanceMovedSinceLastFrame()
    {
      return DEFAULT_SPEED * Time.deltaTime;
    }

    private bool isOutOfBoundry()
    {
      return (this.transform.position.y < MIN_Y_POS);
    }

    private void removeObject()
    {
      Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
      switch (other.tag)
      {
        case "Player":
          this.givePlayerTripleShot(other);
          break;
      }

    }

    private void givePlayerTripleShot(Collider2D other)
    {
      Player player = other.GetComponent<Player>();
      if (player != null)
      {
        player.SetWeapon(new TripleShotLaserCannon(player, this.weaponPrefab));
      }
      Destroy(this.gameObject);
    }
  }
}