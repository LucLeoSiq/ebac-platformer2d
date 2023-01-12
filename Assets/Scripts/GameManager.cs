using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using DG.Tweening;

public class GameManager : Singleton<GameManager>
{
    [Header("Player")]
    public GameObject playerPrefab;

    [Header("Enemies")]
    public List<GameObject> enemies;

    [Header("References")]
    public Transform startPoint;

    [Header("Animation")]
    public float duration = .2f;
    public float delay = .05f;
    public Ease ease = Ease.OutBack;

    private GameObject _currentplayer;

    public void Awake()
    {
        Init();
    }
    
    public void Init()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        _currentplayer = Instantiate(playerPrefab);
        _currentplayer.transform.position = startPoint.transform.position;
        _currentplayer.transform.DOScale(0, duration).SetEase(ease).From().SetDelay(delay);
    }
}
