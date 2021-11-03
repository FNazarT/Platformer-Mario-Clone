using UnityEngine;

public class BonusBlock : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer = default;
    [SerializeField] private Transform bottomCollision = null;
    private Animator anim;
    private bool canAminate = true;
    private bool startAnim;
    private Vector3 moveDirection = Vector3.up;
    private Vector3 animPosition;
    private Vector3 originalPosition;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        animPosition = transform.position + new Vector3(0f, 0.15f, 0f);
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForCollision();
        AnimateUpDown();
    }

    void CheckForCollision()
    {
        if (canAminate)
        {
            RaycastHit2D hit = Physics2D.Raycast(bottomCollision.position, Vector3.down, 0.1f, playerLayer);

            if (hit)
            {
                hit.collider.gameObject.GetComponent<ScoreScript>().BlockScore();
                anim.Play("BlockIdle");
                startAnim = true;
                canAminate = false;
            }
        }
    }

    void AnimateUpDown()
    {
        if (startAnim)
        {
            transform.Translate(moveDirection * Time.smoothDeltaTime);

            if(transform.position.y >= animPosition.y)
            {
                moveDirection = Vector3.down;
            }
            else if (transform.position.y <= originalPosition.y)
            {
                startAnim = false;
            }
        }
    }
}
