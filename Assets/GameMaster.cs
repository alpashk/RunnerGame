using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public int HighScore { get; set; }
    public int Score { get; set; }
    public int Coins;

    public delegate void CoinUpdate(int coinCount);
    public static event CoinUpdate UpdateUI;

    public delegate void EndgameUI(int coincount, int score, int highscore);
    public static event EndgameUI UpdateEndgameUI;

    // Start is called before the first frame update

    private void Start()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("ScoreMaster");
        Coins = PlayerPrefs.GetInt("Coins", 0);
        if (obj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else {
            DontDestroyOnLoad(this.gameObject);
            HighScore = PlayerPrefs.GetInt("HighScore", 0);

            Coins = PlayerPrefs.GetInt("Coins", 0);

            playerMove.OnDeath += DeathHandler;
            playerMove.OnCoin += CoinHandler;
            playerMove.Score += ScoreUpdate;

            StartCoroutine(UpdateCoin());
        }
    }

    void ScoreUpdate(float score)
    {
        Score =(int) score;
    }

    void OnLevelWasLoaded(int level)
    {
        if(level == 0)
        {
            StartCoroutine(UpdateCoin());
            UpdateEndgameUI = null;
        }
        else if (level == 1)
        {
            StartCoroutine(UpdateCoin());
            playerMove.OnDeath += DeathHandler;
            playerMove.OnCoin += CoinHandler;
            playerMove.Score += ScoreUpdate;
            UpdateEndgameUI = null;
        }
        else if (level == 2)
        {
            UpdateEndgameUI(Coins, Score, HighScore);
            UpdateUI = null;
        }
    }

    IEnumerator UpdateCoin()
    {
        yield return new WaitForFixedUpdate();
        UpdateUI(Coins);
    }


    void DeathHandler()
    {
        if(Score>HighScore)
        {
            HighScore = Score;
            PlayerPrefs.SetInt("HighScore", Score);
        }
        PlayerPrefs.SetInt("Coins", Coins);
    }

    void CoinHandler()
    {
        Coins++;
        UpdateUI(Coins);
    }


    // Update is called once per frame
    void Update()
    {
    }

}
