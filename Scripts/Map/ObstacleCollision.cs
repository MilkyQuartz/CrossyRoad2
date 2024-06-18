using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    public GameObject collisionParticlesPrefab;
    private GameManager gameManager;
    private ParticleSystem collisionParticles;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        gameManager = GameManager.Instance;
        if (collisionParticlesPrefab != null)
        {
            collisionParticles = Instantiate(collisionParticlesPrefab, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
            collisionParticles.Stop(); 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        HandleCollision(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        HandleCollision(other.gameObject);
    }

    void HandleCollision(GameObject obj)
    {
        if (obj.CompareTag("Obstacle"))
        {
            if (collisionParticles != null && audioSource != null)
            {
                collisionParticles.transform.position = obj.transform.position;
                collisionParticles.Play();
                audioSource.Play();
            }

            StartCoroutine(PlayParticles());
        }
    }

    IEnumerator PlayParticles()
    {
        if (collisionParticles != null)
        {
            yield return new WaitForSeconds(collisionParticles.main.duration);
        }

        Destroy(gameObject);
        GameManager.Instance.isGameCheck = true;
    }
}
