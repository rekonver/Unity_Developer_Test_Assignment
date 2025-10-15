using UnityEngine;
using System;

[RequireComponent(typeof(Collider2D))]
public class Coin : MonoBehaviour, ICollectible
{
    public static event Action<Coin> OnCollected;

    [SerializeField] private LayerMask playerLayer; // 👈 шар гравця
    [SerializeField] private AudioClip collectSound;
    [SerializeField] private float rotationSpeed = 180f;

    private bool collected = false;

    void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }

    public void Collect(GameObject collector)
    {
        if (collected) return;
        collected = true;

        OnCollected?.Invoke(this);

        if (collectSound)
            AudioSource.PlayClipAtPoint(collectSound, transform.position);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            Collect(other.gameObject);
        }
    }
}
