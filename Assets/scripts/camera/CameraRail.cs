using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ce composent va permettre à la caméra de savoir si un obstacle se situe entre le joueur et elle-même.
/// Si c'est le cas, la camera va se rapprocher du joueur.
/// </summary>
public class CameraRail : MonoBehaviour {

    // Position de la camera principale
    private Transform _camera;
    // Position du joueur
    private Transform _player;

	// Use this for initialization
	void Start () {
        _camera = Camera.main.transform;
        _player = FindObjectOfType<PlayerMovements>().gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {

        var position = transform.position;
        var playerPosition = _player.position;
        // On calcule la distance de l'objet de reference de la camera par rapport au joueur.
        // Cela servira pour déterminer la longueur du Raycast
        float distanceFromPlayer = Vector3.Distance(playerPosition, position);

        // On envoi un rayon du joueur vers la camera. La variable hits stockera tout les points de collision rencontrés par ce rayon.
        var hits = Physics.RaycastAll(playerPosition, position - playerPosition, distanceFromPlayer);

        // Si il y a au moins une collision
        if (hits.Length > 0)
        {
            // On récupère le point le plus proche du joueur grâce à la FoundTheNearestPoint.
            Vector3 nearestPoint = FoundTheNearestPoint(hits, _player.position);

            // On lerp la position de la camera vers ce nouveau point
            _camera.position = nearestPoint;
        }
        else
        {
            // Sinon on lerp la position de la camera vers le point de référence de la camera
            _camera.position = Vector3.Lerp(_camera.position, transform.position, Time.deltaTime * 5);
        }

    }

    Vector3 FoundTheNearestPoint(RaycastHit[] hits, Vector3 position)
    {
        Vector3 nearestPoint = hits[0].point ;
        foreach (var hit in hits) {
            var distanceFromPlayer = Vector3.Distance(hit.point, position);

            if (distanceFromPlayer < Vector3.Distance(nearestPoint, position))
                nearestPoint = hit.point;
        }
        return nearestPoint;
    }

    Vector3 FoundTheNearestPoint(Vector3[] points, Vector3 position)
    {
        Vector3 nearestPoint = points[0];
        foreach (var point in points)
        {
            var distanceFromPlayer = Vector3.Distance(point, position);

            if (distanceFromPlayer < Vector3.Distance(nearestPoint, position))
                nearestPoint = point;
        }
        return nearestPoint;
    }
}
