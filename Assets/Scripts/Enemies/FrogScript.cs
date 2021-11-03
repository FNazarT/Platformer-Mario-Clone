using System.Collections;
using UnityEngine;

public class FrogScript : MonoBehaviour
{
	private Animator anim;
	private bool animation_Finished;
    private bool jumpLeft = true;
    private GameObject player;
    private int jumpedTimes;
    public LayerMask playerLayer;
    private string coroutine_Name = "FrogJump";

	void Awake()
    {
		anim = GetComponent<Animator>();
	}

	void Start()
    {
		StartCoroutine (coroutine_Name);
		player = GameObject.FindGameObjectWithTag(MyTags.PLAYER_TAG);
	}

	void Update()
    {
		if(Physics2D.OverlapCircle(transform.position, 0.5f, playerLayer))
        {
            player.GetComponent<PlayerDamage>().DealDamage();
		}
	}

	void LateUpdate()
    {
		if (animation_Finished)
        {
			transform.parent.position = transform.position;
			transform.localPosition = Vector3.zero;
		}
	}

	IEnumerator FrogJump()
    {
		yield return new WaitForSeconds (Random.Range(1f, 4f));

		animation_Finished = false;
  
		jumpedTimes++;

		if (jumpLeft) {
			anim.Play ("FrogJumpLeft");
		} else {
			anim.Play ("FrogJumpRight");
		}

		StartCoroutine (coroutine_Name);

	}

    //This function is triggered with an Animation Event at the end of both FrogJumpLeft and FrogJumpRight
	void AnimationFinished()
    {
		animation_Finished = true;

		if (jumpLeft) {
			anim.Play ("FrogIdleLeft");
		} else {
			anim.Play ("FrogIdleRight");
		}

		if (jumpedTimes == 3) {

			jumpedTimes = 0;
			jumpLeft = !jumpLeft;
		}
	}
}