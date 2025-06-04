using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : BaseControllor
{
    private new Camera camera;
    private GameManager gameManager;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
        camera = Camera.main;
    }

    public override void Death()
    {
        base.Death();
        gameManager.GameOver();
    }

    private void OnMove(InputValue inputValue)
    {
        movementDirection = inputValue.Get<Vector2>();
        movementDirection = movementDirection.normalized;
    }

    private void OnLook(InputValue inputValue)
    {
        var mousePosition = inputValue.Get<Vector2>();
        Vector2 worldPosition = camera.ScreenToWorldPoint(mousePosition);
        lookDirection = worldPosition - (Vector2)transform.position;

        if (lookDirection.magnitude < 0.9f)
            lookDirection = Vector2.zero;
        else
            lookDirection = lookDirection.normalized;
    }

    private void OnFire(InputValue inputValue)
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        isAttacking = inputValue.isPressed;
    }
}