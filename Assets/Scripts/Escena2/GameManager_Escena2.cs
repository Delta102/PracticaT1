using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager_Escena2 : MonoBehaviour
{
    public TMP_Text puntajeTexto;
    public TMP_Text tipo1Text;
    public TMP_Text tipo2Text;

    public int tipo1;
    public int tipo2;
    public int puntaje;
    //public Vector3 lposicion;

    void Start()
    {
        puntaje=0;
        tipo1=0;
        tipo2=0;
        //lposicion=new Vector3(0,0,0);
        LoadGame();
        PrintPuntajeInScreen();
        Printt1InScreen();
        Printt2InScreen();
    }

    /*public Vector3 Lposicion(){
        return lposicion;
    }*/

    public int Tipo1()
    {
        return tipo1;
    }

    public int Tipo2()
    {
        return tipo2;
    }

    public int Puntaje()
    {
        return puntaje;
    }

    /*public void guardarPosicion(Vector3 lastPosition){
        lposicion=lastPosition;
    }*/
    public void ganarPuntaje(int a){
        puntaje+=a;
        PrintPuntajeInScreen();
    }

    public void ganarMonedaTipo1(int _tipo1)
    {
        tipo1 += _tipo1;
        Printt1InScreen();
    }

    public void ganarMonedaTipo2(int _tipo2)
    {
        tipo2 += _tipo2;
        Printt2InScreen();
    }

    public void SaveGame(){
        var filePath=Path.Combine(Application.persistentDataPath, "gameData.dat");
        FileStream file;
        if(File.Exists(filePath))
            file=File.OpenWrite(filePath);
        else
            file=File.Create(filePath);
        
        GameData data=new GameData();
        data.Score=puntaje;
        //data.posicion=lposicion;
        data.tipo1=tipo1;
        data.tipo2=tipo2;

        BinaryFormatter bf=new BinaryFormatter();
        bf.Serialize(file,data);
        file.Close();
    }

    public void LoadGame(){
        var filePath=Path.Combine(Application.persistentDataPath, "gameData.dat");
        FileStream file;
        if(File.Exists(filePath))
            file=File.OpenRead(filePath);
        else{
            Debug.LogError("No se encontró el archivo");
            return;
        }

        BinaryFormatter bf=new BinaryFormatter();
        GameData data=(GameData) bf.Deserialize(file);
        file.Close();

        puntaje=data.Score;
        tipo1=data.tipo1;
        tipo2=data.tipo2;
        PrintPuntajeInScreen();
    }

    private void PrintPuntajeInScreen()
    {
        puntajeTexto.text = "Puntaje: " + puntaje;
    }
    private void Printt1InScreen()
    {
        tipo1Text.text = "Moneda Tipo1: " + tipo1;
    }

    private void Printt2InScreen()
    {
        tipo2Text.text = "Moneda Tipo2: " + tipo2;
    }
}
