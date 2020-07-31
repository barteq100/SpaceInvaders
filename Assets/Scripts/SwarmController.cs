using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class SwarmController : MonoBehaviour
{
    public int EnemiesInRow;
    public int RowsCount;
    public UnityAction OnAllInvadersDeath;
    public UnityAction<Invader> OnInvaderDeath;
    public GameObject[] InvadersPrefabs;

    private List<GameObject> _spawnedEnemies;
        
    private void Awake()
    {
        _spawnedEnemies = new List<GameObject>(EnemiesInRow * RowsCount);
        var startPos = gameObject.transform.position;
        var halfRows = RowsCount / 2;
        var halfEInR = EnemiesInRow / 2;
        for (var r = -halfRows; r < halfRows; r++)
        {
            var moveX = r % 2 == 0 ? 0 : 7.5f;
            for (var i = -halfEInR; i < halfEInR; i++)
            {
                var position = new Vector3(moveX + startPos.x + (i * 15f), startPos.y + (r * 10), startPos.z);
                var prefabId = Random.Range(0, InvadersPrefabs.Length);
                var prefab = InvadersPrefabs[prefabId];
                var invader = Instantiate(prefab, position, prefab.transform.rotation);
                invader.transform.parent = transform;
                _spawnedEnemies.Add(invader);
                invader.GetComponent<DestroyOnHit>().RegisterOnDeath(InvaderDied);
            }
        }
    }

    private void InvaderDied(GameObject invader)
    {
        _spawnedEnemies.Remove(invader);
        OnInvaderDeath?.Invoke(invader.GetComponent<Invader>());
        if (_spawnedEnemies.Count == 0)
        {
            OnAllInvadersDeath?.Invoke();
        }
        
    }
}
