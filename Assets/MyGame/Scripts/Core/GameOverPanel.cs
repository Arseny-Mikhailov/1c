using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyGame.Scripts.Core
{
    public class GameOverPanel : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private Button restartButton;
    
        private void Awake()
        {
            restartButton.onClick.AddListener(OnRestartClicked);
        }
    
        public void Show(bool isWin, int score)
        {
            panel.SetActive(true);
            titleText.text = isWin ? "ПОБЕДА!" : "ПОРАЖЕНИЕ";
            scoreText.text = isWin ? $"Вы набрали: {score} очков!" : "";
            restartButton.gameObject.SetActive(true);
        }
    
        public void Hide()
        {
            panel.SetActive(false);
        }
    
        private void OnRestartClicked()
        {
            GameManager.Instance.RestartGame();
        }
    }
}