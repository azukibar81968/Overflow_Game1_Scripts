using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
public class Player : MonoBehaviour
{
    public float speed = 0.01f;
    public float thrust = 1.0f;
    public float superthrust = 1.5f;
    private Rigidbody2D rb2D;
    public OnGround onGround;
    public bool isGround = false;
    public bool isDead = false;
    public bool isSquat = false;
    public GameObject bomb;
    public AudioSource jump;
    public AudioSource superjump;
    public AudioSource squat;
    public AudioSource shot;
    public float Rate = 50.0f;
    float currentFrameTime;
    private float updateLen = 0;
    private bool playerDeadCheck = false;
    public EnemyCheck EnemyCheck;
    public bool enemyHit = false;


    // Start is called before the first frame update
    void Start()
    {
        rb2D = this.gameObject.GetComponent<Rigidbody2D>();
        isDead = false;
        AudioSource[] audioSources = GetComponents<AudioSource>();
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 9999;
        currentFrameTime = Time.realtimeSinceStartup;
        StartCoroutine("WaitForNextFrame");
    }

    IEnumerator WaitForNextFrame()
    {
        while (true)
        {
            this.gameObject.transform.Translate(updateLen, 0, 0);

            yield return new WaitForEndOfFrame();
            currentFrameTime += 1.0f / Rate;
            var t = Time.realtimeSinceStartup;
            var sleepTime = currentFrameTime - t - 0.01f;
            if (sleepTime > 0)
                Thread.Sleep((int)(sleepTime * 1000));
            while (t < currentFrameTime)
                t = Time.realtimeSinceStartup;
        }
    }

    private void FixedUpdate()
    {
        this.gameObject.transform.Translate(updateLen, 0, 0);
        
        isGround = onGround.IsGround();
        enemyHit = EnemyCheck.IsGround();
    }
    // Update is called once per frame
    private void Update()
    {

        //update

        if (Input.GetKeyDown(KeyCode.Z))
        {
            updateLen = speed;
            isDead = false;
        }

        //input manager
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
        {
            if (isSquat == true)
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
                isSquat = false;

                rb2D.velocity = new Vector3(0.006f, superthrust) * 8;
                Debug.Log("super jump");
                superjump.PlayOneShot(superjump.clip);
                squat.Stop();
            }
            else
            {
                rb2D.velocity = new Vector3(0, thrust) * 8;
                Debug.Log("jump");
                jump.PlayOneShot(jump.clip);

            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && isSquat == false)
        {
            gameObject.transform.localScale = new Vector3(1.1f, 0.5f, 1);
            isSquat = true;
            Debug.Log("squat");
            squat.Play();

        }
        if (Input.GetKey(KeyCode.LeftShift) == false && isSquat == true)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            isSquat = false;
            Debug.Log("un-squat");
            squat.Stop();
        }
        if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.N))
        {
            Instantiate(bomb, gameObject.transform.position, gameObject.transform.rotation);
            Debug.Log("bomb");
            shot.PlayOneShot(shot.clip);
        }


        //check
        if (gameObject.transform.position.y < -8.0f)
        {
            isDead = true;
        }
    }

}


