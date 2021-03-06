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

    //石井　追加変数① 移動速度低下のフラグ
    public bool isBlind = false;
    //石井　追加変数② 速度低下時の記録用変数
    public float cameraLengthMemory;
    //石井　追加変数➂ カメラのコンポーネント取得用変数
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 9999;
        currentFrameTime = Time.realtimeSinceStartup;
        StartCoroutine("WaitForNextFrame");

        gameObject.transform.position = new Vector3(0, 0.17f, -10);

        //石井　追加部分　カメラのコンポーネント取得
        cam = this.gameObject.GetComponent<Camera>();
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

        //石井追加部分② 視界不良
        if (Input.GetKeyDown(KeyCode.C) && isBlind == false)
        {
            cameraLengthMemory = cam.orthographicSize;
            cam.orthographicSize = cam.orthographicSize * 0.5f;
            isBlind = true;
            Debug.Log("Be Blind");
        }
        //石井追加部分➂ 視界不良回復
        if (Input.GetKeyDown(KeyCode.V) && isBlind)
        {
            cam.orthographicSize = cameraLengthMemory;
            isBlind = false;
            Debug.Log("Crear Blind");
        }

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
