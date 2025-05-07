using UnityEngine;

public class AddScore : MonoBehaviour
{
    [SerializeField] int score;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FlappyGameManager.Instance.AddScore(score);
        }
    }
}
