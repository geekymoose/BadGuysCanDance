using UnityEngine;
using UnityEngine.InputSystem;

public class HunterPlayerController : MonoBehaviour
{
    public void OnInputFire(InputAction.CallbackContext context)
    {
        /*
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        Debug.DrawRay(ray.origin, ray.direction * 15, Color.red, 0.5f);
        */

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 mouseWorldPos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

        RaycastHit2D hitInfo = Physics2D.Raycast(mouseWorldPos2D, Vector2.zero);

        Debug.Log("Fire");
        if (hitInfo.collider != null)
        {
            if(hitInfo.transform.gameObject.GetComponentInParent<SnapGridCharacter>() != null)
            {
                Debug.Log("Hit player");
                Destroy(hitInfo.transform.gameObject.GetComponentInParent<SnapGridCharacter>().gameObject);
            }
        }
    }
}
