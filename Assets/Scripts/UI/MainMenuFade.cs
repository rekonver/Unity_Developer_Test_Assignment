using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class MainMenuFade : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1f;
    private CanvasGroup canvasGroup;
    private float timer;
    private bool fadingIn;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        fadingIn = true;
        timer = 0f;
    }

    void Update()
    {
        if (fadingIn)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(timer / fadeDuration);

            if (canvasGroup.alpha >= 1f)
            {
                fadingIn = false;
            }
        }
    }
}
