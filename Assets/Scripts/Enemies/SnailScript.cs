using System.Collections;
using UnityEngine;

public class SnailScript : MonoBehaviour
{
    private bool canMove;
    private bool moveLeft;
    private bool stunned;
    public float moveSpeed = 1f;
    private Animator anim;
    public LayerMask playerLayer;
    private Rigidbody2D mybody;
    public Transform leftCollision, rightCollision, topCollision, downCollision;
    private Vector3 leftCollisionPos, rightCollisionPos;

    private void Awake()
    {
        mybody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        leftCollisionPos = leftCollision.position;
        rightCollisionPos = rightCollision.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        moveLeft = true;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (moveLeft)
            {
                mybody.velocity = new Vector2(-moveSpeed, mybody.velocity.y);
            }
            else
            {
                mybody.velocity = new Vector2(moveSpeed, mybody.velocity.y);
            }
        }
        CheckCollision();
    }

    void CheckCollision()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftCollision.position, Vector2.left, 0.1f, playerLayer);

        if (leftHit && leftHit.collider.tag == MyTags.PLAYER_TAG)
        {
            if (!stunned)
            {
                //Apply damage to player
                leftHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
            }
            else
            {
                if (tag != MyTags.BEETLE_TAG)
                {
                    mybody.velocity = new Vector2(15f, mybody.velocity.y);
                    StartCoroutine(Dead(3f));
                }
            }
        }

        RaycastHit2D rightHit = Physics2D.Raycast(rightCollision.position, Vector2.right, 0.1f, playerLayer);

        if (rightHit && rightHit.collider.tag == MyTags.PLAYER_TAG)
        {
            if (!stunned)
            {
                //Apply damage to player
                rightHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
            }
            else
            {
                if (tag != MyTags.BEETLE_TAG)
                {
                    mybody.velocity = new Vector2(-15f, mybody.velocity.y);
                    StartCoroutine(Dead(3f));
                }
            }
        }

        Collider2D topHit = Physics2D.OverlapCircle(topCollision.position, 0.2f, playerLayer);

        if (topHit && topHit.tag == MyTags.PLAYER_TAG)
        {
            if (!stunned)
            {
                topHit.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 5f);
                canMove = false;

                anim.Play("Stunned");
                stunned = true;

                if (tag == MyTags.BEETLE_TAG)
                {
                    StartCoroutine(Dead(0.5f));
                }
            }
            else
            {
                topHit.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 5f);
                gameObject.SetActive(false);
            }
        }

        RaycastHit2D downHit = Physics2D.Raycast(downCollision.position, Vector2.down, 0.1f);

        if (downHit.collider == null)
        {            
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        moveLeft = !moveLeft;
        Vector3 tempScale = transform.localScale;

        if (moveLeft)
        {
            tempScale.x = Mathf.Abs(tempScale.x);
            leftCollision.position = leftCollisionPos;
            rightCollision.position = rightCollisionPos;
        }
        else
        {
            tempScale.x = -Mathf.Abs(tempScale.x);
            leftCollision.position = rightCollisionPos;
            rightCollision.position = leftCollisionPos;
        }
        transform.localScale = tempScale;
    }

    IEnumerator Dead(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == MyTags.BULLET_TAG)
        {
            if (tag == MyTags.BEETLE_TAG)
            {
                anim.Play("Stunned");
                StartCoroutine(Dead(0.4f));
                canMove = false;                
            }

            if (tag == MyTags.SNAIL_TAG)
            {
                if (stunned)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    anim.Play("Stunned");
                    canMove = false;
                    stunned = true;
                }
            }
        }
    }
}
