using System.Collections;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    private bool canMove;
    private float speed = 10f;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        StartCoroutine(DisableBullet(5f));
    }

    // Update is called once per frame
    void Update()
    {
        Move();                
    }

    void Move()
    {
        if (canMove)
        {
            transform.Translate(new Vector3 (speed * Time.deltaTime, 0f, 0f));
            //Vector3 temp = transform.position;
            //temp.x += speed * Time.deltaTime;
            //transform.position = temp;
        }
    }

    public float Speed  // Getter and Setter Function
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }

    IEnumerator DisableBullet(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == MyTags.BEETLE_TAG || collision.tag == MyTags.SNAIL_TAG || collision.tag == MyTags.BIRD_TAG || 
            collision.tag == MyTags.SPIDER_TAG || collision.tag == MyTags.BOSS_TAG)
        {
            canMove = false;
            anim.Play("Explode");
            StartCoroutine(DisableBullet(0.2f));

        }
    }
}
