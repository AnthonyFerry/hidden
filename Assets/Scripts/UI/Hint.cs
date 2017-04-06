using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{
    public static int A_BUTTON = 0;
    public static int B_BUTTON = 1;
    public static int X_BUTTON = 2;
    public static int Y_BUTTON = 3;
    public static int START_BUTTON = 4;
    public static int MENU_BUTTON = 5;


    public List<Sprite> Icons;
    public GameObject goText;
    public GameObject goImage;

    private bool _isShowing = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Affiche un indice en jeu en bas de l'écran. Entrer le caractère $ pour indiquer la position d'une icône.
    /// </summary>
    /// <param name="hintString">La chaine de caractère à afficher</param>
    /// <param name="iconsIndexes">Index des icônes dans leur ordre d'apparition</param>
    public void Show(string hintString, params int[] iconsIndexes)
    {
        if (_isShowing)
            return;

        _isShowing = true;

        // On commence par vérifier si le nombre de $ dans la chaine correspond au nombre de paramètre iconsIndexes
        int iconsNumber = 0;
        for (int i = 0; i < hintString.Length; i++)
            if (hintString[i] == '$') iconsNumber++;

        // Si c'est pas le cas on renvoi une erreur
        if (iconsNumber != iconsIndexes.Length)
            return;

        string[] strings = hintString.Split('$');
        string res = "";



        for (int i = 0; i < strings.Length; i++)
        {
            CreateTextObject(strings[i]);

            if (i < strings.Length - 1)
                CreateImageObject(iconsIndexes[i]);
        }
    }

    public void Hide()
    {
        _isShowing = false;

        for (int i = 0; i < transform.childCount; i++)
            Destroy(transform.GetChild(i).gameObject);
    }

    void CreateTextObject(string text)
    {
        var goTxt = Instantiate(goText, transform) as GameObject;
        goTxt.GetComponent<Text>().text = text;
    }

    void CreateImageObject(int spriteIndex)
    {
        var goImg = Instantiate(goImage, transform) as GameObject;
        goImg.GetComponent<Image>().sprite = Icons[spriteIndex];
    }
}