using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleAttack : MonoBehaviour
{
    public Vector2 BulletSpeed;
    public AudioClip TriangleKill;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = BulletSpeed;
        if (BulletSpeed.x < 0)
        {
            Vector3 currentScale = gameObject.transform.localScale;
            currentScale.x *= -1;
            gameObject.transform.localScale = currentScale;
        }
        StartCoroutine(waiter());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tutorial")
        {
            AudioSource.PlayClipAtPoint(TriangleKill, transform.position, 1f);
            Destroy(gameObject);
        }
        
        if (collision.gameObject.tag == "Enemy")
        {
            AudioSource.PlayClipAtPoint(TriangleKill, transform.position, 1f);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "EnemyCheckPoint1")
        {
            AudioSource.PlayClipAtPoint(TriangleKill, transform.position, 1f);
            Destroy(gameObject);
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
