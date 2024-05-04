using UnityEngine;

public class CircleParticle : MonoBehaviour
{
    public Color color = Color.white; // Default color
    public float scale = 0.4f; // Default scale
    public float speed = 6f; // Speed of upward movement
    public float targetHeight = 2f; // Target height for upward movement
    public float maxScale = 0.5f; // Maximum scale when going up
    public float minScale = 0.2f; // Minimum scale when going down
    public float scaleChangeDuration = 1f; // Duration for scaling change
    public float distanceBetweenCircles = 1f; // Distance between circles

    //public GameObject circlePrefab; // Prefab of the circle

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool movingUp = true;



    void Start()
    {
        startPosition = transform.position;
        targetPosition = new Vector3(startPosition.x, startPosition.y + targetHeight, startPosition.z);
        //SpawnCircles(5);
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

        UpdateScale();
    }

    public void SetColor(Color newColor)
    {
        color = newColor;
        GetComponent<SpriteRenderer>().color = color;
    }

    void UpdateScale()
    {
        float targetScale = movingUp ? maxScale : minScale;
        scale = Mathf.Lerp(scale, targetScale, Time.deltaTime / scaleChangeDuration);
        transform.localScale = new Vector3(scale, scale, 1f);
    }

    public void SetScale(float newScale)
    {
        scale = newScale;
        transform.localScale = new Vector3(scale, scale, 1f);
    }

    //void SpawnCircles(int count)
    //{
    //    GameObject previousCircle = null;
    //    for (int i = 0; i < count; i++)
    //    {
    //        // Calculate position based on the previous circle or starting position
    //        Vector3 position = startPosition;
    //        if (previousCircle != null)
    //        {
    //            position.x = previousCircle.transform.position.x + distanceBetweenCircles;
    //        }

    //        // Instantiate new circle GameObject
    //        GameObject newCircle = Instantiate(circlePrefab, position, Quaternion.identity);

    //        newCircle.GetComponent<SpriteRenderer>().color = color;
            

    //        // Set previous circle to the current one for the next iteration
    //        previousCircle = newCircle;
    //    }
    //}

}
