using UnityEngine;
using vio.spaceshooter.player;

namespace vio.spaceshooter.enemy
{
  public class Enemy : MonoBehaviour
  {
    private const float DEFAULT_ENEMY_SPEED = 4f;
    private const float MIN_Y_POS = -2.5f;
    private const float MAX_Y_POS = 12f;
    private const float MAX_X_POS = 10.00f;
    private const float MIN_X_POS = -9.75f;
    private int damageAmount = 1;
    
    void Update()
    {
      this.moveEnemyDown();
      if (this.isEnemyPastBottomOfPlayArea())
      {
        this.moveEnemyToRandomXLocationAtTop();
      }
    }
    private void moveEnemyDown()
    {
      this.transform.Translate(Vector3.down * DEFAULT_ENEMY_SPEED * Time.deltaTime);
    }

    private bool isEnemyPastBottomOfPlayArea()
    {
      return (this.transform.position.y < MIN_Y_POS);
    }

    private void moveEnemyToRandomXLocationAtTop()
    {
      this.transform.position = new Vector3(UnityEngine.Random.Range(MIN_X_POS, MAX_X_POS), MAX_Y_POS);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      switch (other.tag)
      {
        case "Laser":
          laserHit(other);
          break;

        case "Player":
          playerHit(other);
          break;
      }
    }
    private void laserHit(Collider2D other)
    {
      this.GetComponentInParent<Game>().IncreaseScore(10);
      Destroy(other.gameObject);
      Destroy(this.gameObject);
    }

    private void playerHit(Collider2D other)
    {
      Player player = other.GetComponent<Player>();
      if (player != null)
      {
        player.Damage(this.gameObject, this.damageAmount);
      }
      Destroy(this.gameObject);
    }
  }
}