using UnityEngine;

namespace vio.spaceshooter.game.player.weapon
{
  public class LaserCannon : PlayerWeapon
  {
    protected float LASER_STARTING_OFFSET = 1.05f;
    protected float rateOfFire = 150f;
    private float nextFireTime = 0f;

    public LaserCannon(Player player, GameObject weaponPrefab) : base(player,weaponPrefab)
    {
    }

    public override void AttemptFire()
    {
      if (this.isReadToFire()) {
        this.fireLaser();
        this.setNextFireTimeCooldown();
      }
    }

    private bool isReadToFire()
    {
      return Time.time >= this.nextFireTime;
    }


    private void fireLaser()
    {
      MonoBehaviour.Instantiate(
                this.weaponPrefab,
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
