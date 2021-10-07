using System.Collections;
using System.Threading;
using UnityEngine;


public class CamaraControll : MonoBehaviour
{
    public float speed = 0.01f;
    private float updateLen = 0;
    public float Rate = 50.0f;
    float currentFrameTime;
    public Player player;
    public GameObject playerPrefab;
    public GameObject playerObject;
    public GameObject BGM;
    private bool playerDeadCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 9999;
        currentFrameTime = Time.realtimeSinceStartup;
        StartCoroutine("WaitForNextFrame");

        gameObject.transform.position = new Vector3(0, 0.17f, -10);

    }

    // Update is called once per frame
    private void Update()
    {
        if (player == null)
        {
            playerObject = GameObject.Find("player(Clone)");
            player = playerObject.GetComponent<Player>();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            playerDeadCheck = false;
            updateLen = speed;
        }

        if (player.isDead == true && playerDeadCheck == false)// 死亡
        {
            gameObject.transform.position = new Vector3(0, 0.17f, -10);
            playerDeadCheck = true;
            updateLen = 0;
        }
        this.gameObject.transform.Translate(updateLen, 0, 0);
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
}
