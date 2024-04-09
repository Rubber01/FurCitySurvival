using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextPopup : MonoBehaviour
{
    private TextMeshPro textMesh;
    private Color textColor;
    [SerializeField] private float disappearTimer;
    public static TextPopup Create(Vector3 position, int damageAmount)
    {
        Debug.Log("Popup entrato ");
        Transform textPopupTransform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity);
        TextPopup textPopup = textPopupTransform.GetComponent<TextPopup>();
        textPopup.Setup(damageAmount);
        Debug.Log("Popup creato");

        return textPopup;
    }
    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }
    public void Setup(int damageAmount)
    {
        textColor = textMesh.color; 
        textMesh.SetText("-" + damageAmount.ToString());
        disappearTimer = 3f;
    }
    private void Update()
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
