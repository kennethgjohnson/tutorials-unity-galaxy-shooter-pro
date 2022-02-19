using UnityEngine;

namespace vio.spaceshooter.game.player.weapon
{
  public class LaserCannonFast : LaserCannon
  {
    public LaserCannonFast(Player player, GameObject weaponPrefab, AudioSource audioSource, AudioClip audioClip) : base(player,weaponPrefab, audioSource, audioClip)
    {
      this.rateOfFire = 300f;
    }
  }
}
