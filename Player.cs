using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
public class Player : MonoBehaviour
{
    public float speed;
    public float thrust = 1.0f;
    public float superthrust = 1.5f;
    private Rigidbody2D rb2D;
    public OnGround onGround;
    public bool isGround = false;
    public bool isDead = false;
    public bool isSquat = false;
    public bool isShrink = false;
    
    //石井　追加変数① 移動速度低下のフラグ
    public bool isSlow = false;
    //石井　追加変数② 速度低下時の記録用変数
    public float speedMemory;

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

        float horizontalKey = Input.GetAxis("Horizontal");
        float xSpeed = 0.0f;

        if (Input.GetKeyDown(KeyCode.Z))
        {
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

        if (horizontalKey > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            xSpeed = speed;
        }
        else if (horizontalKey < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            xSpeed = -speed;
        }
        else
        {
            xSpeed = 0.0f;
        }
        rb2D.velocity = new Vector2(xSpeed, rb2D.velocity.y);



        if (Input.GetKeyDown(KeyCode.X) && isShrink == false)
        {
            gameObject.transform.localScale = new Vector3(0.3f, 0.3f, 1);
            isShrink = true;
            Debug.Log("shrink");
            squat.Play();
        }
        if (Input.GetKey(KeyCode.X) == false && isShrink == true)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            isShrink = false;
            Debug.Log("un-shrink");
            squat.Stop();
        }


        //check
        if (gameObject.transform.position.y < -8.0f)
        {
            isDead = true;
        }

        //石井追加部分① 移動速度低下
        if (Input.GetKeyDown(KeyCode.Q) && isSlow == false)
        {
            speedMemory = updateLen;
            updateLen *= 0.5f;
            isSlow = true;
            Debug.Log("speed down");
        }
        //石井追加部分② 移動速度回復
        if (Input.GetKeyDown(KeyCode.E) && isSlow)
        {
            updateLen = speedMemory;
            isSlow = false;
            Debug.Log("speed return");
        }

    }

}


