using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingCubeCode : MonoBehaviour
{
    public int Lives = 3;
    public Vector3 KingCubeLocation;
    public Vector3 KingCubeLocation2;
    public GameObject MinionSpawn;
    public GameObject SquareHorde;
    public GameObject KingCubePortrait;
    public AudioClip EndVictory;

    // public EnemyBehavior EnemyBehavior;
    bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnMinions", 1f, 2.3f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage()
    {
        Lives --; // lives -- means the same thing as lives -= 1 (same for lives ++ too)

        if (Lives <= 0)
        {
            Destroy(gameObject);
            KingCubePortrait.SetActive(true);
            AudioSource.PlayClipAtPoint(EndVictory, Camera.main.transform.position, 1f);
        }

        if (Lives == 2)
        {
            transform.position = KingCubeLocation;
            Flip();
            
        }

        if (Lives == 1)
        {
            transform.position = KingCubeLocation2;
            Flip();
        }
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
        EnemyBehavior.MoveRight = !EnemyBehavior.MoveRight;
    }

    private void spawnMinions()
    {
        Instantiate(SquareHorde, MinionSpawn.transform.position, Quaternion.identity);
    }
}
