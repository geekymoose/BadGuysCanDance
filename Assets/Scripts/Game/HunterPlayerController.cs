using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Assertions;

public class HunterPlayerController : MonoBehaviour
{
    [Tooltip("Reload speed in seconds")]
    [SerializeField]
    private float reloadSpeedInSec = 0.5f;

    [Tooltip("Cross display")]
    [SerializeField]
    private Texture2D crossTexture;

    [SerializeField]
    private LineRenderer laserLine;

    [SerializeField]
    private Transform[] listShootBarrelPoints;

    [SerializeField]
    private Animation laserAnimation;

    private float reloadAccumulatorInSec = 0.0f;


    private void Update()
    {
        this.reloadAccumulatorInSec -= Time.deltaTime;
    }

    private void Start()
    {
        Assert.IsNotNull(this.crossTexture, "Missing asset");
        Assert.IsNotNull(this.laserLine, "Missing asset");
        Assert.IsNotNull(this.listShootBarrelPoints, "Missing asset");
        Assert.IsNotNull(this.laserAnimation, "Missing asset");

        this.laserLine.useWorldSpace = true;

        // Center of the texture is where the actual mouse click is
        Vector2 offset = new Vector2(this.crossTexture.width / 2, this.crossTexture.height / 2);
        Cursor.SetCursor(this.crossTexture, offset, CursorMode.Auto);
    }

    public void OnInputFire(InputAction.CallbackContext context)
    {
        if(context.ReadValue<float>() != 0.0f)
        {
            // Value == 1 means button pressed (0 means released)
            this.Fire();
        }
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

        Physics2D.queriesStartInColliders = true;
        RaycastHit2D hitInfo = Physics2D.Raycast(mouseWorldPos2D, Vector2.zero);

        // Laser
        float randomValue = Random.Range(0, 10000);
        int randomBarrelIndex = (int)(randomValue % this.listShootBarrelPoints.Length);
        this.laserLine.SetPosition(0, this.listShootBarrelPoints[randomBarrelIndex].transform.position);
        this.laserLine.SetPosition(1, new Vector3(mouseWorldPos.x, mouseWorldPos.y, this.transform.position.z));
        this.laserAnimation.Play();

        if (hitInfo.collider != null)
        {
            AkSoundEngine.PostEvent("Play_SFX_Laser_Hit", gameObject);

            Character character = hitInfo.transform.gameObject.GetComponentInParent<Character>();
            if (character != null && character.isActiveAndEnabled)
            {
                character.Kill();
            }
        }
    }
}
