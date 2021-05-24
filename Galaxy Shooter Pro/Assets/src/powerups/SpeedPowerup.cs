using System.Collections;
using UnityEngine;
using vio.spaceshooter.player;
using vio.spaceshooter.player.weapon;

namespace vio.spaceshooter.powerups
{
  public class SpeedPowerup : Powerup
  {
    private const float BOOST_SPEED = 22f;
    private const float BOOST_DURATION_SECONDS = 5f;
    private Player player = null;
    protected override void applyPowerupToPlayer(Player player)
    {
      player.BoostSpeed(BOOST_SPEED);
      this.StartCoroutine(PowerdownSpeedBoost());
    }
    
    IEnumerator PowerdownSpeedBoost()
    {
      yield return new WaitForSeconds(BOOST_DURATION_SECONDS);
      this.player.ResetSpeed();
      this.player = null;
    }
  }
}