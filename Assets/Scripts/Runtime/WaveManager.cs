using System;
using System.Collections;
using System.Collections.Generic;
using GameJam.Miura;
using Michsky.UI.ModernUIPack;
using UniRx;
using UnityEngine;


public class WaveManager : MonoBehaviour
{
    [SerializeField]
    private EnemyFactoryComponent enemyFactoryComponent;

    [SerializeField]
    private SliderManager sliderManager;

    public int WaveCount => enemyFactoryComponent.DownEnemyCount.Value;

    public Subject<Unit> OnWaveStart { get; private set; }

    private void Awake()
    {
        OnWaveStart = new Subject<Unit>();
        enemyFactoryComponent = GameObject.Find("EnemyFactory").GetComponent<EnemyFactoryComponent>();
    }

    void Start()
    {
        sliderManager.mainSlider.value = 0;

        enemyFactoryComponent.DownEnemyCount
            .Where(value => value != 0)
            .Subscribe(value =>
            {
                sliderManager.mainSlider.value = value * 0.1f;
                enemyFactoryComponent.InvokeRangeEnemy();
            })
            .AddTo(this);

        OnWaveStart
            .Delay(TimeSpan.FromSeconds(2f))
            .Subscribe(_ =>
            {
                Debug.Log("WaveStart");
                enemyFactoryComponent.InvokeRangeEnemy();
            })
            .AddTo(this);

        Observable
            .Timer(TimeSpan.FromSeconds(2f))
            .Subscribe(_ =>
            {
                WaveStart();
            })
            .AddTo(this);
    }

    public void WaveStart()
    {
        OnWaveStart.OnNext(Unit.Default);
    }
}
