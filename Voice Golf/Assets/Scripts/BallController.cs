using System;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;

    [SerializeField]
    private int yLimit;

    private Vector3 _lastPosion;
    private Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        SetRespawnPoint(transform.position);
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (ball.transform.position.y < yLimit)
        {
            _lastPosion = ball.transform.position;
        }
    }

    private void SetRespawnPoint(Vector3 pos)
    {
        _lastPosion = pos;
    }

    public void Respawn()
    {
        ball.transform.position = _lastPosion;
        body.velocity = Vector3.zero;
    }
}

