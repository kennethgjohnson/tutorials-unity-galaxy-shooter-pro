using System.Collections;
using UnityEngine;

namespace vio.spaceshooter.player.weapon
{
  public class TripleShotLaserCannon : LaserCannon
  {
    protected float CANNON_DURABILITY_DURATION_SECONDS = 5f;
    IEnumerator powerdownRoutine;
    public TripleShotLaserCannon(Player player, GameObject weaponPrefab) : base(player, weaponPrefab)
    {
      this.LASER_STARTING_OFFSET = 0f;
      this.powerdownRoutine = PowerdownRoutine();
      player.StartCoroutine(this.powerdownRoutine);
    }

    IEnumerator PowerdownRoutine()
    {
      yield return new WaitForSeconds(CANNON_DURABILITY_DURATION_SECONDS);
      this.player.SetWeapon(this.previousWeapon);
    }
  }
}

