using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    Text coin;
    // Start is called before the first frame update
    void Start()
    {
        coin = transform.GetChild(0).GetComponent<Text>();
        GameMaster.UpdateUI += UpdateCoin;
    }

    void UpdateCoin(int coins)
    {
        coin.text = coins.ToString();
    }

    private void OnDestroy()
    {
        GameMaster.UpdateUI -= UpdateCoin;
    }


    IEnumerator updateAlready()
    {
        yield return new WaitForFixedUpdate();
        
    }
}
