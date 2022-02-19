using UnityEngine;

namespace vio.spaceshooter.game.player.weapon
{
  public abstract class PlayerWeapon
  {
    protected GameObject weaponPrefab;
    protected Player player;

    protected AudioSource audioSource;
    protected AudioClip audioClip;
    public PlayerWeapon(Player player, GameObject weaponPrefab, AudioSource audioSource, AudioClip audioClip)
    {
      this.player = player;
      this.weaponPrefab = weaponPrefab;
      this.audioSource = audioSource;
      this.audioClip = audioClip;
    }

    abstract public void AttemptFire();
  }
}
