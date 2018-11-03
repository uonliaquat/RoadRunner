using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour {

    public static GameplayController instance;
    public GameObject[] obstaclePrefabs;
    public GameObject[] zombiePrefabs;
    public Transform[] lanes;
    public float min_Obstacledelay = 10f, max_ObstacleDelay = 40f;
    private float halfGroundSize;
    private PlayerController playerController;
    private float obstacle_start_postion;
    int temp_lane;
    public static GameObject obstacleParent;
    public static int  obstacleCount;
    private Text score_text;
    private int zombie_killed_count;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public Text finalScore;


    private void Awake()
    {
        MakeInstance();
        obstacleParent = GameObject.FindGameObjectWithTag("ObstacleParent");
    }
    // Use this for initialization
    void Start () {
        //halfGroundSize = GameObject.Find("GroundBlock Main").GetComponent<GroundBlock>().halfLength;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        obstacle_start_postion = 5;
        //StartCoroutine("GenerateObstacles");
        obstacleCount = 0;
        score_text = GameObject.Find("ScoreText").GetComponent<Text>();
	}

    void MakeInstance(){
        if(instance == null){
            instance = this;
        }
        else if(instance != null){
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(obstacleCount == 0){
            MakeObstacle();
        }
        DestroyGameObjects();
    }


    void DestroyGameObjects(){
        GameObject destroy = GameObject.FindGameObjectWithTag("Obstacle");
        if(destroy.transform.position.y < -10f){
            Destroy(destroy);
        }
    }

    public void MakeObstacle(){
        if (obstacleCount < 25)
        {
            obstacleCount++;
            int obstacle_no = Random.Range(0, obstaclePrefabs.Length);
            int lane = Random.Range(0, lanes.Length);
            float timer = Random.Range(1, 2);
            int random = Random.Range(0, 4);
            if (random == 0)
            {
                while (temp_lane == lane)
                {
                    lane = Random.Range(0, lanes.Length);
                }
                MakeZombies(lane);
            }
            else
            {
                StartCoroutine(PutObstacle(timer, lane, obstacle_no));
            }
            temp_lane = lane;
        }
    }

    IEnumerator PutObstacle(float timer, int lane, int obstacle_no){
        yield return new WaitForSeconds(timer);
        GameObject clone = Instantiate(obstaclePrefabs[obstacle_no], new Vector3(lanes[lane].transform.position.x, 
                                                                                 obstaclePrefabs[obstacle_no].transform.position.y, playerController.transform.position.z + 80f + (obstacle_start_postion+=2)), Quaternion.identity);
        clone.transform.SetParent(obstacleParent.transform);
        MakeObstacle();
    }

    void MakeZombies(int lane){
            int no_of_zombies = Random.Range(0, 6);
            float timer = Random.Range(1, 2);
            StartCoroutine(PutZombies(lane, no_of_zombies, timer));
        }
   

    IEnumerator PutZombies(int lane, int no_of_zombies, float timer){
        yield return new WaitForSeconds(timer);
        float z = playerController.transform.position.z + 200 + obstacle_start_postion;
        for (int i = 0; i < no_of_zombies; i++){
            int zombie_no = Random.Range(0, zombiePrefabs.Length);
            GameObject clone = Instantiate(zombiePrefabs[zombie_no], new Vector3(lanes[lane].transform.position.x + i,
                                                                                 zombiePrefabs[zombie_no].transform.position.y, z+  2f), Quaternion.identity);
            clone.transform.SetParent(obstacleParent.transform);
            MakeObstacle();
        }

    }

    public void IncreaseScore(){
        zombie_killed_count++;
        score_text.text = zombie_killed_count.ToString();
    }

    public void PauseGame(){
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame(){
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void ExitGame(){
        Time.timeScale = 1;
        //SceneManager.LoadScene("MainMenu");
    }
    public void GameOver(){
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        finalScore.text = "Killed: " + zombie_killed_count.ToString();
    }
    public void Restart(){
        Time.timeScale = 1;
        SceneManager.LoadScene("Gameplay");
    }
}
