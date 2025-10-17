using System.Collections.Generic;
using UnityEngine;

public class TeamsManager : MonoBehaviour
{
    public static TeamsManager Instance;
    [SerializeField] public List<Transform> playerSpawns;
    public int playersInGame = 0, team1Score, team2Score;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public void PlayerJoined()
    {
        playersInGame++;
    }
    public void Team1Scored()
    {
        team1Score++;
        UIManager.Instance.UpdateTeam1Score(team1Score.ToString());
        if (team1Score >= 5)
            GameManager.Instance.FinishGame("1");
    }
    public void Team2Scored() 
    { 
        team2Score++;
        UIManager.Instance.UpdateTeam2Score(team2Score.ToString());
        if (team2Score >= 5)
            GameManager.Instance.FinishGame("2");
    }
}
