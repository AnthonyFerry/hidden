using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    // On récupère une référence vers le player pour pouvoir modifier sa vie et/ou son xp
    private Player _player;

    [Header("Collectible parameters")]
    /// <summary>
    /// Distance minimale à laquelle le joueur doit se trouver pour attirer la ressource.
    /// </summary>
    public float StickDistance = 2f;

    /// <summary>
    /// Vitesse du collectible.
    /// </summary>
    public float Speed = 2;

    /// <summary>
    /// "true" si le collectible est en train de suivre le joueur.
    /// </summary>
    public bool FollowsPlayer = false;
    public RessourceData Ressource;

    void Start()
    {
        var playerObject = FindObjectOfType<Player>();
        _player = playerObject.GetComponent<Player>();

        if (_player == null)
            Debug.LogError("Aucun objet de type Player se trouve dans la scène");

        SetColorToRessourceType();
    }

    // Update is called once per frame
    void Update()
    {
        var playerPosition = _player.gameObject.transform.position;
        var distanceFromPlayer = Vector3.Distance(playerPosition, transform.position);

        if (distanceFromPlayer <= StickDistance)
            FollowsPlayer = true;

        if (FollowsPlayer == true)
        {
            // On fait regarder l'objet vers le joueur
            transform.LookAt(playerPosition);

            // Et on le fait avancer
            transform.position += transform.forward * Time.deltaTime * Speed;
        }

    }

    void OnTriggerEnter(Collider playercol)
    {
        if (playercol.gameObject.name == "Player")
        {
            switch (Ressource.Type)
            {
                case TypeRessource.vie:
                    _player.Life++;
                    break;
                case TypeRessource.experience:
                    _player.XP++;
                    break;
            }

            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Attribut la bonne couleur de lumière à l'objet. Rouge si vie, vert si experience.
    /// </summary>
    void SetColorToRessourceType()
    {
        // On récupère le composant "light" de l'objet
        var light = GetComponent<Light>();

        switch (Ressource.Type)
        {
            case TypeRessource.vie:
                light.color = Color.red;
                break;
            case TypeRessource.experience:
                light.color = Color.green;
                break;
        }
    }
}

public enum TypeRessource
{
    vie,
    experience
};

[System.Serializable]
public struct RessourceData
{
    public TypeRessource Type;
    public int Quantity;
}