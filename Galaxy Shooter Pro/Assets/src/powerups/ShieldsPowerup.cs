using UnityEngine;
using vio.spaceshooter.player;
using vio.spaceshooter.player.weapon;

namespace vio.spaceshooter.powerups
{
  public class ShieldsPowerup : Powerup
  {
    protected override void applyPowerupToPlayer(Player player)
    {
      player.EnableShields();
    }
  }
}