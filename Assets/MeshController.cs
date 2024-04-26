using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshController : MonoBehaviour
{
    //BasicTile bt;
    GameObject catBuilding;
    GameObject dogBuilding;
    
    

    // Start is called before the first frame update
    void Awake()
    {
        //bt = transform.parent.GetComponent<BasicTile>();
        catBuilding = this.transform.GetChild(0).gameObject;
        dogBuilding = this.transform.GetChild(1).gameObject;
        
    }

   

    public void SwitchMesh(bool _isControlledByPlayer)
    {
        if (_isControlledByPlayer)
        {
            catBuilding.SetActive(true);
            dogBuilding.SetActive(false);
        }
        else
        {
            catBuilding.SetActive(false);
            dogBuilding.SetActive(true);
        }
    }
}
