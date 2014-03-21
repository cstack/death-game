using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
	public float moveTimePeriod = 0.6f;
	public bool moveUpAndDown = true;
	public bool spawnFacingScrooge = false;
	public bool spawnOnlyOnStart = false;


	public GameObject enemyPrefab;
	public EnemyType enemyType;

	private GameObject enemy;
	private EnemyMovement enemyMovement;
	private Plane[] planes;
	private Camera cam;
	
	void Start ()
	{
		cam = Camera.main;
		if (spawnOnlyOnStart)
			SpawnEnemy();
	}

	void Update ()
	{
		// Current camera location
		planes = GeometryUtility.CalculateFrustumPlanes(cam);
		
		// If the camera can see enemy spawner, and enemy is dead, respawn
		if (GeometryUtility.TestPlanesAABB(planes, gameObject.collider.bounds)) {
			if (enemy == null)
				SpawnEnemy();
		}
		else if (enemy != null) {
			if (!GeometryUtility.TestPlanesAABB(planes, enemy.renderer.bounds) && !spawnOnlyOnStart)
				Destroy(enemy);
		}

	}

	void SpawnEnemy() {
		enemy = (GameObject)Instantiate(enemyPrefab, transform.position, transform.rotation);
		enemyMovement = enemy.GetComponent<EnemyMovement>();
		
		enemyMovement.spawnPosistion = transform.position;
		enemyMovement.moveTimePeriod = moveTimePeriod;
		enemyMovement.moveUpAndDown = moveUpAndDown;
		enemyMovement.spawnFacingScrooge = spawnFacingScrooge;
		enemyMovement.enemyType = enemyType;
		
		enemyMovement.Spawn();
	}
}

