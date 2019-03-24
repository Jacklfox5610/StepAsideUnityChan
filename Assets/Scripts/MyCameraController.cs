using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraController : MonoBehaviour
{
    public GameObject unitychan;
    float difference=10.0f;


    // Start is called before the first frame update
    void Start()
    {

       // this.difference = unitychan.transform.position.z - this.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {


        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, unitychan.transform.position.z - this.difference);
            
    }
}
