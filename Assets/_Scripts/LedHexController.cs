using System.Collections.Generic;
using UnityEngine;

public class LedHexController : MonoBehaviour
{
    public Material materialGreen;
    public Material materialRed;
    private bool isControlledByPlayer;
    private bool previousControlledByPlayer;
    private BasicTile tile;
    private List<MeshRenderer> meshRenderers = new List<MeshRenderer>();

    void Start()
    {
        tile = GetComponentInParent<BasicTile>();
        isControlledByPlayer = tile.isControlledByPlayer;
        previousControlledByPlayer = isControlledByPlayer;

        foreach (Transform child in transform)
        {
            MeshRenderer renderer = child.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                meshRenderers.Add(renderer);
            }
            if (isControlledByPlayer)
            {
                renderer.material = materialGreen;
            }
            else
            {
                renderer.material = materialRed;
            }

        }


    }

    void Update()
    {
        isControlledByPlayer = tile.isControlledByPlayer;

        // Verifica se lo stato di controllo del giocatore è cambiato
        if (isControlledByPlayer != previousControlledByPlayer)
        {
            previousControlledByPlayer = isControlledByPlayer;

            foreach (MeshRenderer renderer in meshRenderers)
            {
                if (isControlledByPlayer)
                {
                    renderer.material = materialGreen;
                }
                else
                {
                    renderer.material = materialRed;
                }
            }
        }
    }
}
