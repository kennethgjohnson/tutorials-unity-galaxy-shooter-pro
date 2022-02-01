﻿using UnityEngine;

namespace vio.spaceshooter.game.player.weapon
{
  public class TripleShotLaserCannon : LaserCannonFast
  {
    
    public TripleShotLaserCannon(Player player, GameObject weaponPrefab) : base(player, weaponPrefab)
    {
      this.LASER_STARTING_OFFSET = 0f;
    }
  }
}

