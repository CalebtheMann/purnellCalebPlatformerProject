using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBehavior : MonoBehaviour
{
    public float Min = 2f;
    public float Max = 3f;
    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        Min = transform.position.x;
        Max = transform.position.x + 9;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.PingPong(Time.time * Speed, Max - Min) + Min, transform.position.y, transform.position.z);
    }
}
