
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using com.cyborgAssets.inspectorButtonPro;
using Unity.VisualScripting;
using AYellowpaper.SerializedCollections;
using System;
using UnityEngine.Tilemaps;



public class HexGrid : MonoSingleton<HexGrid>
{

    [field: SerializeField] public int Width { get; private set; }
    [field: SerializeField] public int Height { get; private set; }
    [field: SerializeField] public float HexSize { get; private set; }
    [field: SerializeField] public GameObject HexPrefab { get; private set; }
    [field: SerializeField] public HexOrientation Orientation { get; private set; }

    //public List<Vector3> HexPositions = new List<Vector3>();
    [SerializedDictionary("Grid Coord", "World Origin")]
    public SerializedDictionary<Vector2Int, Vector3> HexPositions; // = new Dictionary<Vector2Int, Vector3>();

    public TileDictionary hexData = new TileDictionary();



    private void OnDrawGizmos()
    {
        DrawHexagons();
    }


    public void DrawHexagons()
    {
        for (int z = 0; z < Height; z++)
        {
            for (int x = 0; x < Width; x++)
            {
                Vector3 centerPosition = HexMetrics.Center(HexSize, x, z, Orientation) + transform.position;
                for (int s = 0; s < HexMetrics.Corners(HexSize, Orientation).Length; s++)
                {
                    Gizmos.DrawLine(
                        centerPosition + HexMetrics.Corners(HexSize, Orientation)[s % 6],
                        centerPosition + HexMetrics.Corners(HexSize, Orientation)[(s + 1) % 6]);

                }
            }
        }
    }


   
    public void GetGridTilePositions()
    {
        HexPositions.Clear();

        //List<Vector3> hexPositions = HexPositions;

        for (int z = 0; z < Height; z++)
        {
            for (int x = 0; x < Width; x++)
            {
                Vector3 centerPosition = HexMetrics.Center(HexSize, x, z, Orientation) + transform.position;
                Vector2Int tileKey = new Vector2Int(x, z);
                HexPositions[tileKey] = centerPosition;
                //hexPositions.Add(centerPosition);
            }
        }
        //HexPositions = hexPositions;
        Debug.Log("#HexPositions: " + HexPositions.Count);
        //Debug.Log(HexPositions.ToString());
    }



    

    [ExecuteInEditMode]
    public void GenerateTiles()
    {
        //if (HexPositions.Count != 0 && HexPrefab != null)
        //{
            //// Clear existing tiles if any
            //ClearTiles();

            //foreach (var tilePosition in HexPositions)
            //{

            //    Vector2Int tileKey = tilePosition.Key;
            //    Vector3 position = tilePosition.Value;

            //    GameObject instantiatedTile = Instantiate(HexPrefab, position, Quaternion.identity);
            //    // Parent the instantiated tile under the HexGrid GameObject
            //    instantiatedTile.transform.parent = transform;
            //    instantiatedTile.transform.Rotate(Vector3.right, -90f);
            //    instantiatedTile.name = "Tile_" + tileKey.x + "_" + tileKey.y;
            //}

            if (HexPositions.Count != 0 && HexPrefab != null)
            {
                ClearTiles();

                foreach (var tilePosition in HexPositions)
                {
                    Vector2Int tileKey = tilePosition.Key;
                    Vector3 position = tilePosition.Value;

                    GameObject instantiatedTile = Instantiate(HexPrefab, position, Quaternion.identity);
                    instantiatedTile.transform.parent = transform;
                    instantiatedTile.transform.Rotate(Vector3.right, -90f);

                    string tileName = "Tile_" + tileKey.x + "_" + tileKey.y;

                    instantiatedTile.name = tileName;

                    hexData.Add(tileKey, new TileData(instantiatedTile, position, tileName,null));
                
                //Debug.Log(instantiatedTile.GetComponent<BasicTile>().name);
                instantiatedTile.GetComponent<BasicTile>().tileCoords = tileKey;
                instantiatedTile.GetComponent<BasicTile>().tilePosition = position;

                }
            }
        //}
    }

    [ExecuteInEditMode]
    public void ClearTiles()
    {
        // Destroy all child objects under HexGrid
        foreach (var tiles in hexData)
        {
            TileData tileData = tiles.Value;
            GameObject tileGameObject = tileData.instantiatedTile;
            GameObject tilePrefab = tileData.prefabType;
            DestroyImmediate(tilePrefab);
            DestroyImmediate(tileGameObject);
                       
        }
        hexData.Clear();
    }

    [ExecuteInEditMode]
    public void UpdateTiles()
    {
        if (HexPositions.Count != 0 && HexPrefab != null)
        {
            foreach (var tilePosition in HexPositions)
            {
                UpdateTile(tilePosition);
            }
        }
    }

    public void UpdateTile(KeyValuePair<Vector2Int, Vector3> tilePosition)
    {
        Vector2Int tileKey = tilePosition.Key;
        TileData tileData;

        if (hexData.TryGetValue(tileKey, out tileData))
        {
            Vector3 position = tileData.position;
            GameObject PrefabToLoad = tileData.prefabType;
            GameObject originalTile = tileData.instantiatedTile;

            if (PrefabToLoad != null)
            {


                string name = originalTile.name;

                GameObject instantiatedPrefab = Instantiate(PrefabToLoad, position, Quaternion.identity);
                instantiatedPrefab.transform.parent = tileData.instantiatedTile.transform;
                instantiatedPrefab.transform.Rotate(Vector3.right, -90f);
                instantiatedPrefab.name = name;
                tileData.instantiatedTile = instantiatedPrefab;
                instantiatedPrefab.transform.SetParent(transform);

                DestroyImmediate(originalTile);
                //hexData.Add(tileKey, tileData);
                hexData[tileKey] = tileData;


                //tileData.instantiatedTile = instantiatedPrefab; // Salva l'istanza nella classe TileData
            }
        }
    }
    [ExecuteInEditMode]
    public void ChangeSpecificTile(BasicTile _basicTile, GameObject _tilePrefab)
    {
        //Debug.Log("ChangeSpecificTile: Tile Position"+ _basicTile.TileCoords);
        //TileData tileData;

        //foreach (var tileposition in HexPositions)
        //{
        //    if (tileposition.Key == _basicTile.TileCoords)
        //    {
        //        if (hexData.TryGetValue(tileposition.Key, out tileData))
        //        {
        //            Vector3 position = tileData.position;
        //            GameObject PrefabToLoad = _tilePrefab;
        //            GameObject originalTile = tileData.instantiatedTile;

        //            if (PrefabToLoad != null)
        //            {

        //                string name = originalTile.name;

        //                GameObject instantiatedPrefab = Instantiate(PrefabToLoad, position, Quaternion.identity);
        //                instantiatedPrefab.transform.parent = tileData.instantiatedTile.transform;
        //                instantiatedPrefab.transform.Rotate(Vector3.right, -90f);
        //                instantiatedPrefab.name = name;
        //                tileData.instantiatedTile = instantiatedPrefab;
        //                instantiatedPrefab.transform.SetParent(transform);
        //                //#if UNITY_EDITOR
        //                //                        DestroyImmediate(originalTile);
        //                //#endif
        //                if (originalTile != null) Destroy(originalTile);

        //                //hexData.Add(tileKey, tileData);
        //                hexData[tileposition.Key] = tileData;

        //                DestroyImmediate(originalTile);
        //                Debug.Log(originalTile.name);

        //                //tileData.instantiatedTile = instantiatedPrefab; // Salva l'istanza nella classe TileData
        //            }
        //        }

        //    }
        //}
        Debug.Log("ChangeSpecificTile: Tile Position" + _basicTile.TileCoords);
        TileData tileData;

        // Cerca tra i tiles memorizzati nella mappa
        if (hexData.TryGetValue(_basicTile.TileCoords, out tileData))
        {
            Vector3 position = tileData.position;
            GameObject instantiatedPrefab = Instantiate(_tilePrefab, position, Quaternion.identity);
            instantiatedPrefab.name = tileData.name;
            instantiatedPrefab.transform.SetParent(transform);
            instantiatedPrefab.transform.Rotate(Vector3.right, -90f);

            //Da aggiungere le coordinate della posizione

            // Ottieni tutti i componenti TriggerTileUnlocker presenti in _basicTile e nei suoi figli
            TriggerTileUnlocker[] triggerTileUnlockers = _basicTile.GetComponentsInChildren<TriggerTileUnlocker>();

            // Se l'array triggerTileUnlockers non è vuoto, impara i suoi trasform come figli di instantiatedPrefab
            if (triggerTileUnlockers != null && triggerTileUnlockers.Length > 0)
            {
                foreach (TriggerTileUnlocker triggerTileUnlocker in triggerTileUnlockers)
                {
                    // Imparenta triggerTileUnlocker con l'instantiatedPrefab
                    triggerTileUnlocker.transform.SetParent(instantiatedPrefab.transform);
                }
            }
            else
            {
                Debug.Log("No TriggerTileUnlocker components found in _basicTile.");
            }


            // Se il tile originale esiste, lo distruggi
            if (tileData != null)
            {
                Debug.Log("TileDestroyed?");

                
#if UNITY_EDITOR
                if (!Application.isPlaying)
            { 
                    Debug.Log("UnityEDITOR!!");
                DestroyImmediate(_basicTile.gameObject);
            }
                else
                {
                    //_basicTile.gameObject.SetActive(false);
                    Destroy(_basicTile.gameObject);
                }
#else
                Debug.Log("NOT IN EDITOR!!");
                //_basicTile.gameObject.SetActive(false);
                Destroy(_basicTile.gameObject);
#endif

            }

            // Aggiorna le informazioni del tile nella mappa
            tileData.instantiatedTile = instantiatedPrefab;
            tileData.prefabType = _tilePrefab;
            hexData[_basicTile.TileCoords] = tileData;

            // Opzionalmente, aggiorna le proprietà del tile
            instantiatedPrefab.GetComponent<BasicTile>().tileCoords = _basicTile.TileCoords;
            instantiatedPrefab.GetComponent<BasicTile>().tilePosition = position;

            Debug.Log("Tile changed successfully.");
        }
        else
        {
            Debug.LogWarning("Tile not found at position: " + _basicTile.TileCoords);
        }
    }


}



public enum HexOrientation
{
    FlatTop,
    PointyTop
}

[Serializable]
public class TileData
{
    public GameObject instantiatedTile;
    public Vector3 position;
    [HideInInspector]
    public string name;
    public GameObject prefabType;

    
    public TileData(GameObject tile, Vector3 pos, string n, GameObject prefabType)
    {
        instantiatedTile = tile;
        position = pos;
        name = n;
        this.prefabType = prefabType;
    }
}

[Serializable]
public class TileDictionary : SerializedDictionary<Vector2Int, TileData> { }