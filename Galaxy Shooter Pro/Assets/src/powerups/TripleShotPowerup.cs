using System.Collections;
using UnityEngine;
using vio.spaceshooter.player;
using vio.spaceshooter.player.weapon;

namespace vio.spaceshooter.powerups {
  public class TripleShotPowerup : Powerup
  {
    [SerializeField]
    private GameObject weaponPrefab;
    protected float CANNON_DURABILITY_DURATION_SECONDS = 5f;

    private Player player = null;
    private PlayerWeapon playerOriginalWeapon = null;

    protected override void applyPowerupToPlayer(Player player)
    {
      this.player = player;
      this.playerOriginalWeapon = player.GetWeapon();
      player.SetWeapon(new TripleShotLaserCannon(player, this.weaponPrefab));
      player.StartCoroutine(PowerdownRoutine());
    }

    IEnumerator PowerdownRoutine()
    {
      yield return new WaitForSeconds(CANNON_DURABILITY_DURATION_SECONDS);
      this.player.SetWeapon(this.playerOriginalWeapon);
      this.player = null;
      this.playerOriginalWeapon = null;
    }
  }
}