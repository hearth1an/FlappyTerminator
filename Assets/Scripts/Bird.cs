using System;
using UnityEngine;

[RequireComponent(typeof(BirdController))]
[RequireComponent(typeof(BirdCollisionHandler))]
public class Bird : MonoBehaviour
{
    private BirdController _birdMover;
    private ScoreCounter _scoreCounter;
    private BirdCollisionHandler _handler;

    public event Action GameOver;

    private void Awake()
    {
        _scoreCounter = GetComponent<ScoreCounter>();
        _handler = GetComponent<BirdCollisionHandler>();
        _birdMover = GetComponent<BirdController>();
    }

    private void OnEnable()
    {
        _handler.CollisionDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        _handler.CollisionDetected -= ProcessCollision;
    }

    private void ProcessCollision(IInteractable interactable)
    {
        if (interactable is Turret || interactable is Bullet || interactable is Ground)
        {
            GameOver?.Invoke();
        }
    }

    public void Reset()
    {        
        _birdMover.Reset();
    }
}
