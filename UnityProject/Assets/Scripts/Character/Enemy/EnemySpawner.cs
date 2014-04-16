using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
	public float moveTimePeriod = 0.6f;
	public bool moveUpAndDown = true;
	public bool spawnFacingPlayer = false;
	public bool spawnOnlyOnce = false;

	public GameObject enemyPrefab;

	private GameObject enemy;
	private EnemyMovement enemyMovement;
	private Plane[] planes;
	private Camera cam;
	private bool hasSpawned = false;
	
	void Start ()
	{
		cam = Camera.main;
	}

	void Update ()
	{
		// If the camera can see enemy spawner, and enemy is dead, respawn
		if (isOnScreen() && enemy == null && !(hasSpawned && spawnOnlyOnce)) {
			SpawnEnemy();
			hasSpawned = true;
		}
	}

	private bool isOnScreen() {
		// Current camera location
		planes = GeometryUtility.CalculateFrustumPlanes(cam);
		return GeometryUtility.TestPlanesAABB (planes, gameObject.collider.bounds);
	}

	void SpawnEnemy() {
		enemy = (GameObject)Instantiate(enemyPrefab, transform.position, transform.rotation);
		enemyMovement = enemy.GetComponent<EnemyMovement>();
		enemy.transform.parent = transform;

		if (enemy != null) { 
			enemy.renderer.sortingLayerName = "Enemies";
		}

		if (enemyMovement != null) {
			enemyMovement.spawnPosistion = transform.position;
			enemyMovement.moveTimePeriod = moveTimePeriod;
			enemyMovement.moveUpAndDown = moveUpAndDown;
			enemyMovement.spawnFacingScrooge = spawnFacingPlayer;
			
			enemyMovement.Spawn();
		}
	}
}

