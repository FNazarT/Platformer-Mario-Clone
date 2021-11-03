using System.Collections;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    private Animator anim;
    private bool attacked;
    private bool canMove;
    private float speed = 2.5f;
    [SerializeField] private GameObject birdEgg = null;
    public LayerMask playerLayer;
    private Rigidbody2D myBody;
    private Vector3 finalPositionNeg;
    private Vector3 finalPositionPos;
    private Vector3 moveDirection = Vector3.left;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        finalPositionPos = transform.position;
        finalPositionPos.x += 6f;

        finalPositionNeg = transform.position;
        finalPositionNeg.x -= 6f;

        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        MoveTheBird();
        DropTheEgg();
    }

    void MoveTheBird()
    {
        if (canMove)
        {
            transform.Translate(moveDirection * speed * Time.smoothDeltaTime);

            if (transform.position.x >= finalPositionPos.x)
            {
                moveDirection = Vector3.left;
                ChangeDirection(0.5f);
            }
            else if (transform.position.x <= finalPositionNeg.x)
            {
                moveDirection = Vector3.right;
                ChangeDirection(-0.5f);
            }
        }
    }

    void ChangeDirection (float direction)
    {
        Vector3 TempScale = transform.localScale;
        TempScale.x = direction;
        transform.localScale = TempScale;
    }

    void DropTheEgg()
    {
        if (!attacked)
        {
            if (Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, playerLayer))
            {
                Instantiate(birdEgg, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Quaternion.identity);
                attacked = true;
                anim.Play("BirdFly");
            }
        }
    }

    IEnumerator BirdDead()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == MyTags.BULLET_TAG)
        {
            anim.Play("BirdDead");
            canMove = false;
            GetComponent<BoxCollider2D>().isTrigger = true;
            myBody.bodyType = RigidbodyType2D.Dynamic;

            StartCoroutine(BirdDead());
        }
    }
}
