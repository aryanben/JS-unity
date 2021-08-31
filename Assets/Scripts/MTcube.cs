using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MTcube : MonoBehaviour
{
    public Vector3 myPos;
    public Quaternion myRot;
    void Start()
    {
        myPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = myPos;
        transform.rotation = myRot;
    }
}
