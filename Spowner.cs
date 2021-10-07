using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spowner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject spowner;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Instantiate(spowner, new Vector3(0,0,0), new Quaternion(0,0,0,0));
        }

    }
}
