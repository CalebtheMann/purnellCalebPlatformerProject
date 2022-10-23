using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStomp : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public Vector2 Jump = new Vector2(0, 300);
    private BlobBehavior blob;

    // Start is called before the first frame update

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        blob = GetComponent<BlobBehavior>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weak Point")
        {
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(Jump);
            Destroy(collision.transform.parent.gameObject);
        }
        
        if (blob.CurrentShape == BlobBehavior.Shapes.Square)
        {
            if (collision.gameObject.tag == "Triangle Weak Point")
            {
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(Jump);
                Destroy(collision.transform.parent.gameObject);
            }
        }

        if (blob.CurrentShape == BlobBehavior.Shapes.Square)
        {
            if (collision.gameObject.tag == "Rhombus Weak Point")
            {
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(Jump);
            }
        }
    }
}
