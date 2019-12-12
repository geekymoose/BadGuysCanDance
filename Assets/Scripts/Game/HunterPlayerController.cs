using UnityEngine;
using UnityEngine.InputSystem;

public class HunterPlayerController : MonoBehaviour
{
    [Tooltip("Reload speed in seconds")]
    [SerializeField]
    private float reloadSpeedInSec = 0.5f;

    [Tooltip("Cross display")]
    [SerializeField]
    private Texture2D crossTexture;

    private float reloadAccumulatorInSec = 0.0f;


    private void Update()
    {
        this.reloadAccumulatorInSec -= Time.deltaTime;
    }

    private void Start()
    {
        // Center of the texture is where the actual mouse click is
        Vector2 offset = new Vector2(this.crossTexture.width / 2, this.crossTexture.height / 2);
        Cursor.SetCursor(this.crossTexture, offset, CursorMode.Auto);
    }

    private void OnDisable()
    {
        //Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void OnInputFire(InputAction.CallbackContext context)
    {
        this.Fire();
    }

    public bool CanFire()
    {
        return this.reloadAccumulatorInSec <= 0.0f;
    }

    private void Fire()
    {
        if (!this.CanFire())
        {
            return;
        }

        this.reloadAccumulatorInSec = this.reloadSpeedInSec;

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
