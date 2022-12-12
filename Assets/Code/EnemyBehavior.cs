using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float Speed;
    public static bool MoveRight;
    bool facingRight = true;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        // if move right bool is true mean he will move to the right
        if(MoveRight)
        {
            transform.position = new Vector2(transform.position.x + (Time.deltaTime * Speed), transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x + (Time.deltaTime * -Speed), transform.position.y);
        }
    }
    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("Turn"))
        {
            if (MoveRight)
            {
                MoveRight = false;
                Flip();
            }
            else
            {
                MoveRight = true;
                Flip();
            }
        }
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "SpikeCheckPoint1")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Boundary")
        {
            Destroy(gameObject);
        }
    }
}
