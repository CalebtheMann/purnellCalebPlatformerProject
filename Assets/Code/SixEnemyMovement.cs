using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SixEnemyMovement : MonoBehaviour
{
    public float Min;
    public float Max;
    public float Speed;
    bool facingRight = false;

    // Start is called before the first frame update
    void Start()
    {
        Min = transform.position.x;
        Max = transform.position.x + 6;
        facingRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.PingPong(Time.time * Speed, Max - Min) + Min, transform.position.y, transform.position.z);

        if (Min + 0.04f >= transform.position.x && !facingRight)
        {
            Flip();
        }

        if (Max - 0.04f <= transform.position.x && facingRight)
        {
            Flip();
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
    }
}
