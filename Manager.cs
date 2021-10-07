using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public Player player;
    public GameObject playerPrefab;
    public GameObject playerObject;
    public GameObject BGM;
    public GameObject BGMspeaker;
    bool soundFlg = false;
    bool createFlg = false;

    public GameObject tutorial1;
    public GameObject tutorial2;
    public GameObject tutorial3;
    public GameObject tutorial4;
    public GameObject tutorial5;
    public GameObject restartMessage;

    private bool finishTutorial1 = false;
    private bool finishTutorial2 = false;
    private bool finishTutorial3 = false;
    private bool finishTutorial4 = false;
    private bool finishTutorial5 = false;
    private GameObject tutorialMessage;
    void Start()
    {
        playerObject = Instantiate(playerPrefab, new Vector3(0.24f, 0.5f, 0), new Quaternion(0, 0, 0, 0));
        player = playerObject.GetComponent<Player>();
        tutorialMessage = Instantiate(tutorial1, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {

        //  tutorial
        if (Input.GetKeyDown(KeyCode.Space) && finishTutorial1 == false)
        {
            Destroy(tutorialMessage);
            tutorialMessage = Instantiate(tutorial2, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            finishTutorial1 = true;
        }
        if ((Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.N)) && finishTutorial1 == true && finishTutorial2 == false)
        {
            Destroy(tutorialMessage);
            tutorialMessage = Instantiate(tutorial3, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            finishTutorial2 = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && finishTutorial1 == true && finishTutorial2 ==true && finishTutorial3 == false)
        {
            Destroy(tutorialMessage);
            tutorialMessage = Instantiate(tutorial4, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            finishTutorial3 = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.LeftShift) && finishTutorial1 == true && finishTutorial2 == true && finishTutorial3 == true && finishTutorial4 == false)
        {
            Destroy(tutorialMessage);
            tutorialMessage = Instantiate(tutorial5, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            finishTutorial4 = true;
        }


        if (player == null)
        {
            playerObject = GameObject.Find("player(Clone)");
            player = playerObject.GetComponent<Player>();
        }
        if (player.isDead == true && createFlg == false)// 死亡
        {
            Destroy(playerObject);
            playerObject = Instantiate(playerPrefab, new Vector3(0.24f, 0.5f, 0), new Quaternion(0, 0, 0, 0));
            createFlg = true;
            Destroy(BGMspeaker);
            soundFlg = false;
            tutorialMessage = Instantiate(restartMessage, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));

        }
        if (Input.GetKeyDown(KeyCode.Z) && soundFlg == false) // ゲームスタート
        {
            Destroy(tutorialMessage);

            soundFlg = true;
            createFlg = false;
            BGMspeaker = Instantiate(BGM, gameObject.transform.position, gameObject.transform.rotation);
        }
    }
}
