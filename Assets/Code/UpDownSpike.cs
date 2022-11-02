using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownSpike : MonoBehaviour
{
    public float Min = 2f;
    public float Max = 3f;
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        Min = transform.position.y;
        Max = transform.position.y + 6f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * Speed, Max - Min) + Min, transform.position.z);
    }
}
