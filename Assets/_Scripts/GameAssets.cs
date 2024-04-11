using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    //questo script serve per tenere conto di tutte le reference del progetto
    private static GameAssets _i;
    public static GameAssets i
    {
        get
        {
            if(_i==null) _i=Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return _i;
        }
    }
    public Transform pfDamagePopup;
    public Transform healthBar;

}
