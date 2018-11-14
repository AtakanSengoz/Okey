using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Tas tas;
    [HideInInspector]
    public int arraySize = 14;
    [HideInInspector]
    public Tas[] playerTaslari;
    public int istakaPuanı = 0;
    public bool isFirstPlayer = false;

    public void AlanAyır()
    {
        playerTaslari = new Tas[arraySize];
    }

    public void IstakayaDiz()
    {
        Array.Sort(playerTaslari, delegate (Tas tas1, Tas tas2)
        {
            return tas1.sayi.CompareTo(tas2.sayi);
        });

        for (int i = 0; i < playerTaslari.Length; i++)
        {
            Tas tas_m = Instantiate(tas, transform.position, Quaternion.identity) as Tas;
            tas_m.transform.SetParent(gameObject.transform);

            tas_m.sayi = playerTaslari[i].sayi;
            tas_m.renk = playerTaslari[i].renk;
            tas_m.isGosterge = playerTaslari[i].isGosterge;
            tas_m.isOkey = playerTaslari[i].isOkey;
            tas_m.isSahteOkey = playerTaslari[i].isSahteOkey;
        }
    }

    public int IstakaPuanıBelirle()
    {
        for (int i = 0; i < playerTaslari.Length - 1; i++)
        {
            if (playerTaslari[i].renk == playerTaslari[i + 1].renk &&
                playerTaslari[i].sayi + 1 == playerTaslari[i + 1].sayi)
            {
                istakaPuanı++;
            }
            else if (playerTaslari[i].renk != playerTaslari[i + 1].renk &&
                    playerTaslari[i].sayi == playerTaslari[i + 1].sayi)
            {
                istakaPuanı++;
            }

            if (playerTaslari[i].isOkey == true)
            {
                istakaPuanı += 2;
            }
        }

        return istakaPuanı;
    }
}
