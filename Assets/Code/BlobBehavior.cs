using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlobBehavior : MonoBehaviour
{
    public float Speed = 10;
    private Rigidbody2D rb2d;
    public Vector2 Jump = new Vector2(0, 300);
    public bool IsJumping;
    public bool SquareUnlocked;
    bool facingRight = true;
    public Transform BulletSpawnLocation;
    public AudioClip Movement;
    public SpriteRenderer SpriteRenderer;
    public Sprite Blob;
    public Sprite BlobPuddle;
    public Sprite Square;
    public Sprite Triangle;
    public GameObject TriangleAttack;
    public Sprite Rhombus;
    public BoxCollider2D Collider;
    public Vector2 BlobSize;
    public Vector2 PuddleSize;
    public Vector2 SquareSize;
    public Vector2 TriangleSize;
    public Vector2 RhombusSize;
    public enum Shapes
    {
        Blob,
        BlobPuddle,
        Square,
        Triangle,
        Rhombus
    }

    public Shapes CurrentShape;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Collider = GetComponent<BoxCollider2D>();
        BlobSize = Collider.size;
        SpriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer.sprite = Blob;
        Jump = new Vector2(0, 500);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            IsJumping = false;
        }

        if (collision.collider.tag == "Enemy")
        {
            RestartGame();
        }

        if (collision.collider.tag == "Bullet")
        {
            IsJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            IsJumping = true;
        }

        if (collision.collider.tag == "Bullet")
        {
            IsJumping = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxis("Horizontal");

        if (xMove > 0 && !facingRight)
        {
            Flip();
        }

        if (xMove < 0 && facingRight)
        {
            Flip();
        }

        Vector3 newPosition = gameObject.transform.position;

        newPosition.x += xMove * Speed * Time.deltaTime;
        transform.position = newPosition;

        bool shouldJump = (Input.GetKeyDown(KeyCode.W));

        if (shouldJump && IsJumping == false)
        {
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(Jump);
        }

        if (CurrentShape == Shapes.Blob || CurrentShape == Shapes.BlobPuddle)
        {
            if (Input.GetKeyUp(KeyCode.S))
            {
                SpriteRenderer.sprite = Blob;
                Collider.size = BlobSize;
                Speed = 6.0f;
                Jump = new Vector2(0, 500);
                CurrentShape = Shapes.Blob;
            }
            if (Input.GetKey(KeyCode.S))
            {
                SpriteRenderer.sprite = BlobPuddle;
                Collider.size = PuddleSize;
                Speed = 2.5f;
                Jump = new Vector2(0, 350);
                CurrentShape = Shapes.BlobPuddle;
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            SpriteRenderer.sprite = Blob;
            Collider.size = BlobSize;
            Speed = 6.0f;
            Jump = new Vector2(0, 500);
            CurrentShape = Shapes.Blob;
        }

        if (Input.GetKeyDown(KeyCode.J) && SquareUnlocked == true)
        {
            SpriteRenderer.sprite = Square;
            Collider.size = SquareSize;
            Speed = 6.0f;
            Jump = new Vector2(0, 350);
            CurrentShape = Shapes.Square;
        }
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            SpriteRenderer.sprite = Triangle;
            Collider.size = TriangleSize;
            Speed = 3.0f;
            Jump = new Vector2(0, 200);
            CurrentShape = Shapes.Triangle;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            SpriteRenderer.sprite = Rhombus;
            Collider.size = RhombusSize;
            Speed = 8.5f;
            Jump = new Vector2(0, 350);
            CurrentShape = Shapes.Rhombus;
        }

        if (CurrentShape == Shapes.Triangle)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                TriangleAttack Bullet = Instantiate(TriangleAttack, BulletSpawnLocation.position, Quaternion.identity).GetComponent<TriangleAttack>();
                if (!facingRight)
                {
                    Bullet.BulletSpeed.x *= -1;
                }
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Exit")
        {
            Debug.Log("ggs you made it");
        }

        if (collision.gameObject.tag == "SquarePowerUp")
        {
            SquareUnlocked = true;
            Destroy(collision.gameObject);
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}