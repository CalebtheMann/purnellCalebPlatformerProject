using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public float yOffset = 1f;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 newPos = new Vector3(target.position.x, -0.9715f, -1f);
        Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 newPos = new Vector3(target.position.x, -0.9715f, -1f);
            transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
        }
    }
}
