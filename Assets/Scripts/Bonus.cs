using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, 20.0f * Time.deltaTime, 0.0f);
        transform.position = new Vector3(transform.position.x, transform.position.y,  transform.position.z - 1.0f*Time.deltaTime);
    }
}
