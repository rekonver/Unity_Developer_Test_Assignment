using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    [SerializeField] private Sprite[] frames;
    [SerializeField] private float framesPerSecond = 10f;

    private SpriteRenderer spriteRenderer;
    private int currentFrame;
    private float timer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (frames.Length > 0)
            spriteRenderer.sprite = frames[0];
    }

    void Update()
    {
        if (frames.Length == 0) return;

        timer += Time.deltaTime;
        if (timer >= 1f / framesPerSecond)
        {
            timer -= 1f / framesPerSecond;
            currentFrame = (currentFrame + 1) % frames.Length;
            spriteRenderer.sprite = frames[currentFrame];
        }
    }
}
