using UnityEngine;

namespace vio.spaceshooter.game.player.weapon
{
  public class TripleShotLaserCannon : LaserCannonFast
  {
    
    public TripleShotLaserCannon(Player player, GameObject weaponPrefab, AudioSource audioSource, AudioClip audioClip) : base(player, weaponPrefab, audioSource, audioClip)
    {
      this.LASER_STARTING_OFFSET = 0f;
    }
  }
}

