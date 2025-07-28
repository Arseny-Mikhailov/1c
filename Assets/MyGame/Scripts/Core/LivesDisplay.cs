using TMPro;
using UnityEngine;

namespace MyGame.Scripts.Core
{
    public class LivesDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text livesText;

        public void UpdateLives(int lives)
        {
            livesText.text = $"Жизни: {lives}";
        }
    }
}