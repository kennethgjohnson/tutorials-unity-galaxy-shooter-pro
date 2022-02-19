using System.Collections;
using UnityEngine;
using vio.spaceshooter.game.player;
using vio.spaceshooter.game.player.weapon;

namespace vio.spaceshooter.game.powerups
{
  public class SpeedPowerup : Powerup
  {
    [SerializeField]
    private GameObject weaponPrefab;

    private const float BOOST_SPEED = 22f;
    private const float BOOST_DURATION_SECONDS = 5f;
    private Player player = null;
    private PlayerWeapon playerOriginalWeapon = null;

    [SerializeField]
    private AudioClip audioClip;
    protected override void applyPowerupToPlayer(Player player)
    {
      this.player = player;
      this.playerOriginalWeapon = player.GetWeapon();
      player.BoostSpeed(BOOST_SPEED);
      player.SetWeapon(new LaserCannonFast(player, this.weaponPrefab, player.getAudioSource(), player.getLaserAudioClip()));
      //player.StartCoroutine(PowerdownSpeedBoost());
      player.playAudioClip(this.audioClip);
    }
    
    IEnumerator PowerdownSpeedBoost()
    {
      yield return new WaitForSeconds(BOOST_DURATION_SECONDS);
      this.player.ResetSpeed();
      this.player.SetWeapon(this.playerOriginalWeapon);
      this.player = null;
      this.playerOriginalWeapon = null;
    }
  }
}