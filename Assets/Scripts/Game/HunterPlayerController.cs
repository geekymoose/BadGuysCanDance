using UnityEngine;
using UnityEngine.InputSystem;

public class HunterPlayerController : MonoBehaviour
{
    public void OnInputFire(InputAction.CallbackContext context)
    {
        AkSoundEngine.PostEvent("Play_SFX_Laser_Shoot", gameObject);

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 mouseWorldPos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

        RaycastHit2D hitInfo = Physics2D.Raycast(mouseWorldPos2D, Vector2.zero);

        if (hitInfo.collider != null)
        {
            AkSoundEngine.PostEvent("Play_SFX_Laser_Hit", gameObject);

            Character character = hitInfo.transform.gameObject.GetComponentInParent<Character>();
            if (character != null)
            {
                character.Kill();
            }
        }
    }
}
