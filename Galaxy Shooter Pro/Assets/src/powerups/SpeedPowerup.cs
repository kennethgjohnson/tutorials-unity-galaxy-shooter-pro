using System.Collections;
using UnityEngine;
using vio.spaceshooter.player;
using vio.spaceshooter.player.weapon;

namespace vio.spaceshooter.powerups
{
  public class SpeedPowerup : Powerup
  {
    [SerializeField]
    private GameObject weaponPrefab;

    private const float BOOST_SPEED = 22f;
    private const float BOOST_DURATION_SECONDS = 5f;
    private Player player = null;
    private PlayerWeapon playerOriginalWeapon = null;

    protected override void applyPowerupToPlayer(Player player)
    {
      this.player = player;
      this.playerOriginalWeapon = player.GetWeapon();
      player.BoostSpeed(BOOST_SPEED);
      player.SetWeapon(new LaserCannonFast(player, this.weaponPrefab));
      player.StartCoroutine(PowerdownSpeedBoost());
    }
    
    IEnumerator PowerdownSpeedBoost()
    {
      yield return new WaitForSeconds(BOOST_DURATION_SECONDS);
      this.player.ResetSpeed();
      this.player.SetWeapon(this.playerOriginalWeapon);
      this.player = null;
      this.playerOriginalWeapon = null;
    }
  }
}