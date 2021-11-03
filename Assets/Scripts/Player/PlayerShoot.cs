using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    [SerializeField] private GameObject fireBullet = null;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            GameObject Bullet = Instantiate(fireBullet, transform.position, Quaternion.identity);
            Bullet.GetComponent<FireBullet>().Speed *= transform.localScale.x;
        }
    }
}
