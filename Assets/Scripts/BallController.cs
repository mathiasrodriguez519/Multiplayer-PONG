using Photon.Pun;
using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField, Range(1f, 15f)] private float _speed = 7f;
    private Vector2 _direction;
    private Vector2 _startPosition = new Vector2(0,0);
    private bool _goal;

    private void Awake()
    {
        _direction = new Vector2(-1, 0.3f);
    }
    private void Update()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        if (!_goal && GameManager.Instance.playingGame)
            transform.Translate(_speed * Time.deltaTime * _direction);
    }
    private IEnumerator ResetBall(Vector2 newDirection)
    {
        _goal = true;
        transform.position = _startPosition;
        _direction = newDirection;
        yield return new WaitForSeconds(0.5f);
        _goal = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Referencia a la paleta
            Transform paddle = collision.transform;

            // Calcular punto de impacto relativo
            float hitPoint = (transform.position.y - paddle.position.y) / (collision.collider.bounds.size.y / 2);

            // Limitar entre -1 y 1
            hitPoint = Mathf.Clamp(hitPoint, -1f, 1f);

            // Determinar nueva dirección
            float directionX = _direction.x > 0 ? -1 : 1; // invierte horizontal
            _direction = new Vector2(directionX, hitPoint).normalized;
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            _direction.y = -_direction.y;
        }
        if (collision.gameObject.CompareTag("Goal"))
        {
            Goal goal = collision.gameObject.GetComponent<Goal>();
            float randomY = Random.Range(-0.5f, 0.5f);
            if (goal._team1Goal)
            {
                TeamsManager.Instance.Team2Scored();
                StartCoroutine(ResetBall(new Vector2(1, -randomY)));
            }
            else
            {
                TeamsManager.Instance.Team1Scored();
                StartCoroutine(ResetBall(new Vector2(-1, randomY)));
            }
        }
    }
}
