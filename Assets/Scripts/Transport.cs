using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Transport : MonoBehaviour
{
    [SerializeField] private Sprite spriteTransport;
    public float startSpeed;
    public float currentSpeed;
    protected  Rigidbody2D rigidbody2d;
    protected BoxCollider2D boxCollider2D;
    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        GetComponent<SpriteRenderer>().sprite = spriteTransport;
        currentSpeed = startSpeed;
    }

    protected virtual void Move()
    {
       var position = Vector3.down * ( currentSpeed * Time.fixedDeltaTime);
       rigidbody2d.MovePosition(rigidbody2d.position + (Vector2)position);
    }
   
    protected virtual  void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
            GameObject.FindWithTag("TextScore").GetComponent<Score>().score++;
            GameObject.FindWithTag("TextScore").GetComponent<Score>().UpdateScore();

        }
        else if (other.CompareTag("WallForEvent"))
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

}
