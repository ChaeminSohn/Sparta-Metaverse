using UnityEngine;

public class GameOverObject : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FlappyGameManager.Instance?.GameOver();
        }
    }
}
