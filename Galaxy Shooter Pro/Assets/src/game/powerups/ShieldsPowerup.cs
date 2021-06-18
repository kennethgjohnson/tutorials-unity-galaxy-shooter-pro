using UnityEngine;
using vio.spaceshooter.game.player;
using vio.spaceshooter.game.player.weapon;

namespace vio.spaceshooter.game.powerups
{
  public class ShieldsPowerup : Powerup
  {
    protected override void applyPowerupToPlayer(Player player)
    {
      player.EnableShields();
    }
  }
}