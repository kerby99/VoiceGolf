using System;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    [SerializeField]
    private float attractionForce = 10.0f;

    [SerializeField]
    private int[] score;

    [SerializeField]
    private LineForce _lf;

    private Collider vortexCollider;
    private const int numberOfHoles = 9;
    private const int zero = 0;

    private void Awake()
    {
        vortexCollider = GetComponent<Collider>();
        score = new int[numberOfHoles];
    }

    private void OnTriggerEnter(Collider other)
    {
        var holeNumber = 0;
        if (other.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Nombre de coups = " + _lf.GetShots());
            Debug.Log("Total = " + GetTotalScore());
            other.gameObject.SetActive(false);

            if (holeNumber > numberOfHoles) return;
            AddScore(holeNumber);
        }
        holeNumber++;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Vector3 normal = other.transform.position - vortexCollider.bounds.center;
            other.attachedRigidbody.AddForce(normal * attractionForce);
        }
    }

    private void AddScore(int holeNumber)
    {
        score[holeNumber] = _lf.GetShots();
        Debug.Log("Test = " + score[1]);
        _lf.SetShots(zero);
    }

    private int GetTotalScore()
    {
        var totalScore = 0;

        foreach (int shots in score)
        {
            totalScore += shots;
        }
        return totalScore;
    }
}
