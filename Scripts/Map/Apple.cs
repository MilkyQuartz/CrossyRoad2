using System.Collections;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public GameObject eatParticlesPrefab;

    private ParticleSystem eatParticles;
    private AudioSource audioSource;
    private Renderer appleRenderer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        appleRenderer = GetComponent<Renderer>();
        if (appleRenderer != null)
        {
            appleRenderer.enabled = true;
        }

        if (eatParticlesPrefab != null)
        {
            GameObject particlesObject = Instantiate(eatParticlesPrefab, transform.position, Quaternion.identity, transform);
            eatParticles = particlesObject.GetComponent<ParticleSystem>();
            eatParticles.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.AddAppleCount(1); // »ç°ú ¸ÔÀº È½¼ö 1 Áõ°¡

            if (eatParticles != null && audioSource != null)
            {
                if (appleRenderer != null)
                {
                    appleRenderer.enabled = false;
                }

                eatParticles.Play();
                audioSource.Play();
            }

            StartCoroutine(DestroyAfterParticles());
        }

        if (other.CompareTag("Obstacle"))
        {
            ReturnToPool();
            ReappearRandomly();
        }
    }

    IEnumerator DestroyAfterParticles()
    {
        if (eatParticles != null)
        {
            yield return new WaitForSeconds(eatParticles.main.duration);
        }
        Destroy(gameObject);
    }

    private void ReturnToPool()
    {
        gameObject.SetActive(false);
    }

    private void ReappearRandomly()
    {
        transform.position = GetRandomPosition();
        gameObject.SetActive(true);
    }

    private Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(-10f, 10f);
        float randomZ = Random.Range(0f, 10f);
        return new Vector3(randomX, 1f, randomZ);
    }
}
