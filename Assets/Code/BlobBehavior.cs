using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlobBehavior : MonoBehaviour
{
    public float Speed = 10;
    private Rigidbody2D rb2d;
    private BoxCollider2D boxCollider2d;
    public Vector2 Jump = new Vector2(0, 300);
    // public bool IsJumping;
    public bool SquareUnlocked;
    public bool TriangleUnlocked;
    public bool RhombusUnlocked;
    bool facingRight = true;
    public Transform BulletSpawnLocation;
    public AudioClip Movement;
    public SpriteRenderer SpriteRenderer;
    public BoxCollider2D BlobCollider;
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
    public AudioClip FallingDeath;
    public AudioClip JumpSound;
    public AudioClip ShapeUp;
    public AudioClip Victory;
    public AudioClip TriangleAttackSound;
    [SerializeField] private LayerMask platformLayerMask;

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
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       /* if (collision.collider.tag == "Ground")
        {
            IsJumping = false;
        }
       */
        if (collision.collider.tag == "Enemy")
        {
            RestartGame();
        }

        if (collision.collider.tag == "Spikes")
        {
            RestartGame();
        }

        if (collision.collider.tag == "Tutorial")
        {
            Tutorial();
        }

        if (collision.collider.tag == "VictoryPlatform")
        {
            AudioSource.PlayClipAtPoint(Victory, Camera.main.transform.position, 2f);
        }

        /* if (collision.collider.tag == "Bullet")
        {
            IsJumping = false;
        }
        */
    }
     
    private void OnCollisionExit2D(Collision2D collision)
    {
        /* if (collision.collider.tag == "Ground")
        {
            IsJumping = true;
        }

        if (collision.collider.tag == "Bullet")
        {
            IsJumping = true;
        }
        */
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

        if (shouldJump && isGrounded())
        {
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(Jump);
            AudioSource.PlayClipAtPoint(JumpSound, Camera.main.transform.position, 1f);
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
        
        if (Input.GetKeyDown(KeyCode.K) && TriangleUnlocked == true)
        {
            SpriteRenderer.sprite = Triangle;
            Collider.size = TriangleSize;
            Speed = 3.0f;
            Jump = new Vector2(0, 200);
            CurrentShape = Shapes.Triangle;
        }

        if (Input.GetKeyDown(KeyCode.L) && RhombusUnlocked == true)
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
                AudioSource.PlayClipAtPoint(TriangleAttackSound, Camera.main.transform.position, 1f);
                if (!facingRight)
                {
                    Bullet.BulletSpeed.x *= -1;
                }
            }
        }
        
    }

    private bool isGrounded()
    {
        float extraHeightText = 0.3f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, extraHeightText, platformLayerMask);
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.blue;
        }
        else
        {
            rayColor = Color.red;
        }
        return raycastHit.collider != null;
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
            ExitGame();
        }

        if (collision.gameObject.tag == "Falling")
        {
            AudioSource.PlayClipAtPoint(FallingDeath, Camera.main.transform.position, 2f);
        }

        if (collision.gameObject.tag == "SquarePowerUp")
        {
            SquareUnlocked = true;
            Destroy(collision.gameObject);
            AudioSource.PlayClipAtPoint(ShapeUp, Camera.main.transform.position, 1f);
        }

        if (collision.gameObject.tag == "TrianglePowerUp")
        {
            TriangleUnlocked = true;
            Destroy(collision.gameObject);
            AudioSource.PlayClipAtPoint(ShapeUp, Camera.main.transform.position, 1f);
        }

        if (collision.gameObject.tag == "RhombusPowerUp")
        {
            RhombusUnlocked = true;
            Destroy(collision.gameObject);
            AudioSource.PlayClipAtPoint(ShapeUp, Camera.main.transform.position, 1f);
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(2);
    }
    
    public void ExitGame()
    {
        SceneManager.LoadScene(0);
    }

    public void Tutorial()
    {
        SceneManager.LoadScene(1);
    }
}