using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{

    
    [SerializeField] GameObject Player;
    List<GameObject> levelHolder;
    List<GameObject> obstacleHolder;
    ObjectPooler oP;
    // Start is called before the first frame update
    void Start()
    {
        oP = GetComponent<ObjectPooler>();
        levelHolder = new List<GameObject>();
        obstacleHolder = new List<GameObject>();
        playerMove.OnDeath += ChangeScene;
        for (int i = 0; i < 3; i++)
        {
            GameObject level = oP.GetPooledObject("Background");
            level.transform.position = new Vector3(0, -1f + i*9.6f, 0);
            level.SetActive(true);
            levelHolder.Add(level);
        }
        generateObstaclesForRange(0, levelHolder[2].transform.position.y);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Player.transform.position.y >= levelHolder[1].transform.position.y)
        {
            levelHolder[0].SetActive(false);
            levelHolder.RemoveAt(0);
            GameObject level = oP.GetPooledObject("Background");
            level.transform.position = new Vector3(0, levelHolder[1].transform.position.y + 9.6f, 0);
            level.SetActive(true);
            levelHolder.Add(level);
            for (int i = 0; i < obstacleHolder.Count;)
            {
                if (obstacleHolder.Count==0 || obstacleHolder[i].transform.position.y < Player.transform.position.y - 3)
                {
                    obstacleHolder[i].SetActive(false);
                    obstacleHolder.RemoveAt(i);
                }
                else
                    break;
            }
            generateObstaclesForRange(levelHolder[1].transform.position.y+1, levelHolder[2].transform.position.y);
        }
    }

    private void generateObstaclesForRange(float minY, float maxY)
    {
        for (float curY = minY; curY < maxY; curY += Random.Range(2f, 5f))
        {
            for (int i = 0; i < 3; i++)
            {
                int switcher = Random.Range(0, 21);
                bool[] jumpable = { true, true, true };
                GameObject obstacle = null;
                if (switcher < 5) //можно перепрыгнуть
                {
                    obstacle = oP.GetPooledObject("JumpableObstacle");
                }
                else if (switcher >= 3 && switcher < 9) //нельзя перепрыгнуть
                {

                    if (i == 2)
                    {
                        if(jumpable[0] || jumpable[1])
                        {
                            obstacle = oP.GetPooledObject("UnjumpableObstacle");
                        }
                        else
                        {
                            obstacle = oP.GetPooledObject("JumpableObstacle");
                        }
                    }
                    else
                    {
                        jumpable[i] = false;
                        obstacle = oP.GetPooledObject("UnjumpableObstacle");
                    }
                }
                else if (switcher >= 9 && switcher <= 10) //монетка
                {
                    obstacle = oP.GetPooledObject("Coin");
                }
                else
                {
                    continue;
                }

                obstacle.transform.position = new Vector3(-1.5f + 1.5f * i, curY, 0);
                obstacle.SetActive(true);
                obstacleHolder.Add(obstacle);

            }
        }

    }


    private void ChangeScene()
    {
        StartCoroutine(deathSequence());
    }

    IEnumerator deathSequence()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Endgame");
    }

}
