using UnityEngine;

namespace vio.spaceshooter.game.player.weapon
{
  public class LaserCannonFast : LaserCannon
  {
    public LaserCannonFast(Player player, GameObject weaponPrefab) : base(player,weaponPrefab)
    {
      this.rateOfFire = 300f;
    }
  }
}
