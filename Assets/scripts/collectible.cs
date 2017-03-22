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
		if (Vector3.Distance(transform.position, _player.transform.position) <= DistanceToTarget)
            _hasTarget = true;

        if (_hasTarget)
        {
            GetComponent<Sine>().Enabled = false;

            var direction = transform.TransformDirection(_player.transform.position - transform.position);
            direction = Vector3.Normalize(direction);

            transform.position += direction * Time.deltaTime * Speed;

            Debug.DrawRay(transform.position, direction, Color.red);
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