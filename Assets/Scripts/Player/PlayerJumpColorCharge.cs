using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerJumpColorCharge : MonoBehaviour
{
    [Header("Color Settings")]
    [SerializeField] private Color startColor = Color.white;
    [SerializeField] private Color endColor = Color.yellow;

    [Header("References")]
    [SerializeField] private MonoBehaviour inputProviderComponent;
    [SerializeField] private PlayerController playerController;

    private IPlayerInput input;
    private SpriteRenderer spriteRenderer;

    private float holdTime;
    private bool returning;

    private float MaxChargeTime => playerController != null ? playerController.coyoteTime : 1f; 

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Пошук компонентів
        if (inputProviderComponent != null && inputProviderComponent is IPlayerInput)
            input = inputProviderComponent as IPlayerInput;
        else
            input = GetComponentInParent<IPlayerInput>();

        if (playerController == null)
            playerController = GetComponentInParent<PlayerController>();

        if (input == null)
            Debug.LogWarning("PlayerJumpColorCharge: input provider not found.");

        if (playerController == null)
            Debug.LogWarning("PlayerJumpColorCharge: PlayerController not found.");
    }

    void Update()
    {
        if (input == null || playerController == null)
            return;

        float maxChargeTime = MaxChargeTime;

        if (input.JumpHeld)
        {
            returning = false;
            holdTime += Time.deltaTime;
            float t = Mathf.Clamp01(holdTime / maxChargeTime);
            spriteRenderer.color = Color.Lerp(startColor, endColor, t);
        }
        else
        {
            if (!returning && holdTime > 0f)
                returning = true;

            if (returning)
            {
                holdTime -= Time.deltaTime;
                float t = Mathf.Clamp01(holdTime / maxChargeTime);
                spriteRenderer.color = Color.Lerp(startColor, endColor, t);

                if (holdTime <= 0f)
                {
                    holdTime = 0f;
                    returning = false;
                }
            }
        }
    }
}
