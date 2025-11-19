using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform target;
    public Vector2 offset;

    private void Update()
    {
        Vector3 targetPos = new Vector3(target.position.x + offset.x, target.position.y + offset.y, -10f);
        transform.position = Vector3.Lerp(transform.position, targetPos, 3f * Time.deltaTime);
    }
}
