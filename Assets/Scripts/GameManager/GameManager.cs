using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public GameObject player;
    public Text textEnemiesKilled;
    public int enemiesKilled = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        AudioManager.instance.Play("Game");

        TimerController.instance.BeginTimer();
        EnemiesManager.instance.startSpawning();
        updateTextEnemiesDeadCount();
    }

    public void addEnnemyDead()
    {
        enemiesKilled++;
        updateTextEnemiesDeadCount();
    }

    private void updateTextEnemiesDeadCount()
    {
        textEnemiesKilled.text = enemiesKilled.ToString();
    }
}
