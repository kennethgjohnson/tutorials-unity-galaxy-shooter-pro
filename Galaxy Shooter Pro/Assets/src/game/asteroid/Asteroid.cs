using UnityEngine;
using vio.spaceshooter.game.player;
namespace vio.spaceshooter.game.asteroid
{
  public class Asteroid : MonoBehaviour
  {
    private Vector2 momentumVector;
    private const float MAX_Y_POS = 12f;
    private const float MIN_Y_POS = -2.5f;
    private const float MAX_X_POS = 15.00f;
    private const float MIN_X_POS = -14.75f;
    private float rotation = 25f;

    private int damageAmount = 2;

    private int asteroidLife = 7;

    // Start is called before the first frame update
    void Start()
    {
      this.beginMomentum();
    }

    private void beginMomentum()
    {
      if (this.transform.position.x < -9.75f)
      {
        this.randomizeMomentum(true);
      }
      else if (this.transform.position.x > 10f)
      {
        this.randomizeMomentum(false);
      }
      else
      {
        this.randomizeMomentum(UnityEngine.Random.Range(0, 1) == 1);
      }
    }

    private void randomizeMomentum(bool isRight)
    {
      if (isRight) {
        this.momentumVector = new Vector2(UnityEngine.Random.Range(1f, 4f), UnityEngine.Random.Range(-3f, -7f));
      } else
      {
        this.momentumVector = new Vector2(UnityEngine.Random.Range(-1f, -4f), UnityEngine.Random.Range(-3f, -7f));
      }
    }

    // Update is called once per frame
    void Update()
    {
      this.transform.Rotate(0, 0, rotation * Time.deltaTime);
      this.transform.Translate(this.momentumVector * Time.deltaTime, Space.World);
      /*
      Vector2 downward = Vector2 * speed * Time.deltaTime;
      this.transform.Translate(downward);*/
      if (this.isAsteroidPastBottomOfPlayArea()) moveAsteroidToRandomXLocationAtTop();
    }

    private bool isAsteroidPastBottomOfPlayArea()
    {
      return (this.transform.position.y < MIN_Y_POS);
    }

    private void moveAsteroidToRandomXLocationAtTop()
    {
      this.transform.position = new Vector3(UnityEngine.Random.Range(MIN_X_POS, MAX_X_POS), MAX_Y_POS);
      this.transform.Rotate(0, 0, UnityEngine.Random.Range(0f, 360f));
      this.beginMomentum();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      switch (other.tag)
      {
        case "Laser":
          laserHit(other);
          break;

        case "Asteroid":
          asteroidDestroyed();
          break;
          
        case "Player":
          playerHit(other);
          break;
      }
    }

    private void laserHit(Collider2D other)
    {
      Destroy(other.gameObject);
      this.Damage(1);
      if (this.asteroidLife < 1)
      {
        this.GetComponentInParent<Game>().IncreaseScore(100);
      }
    }

    private void playerHit(Collider2D other)
    {
      Player player = other.GetComponent<Player>();
      if (player != null)
      {
        player.Damage(this.gameObject, this.damageAmount);
      }
      this.GetComponent<PolygonCollider2D>().enabled = false;
      this.GetComponent<Animator>().SetTrigger("OnAsteroidDestroyed");
    }

    private void asteroidDestroyed()
    {
      this.GetComponent<PolygonCollider2D>().enabled = false;
       this.GetComponent<Animator>().SetTrigger("OnAsteroidDestroyed");
    }

    public void OnAsteroidDestroyedComplete()
    {
      Destroy(this.gameObject);
    }

    public void Damage(int damage)
    {
      this.asteroidLife -= damage;
      if (this.asteroidLife<1)
      {
        this.GetComponent<PolygonCollider2D>().enabled = false;
        this.GetComponent<Animator>().SetTrigger("OnAsteroidDestroyed");
      }
    }
  }
}
