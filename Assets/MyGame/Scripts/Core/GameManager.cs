using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace MyGame.Scripts.Core
{
    public class GameManager : MonoBehaviour
    {
        [Header("UI References")] [SerializeField]
        private ScoreDisplay scoreDisplay;

        [SerializeField] private LivesDisplay livesDisplay;
        [SerializeField] private GameOverPanel gameOverPanel;

        private readonly List<ShapeItem> _activeShapes = new();
        private int _currentLives;

        private int _currentScore;
        [Inject] private ShapeSettings _shapeSettings;
        [Inject] private ShapeSpawner _shapeSpawner;

        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            ResetGame();
            SubscribeToEvents();
        }

        private void ResetGame()
        {
            DOTween.KillAll();

            ClearAllShapes();

            _currentScore = 0;
            _currentLives = _shapeSettings.InitialLives;

            scoreDisplay.UpdateScore(_currentScore);
            livesDisplay.UpdateLives(_currentLives);
            gameOverPanel.Hide();

            _shapeSpawner.StartSpawning();
        }

        private void ClearAllShapes()
        {
            foreach (var shape in _activeShapes.Where(shape => shape != null)) Destroy(shape.gameObject);

            _activeShapes.Clear();
        }

        public void RegisterShape(ShapeItem shape)
        {
            _activeShapes.Add(shape);
        }

        public void UnregisterShape(ShapeItem shape)
        {
            _activeShapes.Remove(shape);
        }

        public void RestartGame()
        {
            Time.timeScale = 1f;
            ResetGame();
        }

        private void SubscribeToEvents()
        {
            EventBus.Subscribe<ShapeSortedEvent>(OnShapeSorted);
            EventBus.Subscribe<LifeLostEvent>(OnLifeLost);
        }

        private void OnShapeSorted(ShapeSortedEvent e)
        {
            _currentScore += ShapeSortedEvent.Points;

            scoreDisplay.UpdateScore(_currentScore);

            CheckWinCondition();
        }

        private void OnLifeLost(LifeLostEvent e)
        {
            _currentLives -= LifeLostEvent.LivesLost;
            livesDisplay.UpdateLives(_currentLives);

            if (_currentLives <= 0) EndGame(false);
        }

        private void CheckWinCondition()
        {
            if (_currentScore >= _shapeSettings.ShapesToWin) EndGame(true);
        }

        private void EndGame(bool isWin)
        {
            ClearAllShapes();
            Time.timeScale = 0f;
            gameOverPanel.Show(isWin, _currentScore);
            _shapeSpawner.StopSpawning();
        }
    }
}