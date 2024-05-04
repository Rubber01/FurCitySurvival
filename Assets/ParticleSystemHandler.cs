using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public string playerTag = "Player"; // Il tag del player
    public Transform mainCamera; // Il Transform della telecamera principale
    public float distanceFromCamera = 2f; // La distanza desiderata dall'oggetto alla telecamera

    private Transform playerTransform; // Il Transform del player
    private float offsetY = 1.7f;

    private void Start()
    {
        // Cerca il player utilizzando il tag specificato
        GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("Player non trovato con il tag specificato: " + playerTag);
        }
    }

    private void Update()
    {
        // Verifica che il player e la telecamera siano assegnati
        if (playerTransform == null || mainCamera == null)
        {
            Debug.LogWarning("Assicurati di assegnare il player e la telecamera nel componente FollowPlayer.");
            return;
        }

        // Calcola la direzione dal player alla telecamera
        Vector3 directionToCamera = mainCamera.position - playerTransform.position;
        
        // Normalizza la direzione e la moltiplica per la distanza desiderata
        Vector3 targetPosition = playerTransform.position + directionToCamera.normalized * distanceFromCamera;

        targetPosition += new Vector3(0f, offsetY, 0f);
        // Aggiorna la posizione dell'oggetto per farlo seguire il player
        transform.position = targetPosition;
    }
}
