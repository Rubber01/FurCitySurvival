using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

[CustomEditor(typeof(HexGrid))]
public class HexGridEditor : Editor
{

    private GameObject tileToSnap; // Il GameObject da posizionare sulla griglia
    public bool CoordVisible = false;

    //private void SnapSeeker()

    //{
    //    HexGrid hexGrid = (HexGrid)target;

    //    Event e = Event.current;
    //    if (e.type == EventType.MouseDown && e.button == 0) // Controlla se è stato cliccato il pulsante sinistro del mouse
    //    {
    //        // Effettua un raycast per rilevare il GameObject sotto il cursore del mouse
    //        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
    //        RaycastHit hit;
    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            tileToSnap = hit.collider.gameObject;
    //        }
    //    }

    //    if (tileToSnap != null)
    //    {
    //        // Rileva la posizione corrente del GameObject
    //        Vector3 snapPosition = tileToSnap.transform.position;

    //        // Converti la posizione corrente in coordinate offset
    //        int x = Mathf.RoundToInt(snapPosition.x / (hexGrid.HexSize * 1.5f));
    //        int z = Mathf.RoundToInt(snapPosition.z / (hexGrid.HexSize * 1.73205f));

    //        // Calcola le coordinate cubiche del punto di snap
    //        Vector3 cubeCoord = HexMetrics.OffsetToCube(x, z, hexGrid.Orientation);

    //        // Calcola la posizione centrale dell'esagono corrispondente
    //        Vector3 centerPosition = HexMetrics.Center(hexGrid.HexSize, x, z, hexGrid.Orientation) + hexGrid.transform.position;

    //        // Posiziona il GameObject sulla griglia
    //        tileToSnap.transform.position = centerPosition;

    //        // Disegna una linea di debug per visualizzare il punto di snap
    //        Debug.DrawLine(snapPosition, centerPosition, Color.red);

    //        // Eventuale aggiornamento dell'interfaccia utente
    //        SceneView.RepaintAll();
    //    }
    //}
    void OnSceneGUI()
    {
        if (CoordVisible)
        {
            ShowHexCoords();
        }
        //SnapSeeker();
    }


    public void ShowHexCoords()
    {
        HexGrid hexGrid = (HexGrid)target;

        for (int z = 0; z < hexGrid.Height; z++)
        {
            for (int x = 0; x < hexGrid.Width; x++)
            {
                Vector3 centerPosition = HexMetrics.Center(hexGrid.HexSize, x, z, hexGrid.Orientation) + hexGrid.transform.position;

                int centerX = x;
                int centerZ = z;

                Vector3 cubeCoord = HexMetrics.OffsetToCube(centerX, centerZ, hexGrid.Orientation);
                Handles.Label(centerPosition + Vector3.forward * 0.5f, $"[{centerX}, {centerZ}]\n");
                Handles.Label(centerPosition, $"\n({cubeCoord.x},{cubeCoord.y},{cubeCoord.z})");
            }
        }
    }


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        HexGrid grid = (HexGrid)target;


        if (GUILayout.Button("Show/Hide Coordinates"))
        {
            CoordVisible = !(CoordVisible);
        }

        if (GUILayout.Button("Get position"))
        {
            grid.GetGridTilePositions();
        }

        

        if (GUILayout.Button("Generate Tiles"))
        {
            grid.GenerateTiles();
        }

        if (GUILayout.Button("Load Prefab Tiles"))
        {
            grid.UpdateTiles();
        }

        if (GUILayout.Button("Clear Tiles"))
        {

            grid.ClearTiles();
            
        }


    }

}