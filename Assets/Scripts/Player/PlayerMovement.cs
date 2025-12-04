using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    private PlayerInput input;
    private Rigidbody2D rb;

    private Animator animator;
    private Vector2 move;
    private Player player;

    private void Start()
    {
        input = PlayerInput.Instance;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(move * moveSpeed, ForceMode2D.Force);
    }

    private void Update()
    {
        if (PauseMenuManager.Instance.isPaused)
        {
            player.seedSelector.CloseMenu();
            return;
        }
        if (player.isInventoryOpen || player.seedSelector.isOpen)
        {
            move = Vector2.zero;
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", 0);
            animator.SetBool("IsMoving", false);
            return;
        }
        move = input.GetMoveInput().normalized;

        animator.SetFloat("MoveX", move.x);
        animator.SetFloat("MoveY", move.y);
        animator.SetBool("IsMoving", move.sqrMagnitude > 0.01f);
    }
}
