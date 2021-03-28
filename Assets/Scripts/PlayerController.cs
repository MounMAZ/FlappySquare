using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static bool isAlive = true;
    public UnityEvent OnScoreChange;
    public UnityEvent OnDeath;

    [SerializeField] private KeyCode jump;
    [SerializeField] private int force;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    
    void Start()
    {
        isAlive = true;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(jump) && isAlive)
        {
            _rigidbody2D.AddForce(Vector3.up * Mathf.Abs(Physics.gravity.y) * force, ForceMode2D.Force);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == Constants.Tags.Score)
        {
            OnScoreChange.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == Constants.Tags.Pipe || collision.transform.tag == Constants.Tags.Floor)
        {
            isAlive = false;
            OnDeath.Invoke();
        }
    }
}
