using UnityEngine;

public class GameLimitRespawn : MonoBehaviour
{
    [SerializeField]
    private BallController ball;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            ball.Respawn();
        }
    }
}
