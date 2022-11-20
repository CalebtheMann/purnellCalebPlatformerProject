using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlobBehavior : MonoBehaviour
{
    public int Lives = 5;
    public float Speed = 10;
    private Rigidbody2D rb2d;
    private BoxCollider2D boxCollider2d;
    public Vector2 Jump = new Vector2(0, 300);
    // public bool IsJumping;
    public bool SquareUnlocked;
    public bool TriangleUnlocked;
    public bool RhombusUnlocked;
    bool facingRight = true;
    bool isJumping = false;
    bool isPuddle = false;
    bool isSquare = false;
    bool isTriangle = false;
    bool isRhombus = false;
    public Transform BulletSpawnLocation;
    public Transform SpawnLocation;
    public Transform ParaSpawnLocation;
    public Transform KingCubeLocation;
    public AudioClip Movement;
    public SpriteRenderer SpriteRenderer;
    public BoxCollider2D BlobCollider;
    public Sprite Blob;
    public Sprite BlobPuddle;
    public Sprite Square;
    public Sprite Triangle;
    public GameObject TriangleAttack;
    public GameObject LoseScreen;
    public GameObject WinScreen;
    public GameObject RhombusHorde;
    public GameObject ParaSquares;
    public GameObject KingCube;
    public Sprite Rhombus;
    public BoxCollider2D Collider;
    public Vector2 BlobSize;
    public Vector2 PuddleSize;
    public Vector2 SquareSize;
    public Vector2 BoxCastSize;
    public Vector2 TriangleSize;
    public Vector2 RhombusSize;
    public Vector3 CheckPoint;
    public AudioClip FallingDeath;
    public AudioClip JumpSound;
    public AudioClip ShapeUp;
    public AudioClip Victory;
    public AudioClip TriangleAttackSound;
    public AudioClip PuddleSound;
    public AudioClip BlobKill;
    public AudioClip CheckPointSound;
    public AudioClip MorphSound;
    public AudioClip ParaSquareSpawn;
    public AudioClip RhombusTrigger;
    public Animator MyAnimator;
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
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(BlobKill, transform.position, 1f);
            RestartGame();
        }

        if (collision.collider.tag == "Spikes")
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(BlobKill, transform.position, 1f);
            RestartGame();
        }

        if (collision.collider.tag == "EnemyCheckPoint1")
        {
            transform.position = CheckPoint;
            AudioSource.PlayClipAtPoint(BlobKill, transform.position, 1f);
            SpriteRenderer.sprite = Blob;
            Collider.size = BlobSize;
            Speed = 6.0f;
            Jump = new Vector2(0, 500);
            CurrentShape = Shapes.Blob;
            isSquare = false;
            isTriangle = false;
            isRhombus = false;
        }

        if (collision.collider.tag == "SpikeCheckPoint1")
        {
            transform.position = CheckPoint;
            AudioSource.PlayClipAtPoint(BlobKill, transform.position, 1f);
            SpriteRenderer.sprite = Blob;
            Collider.size = BlobSize;
            Speed = 6.0f;
            Jump = new Vector2(0, 500);
            CurrentShape = Shapes.Blob;
        }

        if (collision.collider.tag == "Tutorial")
        {
            Destroy(gameObject);
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
            AudioSource.PlayClipAtPoint(JumpSound, transform.position, 1f);
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
                isPuddle = false;
            }
            if (Input.GetKey(KeyCode.S))
            {
                SpriteRenderer.sprite = BlobPuddle;
                Collider.size = PuddleSize;
                Speed = 2.5f;
                Jump = new Vector2(0, 350);
                CurrentShape = Shapes.BlobPuddle;
                isPuddle = true;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                AudioSource.PlayClipAtPoint(PuddleSound, transform.position, 1f);
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            if (CurrentShape != Shapes.Blob)
            {
                AudioSource.PlayClipAtPoint(MorphSound, transform.position, 1f);
            }

            SpriteRenderer.sprite = Blob;
            Collider.size = BlobSize;
            BoxCastSize = BlobSize;
            Speed = 6.0f;
            Jump = new Vector2(0, 500);
            CurrentShape = Shapes.Blob;
            isSquare = false;
            isTriangle = false;
            isRhombus = false;
        }

        if (Input.GetKeyDown(KeyCode.J) && SquareUnlocked == true)
        {
            if (CurrentShape != Shapes.Square)
            {
                AudioSource.PlayClipAtPoint(MorphSound, transform.position, 1f);
            }

            SpriteRenderer.sprite = Square;
            Collider.size = SquareSize;
            BoxCastSize = SquareSize;
            Speed = 6.0f;
            Jump = new Vector2(0, 350);
            CurrentShape = Shapes.Square;
            isSquare = true;
            isTriangle = false;
            isRhombus = false;
        }
        
        if (Input.GetKeyDown(KeyCode.K) && TriangleUnlocked == true)
        {
            if (CurrentShape != Shapes.Triangle)
            {
                AudioSource.PlayClipAtPoint(MorphSound, transform.position, 1f);
            }

            SpriteRenderer.sprite = Triangle;
            Collider.size = TriangleSize;
            BoxCastSize = TriangleSize;
            Speed = 3.0f;
            Jump = new Vector2(0, 200);
            CurrentShape = Shapes.Triangle;
            isSquare = false;
            isTriangle = true;
            isRhombus = false;
        }

        if (Input.GetKeyDown(KeyCode.L) && RhombusUnlocked == true)
        {
            if (CurrentShape != Shapes.Rhombus)
            {
                AudioSource.PlayClipAtPoint(MorphSound, transform.position, 1f);
            }

            SpriteRenderer.sprite = Rhombus;
            Collider.size = RhombusSize;
            BoxCastSize = RhombusSize;
            Speed = 8.5f;
            Jump = new Vector2(0, 350);
            CurrentShape = Shapes.Rhombus;
            isSquare = false;
            isTriangle = false;
            isRhombus = true;
        }

        if (CurrentShape == Shapes.Triangle)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                TriangleAttack Bullet = Instantiate(TriangleAttack, BulletSpawnLocation.position, Quaternion.identity).GetComponent<TriangleAttack>();
                AudioSource.PlayClipAtPoint(TriangleAttackSound, transform.position, 1f);
                if (!facingRight)
                {
                    Bullet.BulletSpeed.x *= -1;
                }
            }
        }
        MyAnimator.SetFloat("Speed", Mathf.Abs(xMove));
        MyAnimator.SetBool("IsJumping", isJumping);
        MyAnimator.SetBool("IsPuddle", isPuddle);
        MyAnimator.SetBool("IsSquare", isSquare);
        MyAnimator.SetBool("IsTriangle", isTriangle);
        MyAnimator.SetBool("IsRhombus", isRhombus);
        isJumping = !isGrounded();
    }

    private bool isGrounded()
    {
        float extraHeightText = 0.3f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 0.1f, platformLayerMask);
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
        if (collision.gameObject.tag == "LevelTwo")
        {
            LevelTwo();
        }

        if (collision.gameObject.tag == "Goal")
        {
            Goal();
        }

        if (collision.gameObject.tag == "LevelThree")
        {
            LevelThree();
        }

        if(collision.gameObject.tag == "FinalGoal")
        {
            FinalGoal();
        }

        if (collision.gameObject.tag == "Falling")
        {
            AudioSource.PlayClipAtPoint(FallingDeath, Camera.main.transform.position, 3f);
            Speed = 0;
        }

        if (collision.gameObject.tag == "CheckPoint")
        {
            AudioSource.PlayClipAtPoint(CheckPointSound, transform.position, 2f);
        }

        if (collision.gameObject.tag == "SquarePowerUp")
        {
            SquareUnlocked = true;
            Destroy(collision.gameObject);
            AudioSource.PlayClipAtPoint(ShapeUp, transform.position, 1f);
        }

        if (collision.gameObject.tag == "TrianglePowerUp")
        {
            TriangleUnlocked = true;
            Destroy(collision.gameObject);
            AudioSource.PlayClipAtPoint(ShapeUp, transform.position, 1f);
        }

        if (collision.gameObject.tag == "RhombusPowerUp")
        {
            RhombusUnlocked = true;
            Destroy(collision.gameObject);
            AudioSource.PlayClipAtPoint(ShapeUp, transform.position, 1f);
        }

        if (collision.gameObject.tag == "Spawn")
        {
            AudioSource.PlayClipAtPoint(RhombusTrigger, Camera.main.transform.position, 2f);
            Vector2 RhombusHordePos = new Vector2(SpawnLocation.position.x, SpawnLocation.position.y);

            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    RhombusHordePos.x = SpawnLocation.position.x + ((j + 0.5f) * 3f);
                    Instantiate(RhombusHorde, RhombusHordePos, Quaternion.identity);
                }
            }
        }

        if (collision.gameObject.tag == "ParaSpawn")
        {
            AudioSource.PlayClipAtPoint(ParaSquareSpawn, Camera.main.transform.position, 2f);
            Vector2 ParaSquarePos = new Vector2(ParaSpawnLocation.position.x, ParaSpawnLocation.position.y);

            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    ParaSquarePos.x = ParaSpawnLocation.position.x + ((j + 0.5f) * 2f);
                    Instantiate(ParaSquares, ParaSquarePos, Quaternion.identity);
                }
            }
        }
    }
    public void RestartGame()
    {
        LoseScreen.SetActive(true);
    }
    
    public void BossDamage()
    {
        Lives -= 1;
        //transform.position
    }

    public void ExitGame()
    {
        SceneManager.LoadScene(0);
    }

    public void Goal()
    {
        SceneManager.LoadScene(2);
    }
    public void Tutorial()
    {
        LoseScreen.SetActive(true);
    }

    public void LevelTwo()
    {
        SceneManager.LoadScene(3);
    }

    public void LevelThree()
    {
        SceneManager.LoadScene(4);
    }

    public void FinalGoal()
    {
        WinScreen.SetActive(true);
    }
}