using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBoundaruy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -1.9f, 1.9f), transform.position.y, Mathf.Clamp(transform.position.z, -2.4f, 2.85f));
    }
}
