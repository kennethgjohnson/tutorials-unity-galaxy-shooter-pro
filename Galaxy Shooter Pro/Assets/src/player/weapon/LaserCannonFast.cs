using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vio.spaceshooter.player.weapon
{
  public class LaserCannonFast : LaserCannon
  {
    public LaserCannonFast(Player player, GameObject weaponPrefab) : base(player,weaponPrefab)
    {
      this.rateOfFire = 300f;
    }
  }
}
