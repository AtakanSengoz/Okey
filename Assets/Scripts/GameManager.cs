using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Tas okeyTasi;
    public Player[] players = new Player[4];
    int tasOlusturmaYeri = -1;
    int gostergeSayisi;
    int okeySayisi;
    Color okeyRengi;
    Tas[] okeyTaslari = new Tas[106];
    Color[] renkler = new Color[4] { Color.yellow, Color.blue, Color.black, Color.red };
    public Image[] img = new Image[4];
    void Start()
    {
        TasOlustur();
        GostergeVeOkeySec();
        TaslariKaristir();
        TaslariDagit();
        BitmeyeEnYakınEliBelirle();
    }

    void TasOlustur()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 1; j <= 13; j++)
            {
                Tas tas = new Tas
                {
                    sayi = j,
                    renk = renkler[i / 2]
                };

                tasOlusturmaYeri++;
                okeyTaslari[tasOlusturmaYeri] = tas;
            }
        }

        tasOlusturmaYeri++;
        okeyTaslari[tasOlusturmaYeri] = new Tas();
        okeyTaslari[tasOlusturmaYeri].isSahteOkey = true;

        tasOlusturmaYeri++;
        okeyTaslari[tasOlusturmaYeri] = new Tas();
        okeyTaslari[tasOlusturmaYeri].isSahteOkey = true;

    }

    void GostergeVeOkeySec()
    {
        gostergeSayisi = Random.Range(0, 103);
        okeyTaslari[gostergeSayisi].isGosterge = true;

        if (okeyTaslari[gostergeSayisi].sayi == 13)
        {
            okeySayisi = 1;
        }
        else
        {
            okeySayisi = okeyTaslari[gostergeSayisi].sayi + 1;
        }
        okeyRengi = okeyTaslari[gostergeSayisi].renk;


        foreach (Tas okey in okeyTaslari)
        {
            if (okey.sayi == okeySayisi && okey.renk == okeyRengi)
            {
                okey.isOkey = true;
            }
        }

        //sahte okeyi okey sayısı yap
        okeyTaslari[104].sayi = okeySayisi;
        okeyTaslari[104].renk = okeyRengi;
        okeyTaslari[105].sayi = okeySayisi;
        okeyTaslari[105].renk = okeyRengi;

        string tasRenk;
        if (okeyRengi == Color.yellow)
        {
            tasRenk = "Sarı";
        }
        else if (okeyRengi == Color.red)
        {
            tasRenk = "Kırmızı";
        }
        else if (okeyRengi == Color.black)
        {
            tasRenk = "Siyah";
        }
        else
        {
            tasRenk = "Mavi";
        }
        Debug.Log("Gösterge: " + tasRenk + " " + okeyTaslari[gostergeSayisi].sayi);
        Debug.Log("Okey: " + tasRenk + " " + okeySayisi);
    }

    void TaslariKaristir()
    {
        for (int i = 0; i < okeyTaslari.Length; i++)
        {
            Tas temp = okeyTaslari[i];
            int randomNumber = Random.Range(i, okeyTaslari.Length);
            okeyTaslari[i] = okeyTaslari[randomNumber];
            okeyTaslari[randomNumber] = temp;
        }
    }

    void TaslariDagit()
    {
        int fisrtPlayer = Random.Range(0, players.Length);
        players[fisrtPlayer].isFirstPlayer = true;
        players[fisrtPlayer].arraySize = 15;
        players[fisrtPlayer].AlanAyır();
        int okeyTaslariIndex = 0;
        //Eğer ilk oyuncu ise
        for (int i = 0; i < 15; i++)
        {
            players[fisrtPlayer].playerTaslari[i] = okeyTaslari[i];
            okeyTaslariIndex++;
        }
        //diğer oyuncular ise
        for (int i = 0; i < players.Length; i++)
        {
            if (i != fisrtPlayer)
            {
                players[i].AlanAyır();
                for (int j = 0; j < 14; j++)
                {
                    okeyTaslariIndex++;
                    players[i].playerTaslari[j] = okeyTaslari[okeyTaslariIndex];
                }
            }
        }
        players[0].IstakayaDiz();
        players[1].IstakayaDiz();
        players[2].IstakayaDiz();
        players[3].IstakayaDiz();
    }

    void BitmeyeEnYakınEliBelirle()
    {
        int player_01_score = players[0].IstakaPuanıBelirle();
        int player_02_score = players[1].IstakaPuanıBelirle();
        int player_03_score = players[2].IstakaPuanıBelirle();
        int player_04_score = players[3].IstakaPuanıBelirle();

        Debug.Log("Istaka puanları Player01: " + player_01_score + " Player02: " + player_02_score + "\nPlayer03: " + player_03_score + " Player04: " + player_04_score);
        if (player_01_score >= player_02_score && player_01_score >= player_03_score && player_01_score >= player_04_score)
        {
            Debug.Log("Bitmeye en yakın kişi Player01");
            img[0].color = Color.green;
        }
        else if (player_02_score >= player_01_score && player_02_score >= player_03_score && player_02_score >= player_04_score)
        {
            Debug.Log("Bitmeye en yakın kişi Player02");
            img[1].color = Color.green;
        }
        else if (player_03_score >= player_01_score && player_03_score >= player_02_score && player_03_score >= player_04_score)
        {
            Debug.Log("Bitmeye en yakın kişi Player03");
            img[2].color = Color.green;
        }
        else
        {
            Debug.Log("Bitmeye en yakın kişi Player04");
            img[3].color = Color.green;
        }

    }
}
