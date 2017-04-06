using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {

    public CollectibleType Type;
    public int Value;
    public float DistanceToTarget = 4;
    public float Speed = 2;

    [Header("Resources colors")]
    public Color LifeColor;
    public Color ExperienceColor;

    private GameObject _player;
    private bool _hasTarget = false;

	// Use this for initialization
	void Start () {
        _player = FindObjectOfType<PlayerResources>().gameObject;

        if (Type == CollectibleType.Life)
        {
            GetComponent<Light>().color = LifeColor;
            GetComponent<SpriteRenderer>().color = LifeColor;
        }
        else if (Type == CollectibleType.Experience)
        {
            GetComponent<Light>().color = ExperienceColor;
            GetComponent<SpriteRenderer>().color = ExperienceColor;
        }
	}
	
	// Update is called once per frame
	void Update () {
        // Si le collectible se situe à moins de DistanceToTarget, on switch le booleen hasTarget
		if (Vector3.Distance(transform.position, _player.transform.position) <= DistanceToTarget)
            _hasTarget = true;

        // Si il a trouvé sa cible
        if (_hasTarget)
        {
            // On désactive le comportement sine
            GetComponent<Sine>().Enabled = false;

            // On lui donne la direction de la cible à atteindre
            var direction = (_player.transform.position - transform.position).normalized;

            // On bouge sa position (le deltaTime est la valeur de temps qui s'est écoulé en deux appel de Update, c'est une valeur très faible et cela
            // permet de rendre la translation plus "smooth"
            transform.position += direction * Time.deltaTime * Speed;     
        }
	}

    void OnTriggerEnter(Collider obj)
    {
        PlayerResources player;

        if (player = obj.GetComponent<PlayerResources>())
        {
            if (Type == CollectibleType.Life)
            {
                player.AddLife(Value);
            }
            else if (Type == CollectibleType.Experience)
            {
                player.AddExperience(Value);
            }

            player.DisplayStats();

            Destroy(gameObject);
        }
    }
}

public enum CollectibleType
{
    Life,
    Experience
}