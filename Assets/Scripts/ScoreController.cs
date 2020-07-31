using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text Score;
    public EnemySpawnPoint SpawnPoint;
    private int _score = 0;

    void Awake()
    {
        SpawnPoint.OnInvaderDeath += OnInvaderDeath;
    }

    public void OnInvaderDeath(Invader invader)
    {
        _score += invader.InvaderType.GetInvaderScore();
        Score.text = $"Score {_score}";
    }
}

   
