using UnityEngine;
using vio.spaceshooter.game.player;

namespace vio.spaceshooter.game.enemy
{
  public class Enemy : MonoBehaviour
  {
    private const float DEFAULT_ENEMY_SPEED = 4f;
    private const float MIN_Y_POS = -2.5f;
    private const float MAX_Y_POS = 12f;
    private const float MAX_X_POS = 10.00f;
    private const float MIN_X_POS = -9.75f;
    private int damageAmount = 1;
    private float speed = DEFAULT_ENEMY_SPEED;
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
      this.transform.Translate(Vector3.down * speed * Time.deltaTime);
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
      this.speed = this.speed * UnityEngine.Random.Range(0f, 0.5f);
      this.GetComponent<PolygonCollider2D>().enabled = false;
      this.GetComponent<Animator>().SetTrigger("OnEnemyDeath");
      Destroy(this.gameObject, 1.25f);
    }

    private void playerHit(Collider2D other)
    {
      Player player = other.GetComponent<Player>();
      if (player != null)
      {
        player.Damage(this.gameObject, this.damageAmount);
      }

      this.speed = this.speed * UnityEngine.Random.Range(0f, 0.25f);
      this.GetComponent<PolygonCollider2D>().enabled = false;
      this.GetComponent<Animator>().SetTrigger("OnEnemyDeath");

      Destroy(this.gameObject, 1.25f);
    }
  }
}