using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardHandler : MonoBehaviour
{

    public RectTransform leaderBoardRect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showLeaderBoard()
    {
        leaderBoardRect.gameObject.SetActive(true);
    }
    public void closeLeaderBoard()
    {
        leaderBoardRect.gameObject.SetActive(false);
    }
}
