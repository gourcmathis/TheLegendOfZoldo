using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    private float dazedTime;
    public float startDazedTime;
    public Transform[] waypoints;

    public Animator damageAnim;
    public Animator deathAnim;
    public SpriteRenderer graphics;
    public Rigidbody2D playerRigidbody;
    private Transform target;
    private int destPoint = 0;
    public float hurtForce = 70f;

    // Start is called before the first frame update
    void Start()
    {
        target = waypoints[0];
        playerRigidbody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            destPoint = (destPoint+1) % waypoints.Length;
            target = waypoints[destPoint];
            graphics.flipX = !graphics.flipX;
        }


        if(dazedTime <= 0)
        {
            speed = 2;
        } else
        {
            speed = 0;
            dazedTime -= Time.deltaTime;
        }

        if(health <= 0)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            speed = 0;
            Destroy(gameObject, 1f);
            deathAnim.Play("SlimeDeath");
        }
    }

    public void TakeDamage(int damage)
    {
        dazedTime = startDazedTime;
        damageAnim.SetTrigger("Damage");
        health -= damage;
        Debug.Log("damage TAKEN !");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHurt playerHurt = collision.transform.GetComponent<PlayerHurt>();
            playerHurt.getHurt();
            HealthBar healthBar = collision.transform.GetComponent<HealthBar>();
            healthBar.TakeDamageP();
            
            if(healthBar.health>0)
            {
                if (collision.gameObject.transform.position.x < transform.position.x){
                    playerRigidbody.velocity = new Vector2(-hurtForce*10, playerRigidbody.velocity.y);
                }else {
                    playerRigidbody.velocity = new Vector2(hurtForce*10, playerRigidbody.velocity.y);

                }
            }
            
        }
    }
}
