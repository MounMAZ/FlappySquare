using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   [SerializeField] private KeyCode restart;
   [SerializeField] private TMPro.TMP_Text scoreTxt;
   [SerializeField] public static int score;
   [SerializeField] private PlayerController player;
   [SerializeField] private GameObject restartInstructions;

    private void Start()
    {
        score = 0;
        restartInstructions.SetActive(false);
        player.OnScoreChange.AddListener(UpdateScoreText);
        player.OnDeath.AddListener(DisplayRestartInstructions);
    }
    void UpdateScoreText()
    {
        score++;
        scoreTxt.text = score.ToString();
    }

    void DisplayRestartInstructions()
    {
        restartInstructions.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(restart))
        {
            SceneManager.LoadScene(0);
        }
    }
}
