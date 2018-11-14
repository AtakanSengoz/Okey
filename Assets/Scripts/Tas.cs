using UnityEngine;
using UnityEngine.UI;
public class Tas : MonoBehaviour
{
    public Color renk;
    public int sayi;
    public bool isSahteOkey = false;
    public bool isGosterge = false;
    public bool isOkey = false;
    Text sayiText;

    private void Start()
    {
        sayiText = GetComponentInChildren<Text>();
    }
    private void FixedUpdate()
    {
        if (isSahteOkey)
        {
            sayiText.text = "$";
        }
        else
        {
            sayiText.text = sayi.ToString();
            sayiText.color = renk;
        }
    }
}
