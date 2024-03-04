using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private Vector3 startpoint;
    [SerializeField] private Vector3 endpoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(startpoint, endpoint, Mathf.PingPong(Time.time * speed, 1));
    }
}