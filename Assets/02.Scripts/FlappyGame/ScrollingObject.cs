using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    // Update is called once per frame
    void Update()
    {
        if (FlappyGameManager.Instance.isGameOver) return;

        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
