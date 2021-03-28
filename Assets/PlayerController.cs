using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public KeyCode jump;
    public KeyCode restart;
    public int force;
    public static bool isAlive = true;
    public TMPro.TMP_Text scoreTxt;
    private Rigidbody2D _rigidbody2D;

    //In production, this should be handled by a game manager and not the player
    private int score;
    
    void Start()
    {
        isAlive = true;
        score = 0;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(jump) && isAlive)
        {
            _rigidbody2D.AddForce(Vector3.up * Mathf.Abs(Physics.gravity.y) * force, ForceMode2D.Force);
        }
        if (Input.GetKeyDown(restart))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Score")
        {
            score++;
            scoreTxt.text = score.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Pipe")
        {
            isAlive = false;
        }
    }
}
