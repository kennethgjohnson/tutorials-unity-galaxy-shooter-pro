﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vio.spaceshooter.player.weapon
{
  public class TripleShotLaserCannon : LaserCannon
  {
    public TripleShotLaserCannon(Player player, GameObject weaponPrefab) : base(player, weaponPrefab)
    {
      this.LASER_STARTING_OFFSET = 0f;
    }
  }
}
