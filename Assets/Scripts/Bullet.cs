using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 5f;
    public float hitRange = 0.5f;
    [HideInInspector] public Transform target;

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance < hitRange)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
