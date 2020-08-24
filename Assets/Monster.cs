using UnityEngine;

public class Monster : MonoBehaviour
{

    [SerializeField] private GameObject _cloudParticiplePrefab;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bird bird = collision.collider.GetComponent<Bird>();
        if (bird != null)
        {
            Instantiate(_cloudParticiplePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }

        Monster monster = collision.collider.GetComponent<Monster>();
        if(monster != null)
        {
            return;
        }

        bool collisionOnTop = collision.contacts[0].normal.y < -0.5;
        if (collisionOnTop)
        {
            Instantiate(_cloudParticiplePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
}
