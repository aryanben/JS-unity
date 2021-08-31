using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MT : MonoBehaviour
{

    [SerializeField] List<MTcube> cubeList;
    Thread thread;
    bool tb;
    void Start()
    {
        
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            thread = new Thread(MoveCubePosition);
            thread.Start();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveCubePosition();
        }
        Debug.Log("running");
    }
    public void MoveCubePosition()
    {

        for (int i = 0; i < cubeList.Count; i++)
        {
            cubeList[i].myPos += Vector3.up * 5;
           // Thread.Sleep(21); //complex meths
            
            cubeList[i].myRot =   Quaternion.Euler(0,0, cubeList[i].myRot.eulerAngles.z+  30);
            Thread.Sleep(12); //complex meths

        }

     


    }

}
