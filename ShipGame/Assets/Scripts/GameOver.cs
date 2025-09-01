using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject _gameOverPanel;
    [SerializeField] PauseSetter _pauseSetter;
    public void ShowGameOver()
    {
        _gameOverPanel.SetActive(true);
        _pauseSetter.SetPause(true);
    }
    public void RestartLevel()
    {
        PlayerStats.ResetStats();
        PauseSetter.ForceUnpause();
    }
}

