using System.Numerics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    private PlayerInput input;
    private Rigidbody2D rb;

    private void Start()
    {
        input = PlayerInput.Instance;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(input.GetMoveInput() * moveSpeed, ForceMode2D.Force);
    }

    private void Update()
    {
        
    }
}
