using TMPro;
using UnityEngine;

namespace MyGame.Scripts.Core
{
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;

        public void UpdateScore(int score)
        {
            scoreText.text = $"Очки: {score}";
        }
    }
}