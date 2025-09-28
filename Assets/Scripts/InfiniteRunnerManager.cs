using UnityEngine;
using UnityEngine.SceneManagement;

public class InfiniteRunnerManager : MonoBehaviour
{
    //public bool inputWasDown;
    public KeyCode leftInputKeyCode;
    public KeyCode rightInputKeyCode;
    public bool leftInput;
    public bool rightInput;
    public static float speed = 20f;
    public int score;
    public Transform playerPos;
    public Transform leftPos;
    public Transform centerPos;
    public Transform rightPos;
    public Transform leftObstPos;
    public Transform centerObstPos;
    public Transform rightObstPos;

    public float hInput;
    public GameObject obstacle;
    public float obstacleSpawnTime = 2f;
    public float obstacleSpawnTimer;
    public GameObject coin;
    public float coinSpawnTime;
    public float coinSpawnTimer;
    private void Start()
    {
        speed = 20f;
        Time.timeScale = 1f;
        obstacleSpawnTimer = obstacleSpawnTime;
        coinSpawnTime = obstacleSpawnTime * 2;
        coinSpawnTimer = coinSpawnTime;
        score = 0;
    }


    public void GetSwipe()
    {
        if(Input.touchCount>0)
        {

        }
    }

    void Update()
    {
        obstacleSpawnTimer -= Time.deltaTime;
        coinSpawnTimer -= Time.deltaTime;
        if (obstacleSpawnTimer <= 0)
        {
            int rand = Random.Range(0, 3);
            SpawnObstacle(rand switch
            {
                0 => leftObstPos,
                1 => centerObstPos,
                2 => rightObstPos,
                _ => centerObstPos,
            });
            //obstacleSpawnTime *= 0.95f;
            obstacleSpawnTimer = obstacleSpawnTime;
            if (coinSpawnTimer <= 0)
            {
                //int rand = Random.Range(0, 3);
                switch (rand)
                {
                    case 0:
                        SpawnCoin(centerObstPos);
                        SpawnCoin(rightObstPos);
                        break;
                    case 1:
                        SpawnCoin(leftObstPos);
                        SpawnCoin(rightObstPos);
                        break;
                    case 2:
                        SpawnCoin(leftObstPos);
                        SpawnCoin(centerObstPos);
                        break;

                }

                coinSpawnTimer = coinSpawnTime;
            }
        }
        getMoveLeft();
        getMoveRight();
        getHInputDown();
        if ((leftInput || rightInput))
        {
            //inputWasDown = true;
            switch (hInput)
            {
                case -1:
                    if (playerPos.position == centerPos.position)
                    {
                        playerPos.position = leftPos.position;
                    }
                    else if (playerPos.position == rightPos.position)
                    {
                        playerPos.position = centerPos.position;
                    }
                    break;
                case 1:
                    if (playerPos.position == centerPos.position)
                    {
                        playerPos.position = rightPos.position;
                    }
                    else if (playerPos.position == leftPos.position)
                    {
                        playerPos.position = centerPos.position;
                    }
                    break;
            }
        }
        //else
        //    inputWasDown = false;
    }
    public void getMoveLeft()
    {
        leftInput = Input.GetKeyDown(leftInputKeyCode);
    }
    public void getMoveRight()
    {
        rightInput = Input.GetKeyDown(rightInputKeyCode);

    }
    public void getHInputDown()
    {
        if (rightInput)
        {
            hInput = 1f;
            //Debug.Log(hInput);
        }
        else if (leftInput)
        {
            //Debug.Log(hInput);
            hInput = -1f;

        }
        else
            hInput = 0f;
        //hInput = rightInput? 1 : 0;
        //hInput = leftInput? -1 : hInput;
    }
    public void SpawnObstacle(Transform pos)
    {
        Instantiate(obstacle, pos.position, Quaternion.identity);
    }
    public void SpawnCoin(Transform pos)
    {
        Instantiate(coin, pos.position,Quaternion.identity);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("score"))
        {
            score++;
            speed *= 1.05f;
        }
        if (other.CompareTag("coin"))
        {
            Destroy(other.gameObject);
        }
        if (other.CompareTag("obstacle"))
        {
            Destroy(other.gameObject);
            Time.timeScale = 0f;
            SceneManager.LoadScene(0);
        }
    }
}
