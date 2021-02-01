using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndgameUI : MonoBehaviour
{
    Text Highscore;
    Text Coin;
    Text Score;
    // Start is called before the first frame update
    void Awake()
    {
        Score = transform.GetChild(0).GetComponent<Text>();
        Highscore = transform.GetChild(1).GetComponent<Text>();
        Coin = transform.GetChild(2).GetComponent<Text>();
        GameMaster.UpdateEndgameUI += UpdateUI;
    }
    void UpdateUI(int coin, int score, int highscore)
    {
        Coin.text = coin.ToString();
        Score.text = score.ToString();
        Highscore.text = highscore.ToString();
    }

    private void OnDestroy()
    {
        GameMaster.UpdateEndgameUI -= UpdateUI;
    }
}
