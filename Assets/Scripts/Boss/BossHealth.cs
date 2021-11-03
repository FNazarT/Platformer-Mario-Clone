using System.Collections;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    private Animator anim;
    private int health = 10;
    private bool canDamage;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        canDamage = true;
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canDamage && collision.tag == MyTags.BULLET_TAG)
        {
            health--;
            canDamage = false;

            if(health == 0)
            {
                GetComponent<BossScript>().DeactivateBossScript();
                anim.Play("BossDead");
            }

            StartCoroutine(WaitForDamage());      
        }
    }
}
