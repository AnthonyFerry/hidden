using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectible : MonoBehaviour {

    //Variables (privé par défaut, si public, doit être déclaré)7
    //Appelle player parce que besoin d'influer sur la vie du joueur dans la classe Player
    public Player player; //Pour la rendre accessible dans l'éditeur, et en glissant le script associé, crée une référence

    public float StickDistance = 2f;

    public float Speed = 2;

    public bool SuitJoueur = false;

    public TypeRessource type;


	// Use this for initialization
	void Start () {

        if (player == null)
        {
            Debug.LogError("Oubli de script");
        }
		
	}
	
	// Update is called once per frame
	void Update () {
        var playerpos = player.gameObject.transform.position;
        
        if (Vector3.Distance(playerpos, transform.position) <= StickDistance)
        {
            SuitJoueur = true;
        }

        // ou SuitJoueur = Vector3.Distance(playerpos, transform.position) <= StickDistance

        if (SuitJoueur == true)
        {
            transform.LookAt(playerpos);
            transform.position += transform.forward * Time.deltaTime * Speed;
        }
        
	}

    void OnTriggerEnter (Collider playercol)
    {
        if (playercol.gameObject.name == "player")
        {
            switch (type)
            {
                case TypeRessource.vie:
                    player.Life++;
                    break;
                case TypeRessource.experience:
                    player.XP++;
                    break;
            }

            Destroy(gameObject);
        }
    }

    
}

public enum TypeRessource
{
    vie,
    experience
};