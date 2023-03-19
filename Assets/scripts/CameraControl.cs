using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
  
    public GameObject actor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (actor != null)
        {
            this.transform.position = new Vector3(actor.transform.position.x, actor.transform.position.y + 2, this.transform.position.z);
        }
    }
}
