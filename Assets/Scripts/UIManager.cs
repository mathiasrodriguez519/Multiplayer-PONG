using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private TMP_Text _team1ScoreTxt, _team2ScoreTxt;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public void UpdateTeam1Score(string score)
    {
        _team1ScoreTxt.text = "Team 1: " + score;   
    }
    public void UpdateTeam2Score(string score)
    {
        _team2ScoreTxt.text = "Team 2: " + score;
    }
}
