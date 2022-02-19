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

    private Game game;
    private PolygonCollider2D objectCollider;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
      this.beginMomentum();
      this.game = this.GetComponentInParent<Game>();
      this.objectCollider = this.GetComponent<PolygonCollider2D>();
      this.animator = this.GetComponent<Animator>();
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
        this.game.IncreaseScore(100);
      }
    }

    private void playerHit(Collider2D other)
    {
      Player player = other.GetComponent<Player>();
      if (player != null)
      {
        player.Damage(this.gameObject, this.damageAmount);
      }
      this.objectCollider.enabled = false;
      this.animator.SetTrigger("OnAsteroidDestroyed");
    }

    private void asteroidDestroyed()
    {
      this.objectCollider.enabled = false;
      this.animator.SetTrigger("OnAsteroidDestroyed");
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
        this.objectCollider.enabled = false;
        this.animator.SetTrigger("OnAsteroidDestroyed");
      }
    }
  }
}
