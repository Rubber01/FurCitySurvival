using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextPopup : MonoBehaviour
{
    private TextMeshPro textMesh;
    private Color textColor;
    [SerializeField] private float disappearTimer;
    private bool disappearing = false;
    public static TextPopup Create(Vector3 position, int damageAmount)
    {
        
        Transform textPopupTransform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity);
        TextPopup textPopup = textPopupTransform.GetComponent<TextPopup>();
        textPopup.SetupDamage(damageAmount);

        return textPopup;
    }
    public static TextPopup Create(Vector3 position, string damageAmount)
    {
        Vector3 temp=new Vector3(position.x, position.y+0.5f, position.z);
        Transform textPopupTransform = Instantiate(GameAssets.i.pfDamagePopup, temp, Quaternion.identity);
        TextPopup textPopup = textPopupTransform.GetComponent<TextPopup>();
        textPopup.SetupString(damageAmount);

        return textPopup;
    }
    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }
    public void SetupDamage(int damageAmount)
    {
        textColor = textMesh.color; 
        textMesh.SetText("-" + damageAmount.ToString());
        disappearTimer = 3f;
        disappearing= true;
    }
    public void SetupString(string damageAmount)
    {
        textColor = textMesh.color;
        textMesh.SetText(damageAmount);
        disappearTimer = 3f;
        disappearing = false;

    }
    private void Update()
    {
        if (disappearing ==true)
        {
            float moveYSpeed = 1f;
            transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;
            disappearTimer -= Time.deltaTime;
            if (disappearTimer < 0)
            {
                float disappearSpeed = 3f;
                textColor.a -= disappearSpeed * Time.deltaTime;
                textMesh.color = textColor;
                if (textColor.a < 0)
                {
                    Destroy(gameObject);
                }
            }
        }
        
    }
}
