using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MyGame.Scripts.Factory;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace MyGame.Scripts.Core
{
    public class ShapeSpawner : MonoBehaviour
    {
        private CancellationTokenSource _cancellationTokenSource;
        [Inject] private ShapeSettings _settings;
        [Inject] private IShapeFactory _shapeFactory;

        private void OnDestroy()
        {
            StopSpawning();
        }

        public void StartSpawning()
        {
            StopSpawning();

            _cancellationTokenSource = new CancellationTokenSource();
            SpawnRoutine(_cancellationTokenSource.Token).Forget();
        }

        public void StopSpawning()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }

        private async UniTaskVoid SpawnRoutine(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var delay = Random.Range(_settings.SpawnIntervalMin, _settings.SpawnIntervalMax);

                try
                {
                    await UniTask.Delay((int)(delay * 1000), cancellationToken: token);
                    _shapeFactory.CreateShape();
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            }
        }
    }
}