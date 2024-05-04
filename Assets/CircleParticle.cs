using UnityEngine;

public class CircleParticle : MonoBehaviour
{
    public Color color = Color.white; // Default color
    public float scale = 1f; // Default scale
    public float speed = 2f; // Speed of upward movement

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool movingUp = true;
    [SerializeField]
    private float targetHeight;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = new Vector3(startPosition.x, startPosition.y + targetHeight, startPosition.z);
    }

    void Update()
    {
        // If moving upwards
        if (movingUp)
        {
            // Move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            // If reached target position
            if (transform.position == targetPosition)
            {
                movingUp = false; // Change direction
            }
        }
        else
        {
            // Move towards the starting position
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
            // If reached starting position
            if (transform.position == startPosition)
            {
                movingUp = true; // Change direction
            }
        }
    }

    public void SetColor(Color newColor)
    {
        color = newColor;
        GetComponent<SpriteRenderer>().color = color;
    }

    public void SetScale(float newScale)
    {
        scale = newScale;
        transform.localScale = new Vector3(scale, scale, 1f);
    }
}
