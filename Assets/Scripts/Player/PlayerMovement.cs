using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool onGround;
    private bool jumpInput;
    private bool moveInput;
    private float h = 0;
    [SerializeField] private float jumpPower = 6f;
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D mybody;
    private Animator anim;

    private void Awake()
    {
        mybody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Activates player's walk animation when arrows are pressed
        moveInput = h != 0 ? true : false;
        anim.SetBool("Move", moveInput);

        //Activates player's jump animation when "Space" Key is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpInput = true;
        }
        anim.SetBool("Jump", jumpInput);
    }

    private void FixedUpdate()
    {
        //if (!jumpInput)
        //{
            //Player movement: Take input of arrow buttons and change player's velocity on X Axis to make him move
            h = Input.GetAxisRaw("Horizontal");
            mybody.velocity = new Vector2(h * moveSpeed, mybody.velocity.y);
        //}

        //Flip the direction of the player according to which arrow button was pressed
        Vector3 temp = transform.localScale;
        if (h > 0)
        {          
            temp.x = Mathf.Abs(temp.x);
        }
        else if (h < 0)
        {
            temp.x = -Mathf.Abs(temp.x);
        }
        transform.localScale = temp;

        //Player jump: Change player's velocity on axis Y only when "jump" key is pressed and player is on the ground
        if (jumpInput && onGround)
        {
            mybody.velocity = new Vector2(mybody.velocity.x, jumpPower);
            onGround = false;
        }        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = true;
            jumpInput = false;
        }
    }
} // end of class
