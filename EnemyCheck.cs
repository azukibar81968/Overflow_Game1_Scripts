using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheck : MonoBehaviour
{
    public string groundTag = "Enemy";

    private bool isGround = false;
    public bool isGroundEnter, isGroundStay, isGroundExit;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("onTriggerEnter2D");
        if (collision.tag == groundTag)
        {
            isGroundEnter = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("onTriggerStay2D");

        if (collision.tag == groundTag)
        {
            isGroundStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("onTriggerExit2D");

        if (collision.tag == groundTag)
        {
            isGroundExit = true;
        }
    }
    public bool IsGround()
    {
        if (isGroundEnter || isGroundStay)
        {
            isGround = true;
        }
        else if (isGroundExit)
        {
            isGround = false;
        }

        isGroundEnter = false;
        isGroundStay = false;
        isGroundExit = false;
        
        return isGround;
    }

}
