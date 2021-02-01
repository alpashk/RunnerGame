using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{

    Text score;
    Text coin;
    // Start is called before the first frame update
    void Awake()
    {
        score = transform.GetChild(2).GetComponent<Text>();
        coin = transform.GetChild(1).GetComponent<Text>();
        GameMaster.UpdateUI += UpdateCoin;
        playerMove.Score += UpdateScore;
    }

    void UpdateCoin(int coins)
    {
        coin.text = coins.ToString();
    }

    void UpdateScore(float Score)
    {
        score.text = ((int)Score).ToString();
    }

    private void OnDestroy()
    {
        GameMaster.UpdateUI -= UpdateCoin;
        playerMove.Score -= UpdateScore;
    }



}
