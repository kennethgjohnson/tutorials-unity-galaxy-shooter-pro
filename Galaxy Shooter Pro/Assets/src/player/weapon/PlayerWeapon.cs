using UnityEngine;

namespace vio.spaceshooter.player.weapon
{
  public abstract class PlayerWeapon
  {
    protected GameObject weaponPrefab;
    protected Player player;
    protected PlayerWeapon previousWeapon;
    public PlayerWeapon(Player player, GameObject weaponPrefab)
    {
      this.player = player;
      this.previousWeapon = player.GetWeapon();
      this.weaponPrefab = weaponPrefab;      
    }

    abstract public void AttemptFire();
  }
}
