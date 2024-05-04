using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MeshController : MonoBehaviour
{
    //BasicTile bt;
    GameObject catBuilding;
    GameObject dogBuilding;

    private RaidManager rm;

    private TMP_Text allyNumText;
    private TMP_Text allyCostText;
    private TMP_Text raidCostText;
    private TMP_Text coinText;



    // Start is called before the first frame update
    void Awake()
    {
        //bt = transform.parent.GetComponent<BasicTile>();
        catBuilding = this.transform.GetChild(0).gameObject;
        dogBuilding = this.transform.GetChild(1).gameObject;
        rm = transform.parent.GetComponent<RaidManager>();
        

        Transform childTransform = transform.Find("AllyNumText");
        if (childTransform != null)
        {
            allyNumText = childTransform.GetComponentInChildren<TMP_Text>();
        }
        else
        {
            Debug.Log("AllyNumText not found");
        }

        Transform childTransform2 = transform.Find("AllyCost");
        if (childTransform2 != null)
        {
            allyCostText = childTransform2.GetComponentInChildren<TMP_Text>();
        }
        else
        {
            Debug.Log("AllyCost not found");
        }

        Transform childTransform3 = transform.Find("RaidCost");
        if (childTransform3 != null)
        {
            raidCostText = childTransform3.GetComponentInChildren<TMP_Text>();
            //raidCostText = childTransform3.GetComponent<TMP_Text>();
            raidCostText.text = "" + rm.alliesRequired;
        }
        else
        {
            Debug.Log("RaidCost not found");
        }

        Transform childTransform4 = transform.Find("CoinsText");
        if (childTransform4 != null)
        {
            coinText = childTransform4.GetComponentInChildren<TMP_Text>();
        }
        else
        {
            Debug.Log("coinText not found");
        }
    }

   

    public void SwitchMesh(bool _isControlledByPlayer)
    {
        if (_isControlledByPlayer)
        {
            catBuilding.SetActive(true);
            dogBuilding.SetActive(false);

            if (raidCostText != null)
            {
                raidCostText.gameObject.SetActive(false);
            }

            if (allyNumText != null)
            {
                allyNumText.gameObject.SetActive(true);
            }
            if (allyCostText != null)
            {
                allyCostText.gameObject.SetActive(true);
            }
            if (coinText != null)
            {
                coinText.gameObject.SetActive(true);
            }

        }
        else
        {
            catBuilding.SetActive(false);
            dogBuilding.SetActive(true);

            if (raidCostText != null)
            {
                raidCostText.gameObject.SetActive(true);
            }

            if (allyNumText != null)
            {
                allyNumText.gameObject.SetActive(false);
            }
            if (allyCostText != null)
            {
                allyCostText.gameObject.SetActive(false);
            }
            if (coinText != null)
            {
                coinText.gameObject.SetActive(false);
            }
        }
    }
}
