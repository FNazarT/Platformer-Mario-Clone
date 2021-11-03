using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    [SerializeField] private GameObject stonePrefab = null;
    [SerializeField] private Transform attackInstantiate = null;

    private Animator anim;
    private string coroutine_Name = "StartAttack";

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine(coroutine_Name);
    }

    void Attack()        //Animation Event Function
    {
        GameObject stone = Instantiate(stonePrefab, attackInstantiate.position, Quaternion.identity);
        stone.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-300f, -700f), 0f));
    }

    void BackToIdle()     //Animation Event Function
    {
        anim.Play("BossIdle");
    }

    public void DeactivateBossScript()
    {
        StopCoroutine(coroutine_Name);
        enabled = false;
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(Random.Range(2.0f, 5.0f));
        anim.Play("BossAttack");
        StartCoroutine(coroutine_Name);
    }
}
