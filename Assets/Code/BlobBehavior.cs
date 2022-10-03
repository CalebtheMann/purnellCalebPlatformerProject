using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobBehavior : MonoBehaviour
{
    public float Speed = 10;
    bool beenHit = false;
    private Rigidbody2D rb2d;
    public Vector2 Jump = new Vector2(0, 300);

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxis("Horizontal");

        Vector3 newPosition = gameObject.transform.position;

        newPosition.x += xMove * Speed * Time.deltaTime;
        transform.position = newPosition;

        bool shouldJump = (Input.GetKeyUp(KeyCode.W));

        if (shouldJump && !beenHit)
        {
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(Jump);
        }
    }
}
