using Unity.Burst.CompilerServices;
using UnityEngine;

public class EnemyObject : MonoBehaviour
{
    public enum Behaviour
    {   
        None = 0,
        Collide = 1, // always move towards player
        Strafe = 2, // move to distance and strafe player
        Stand = 3, // move to set distance and stay there, move closer if the player moves away
        KeepAway = 4, // move to a distance and try to keep that distance
        KeepAwayStrafe = 5, // move to a distance and try to keep that distance while strafing the player
    }
    private GameObject player;
    [SerializeField] private BaseEnemy enemy;
    [SerializeField] private Behaviour behaviour;
    [SerializeField] private float chaseToDistance;
    [SerializeField] private float speed;
    [SerializeField] private int scoreValue;
    [SerializeField] private AudioClip deathSound;
    private float currentDistance;
    private Vector2 playerDirection;
    public float distanceToOtherEnemy;
    private bool scored;

    private float currentSpeed;
    public bool tooClose = false;

    private void Start()
    {

        player = FindFirstObjectByType<PlayerStats>().gameObject;
        currentSpeed = speed;
        ChosenAI(behaviour);
    }

    void Update()
    {
        currentDistance = Vector2.Distance(transform.position, player.transform.position);
        playerDirection = player.transform.position - transform.position;

        ChosenAI(behaviour);
        if (enemy.dead)
        {
            if (!scored)
            {
                SpawnManager.SubtractFromCount();
                LevelManager.IncreaseScore(scoreValue);
                scored = true;
                SFXManager.Instance.PlaySoundFXClip(deathSound,gameObject.transform,1f);
            }
            enemy.fadeLevel -= 2*Time.deltaTime;
            Destroy(gameObject, 0.5f);

        }    
    }

    private void ChosenAI(Behaviour behaviour)
    {
        
        
            if (behaviour.Equals(Behaviour.None))
            {

            }
            else if (behaviour.Equals(Behaviour.Collide))
            {
                CollideAI();
            }
            else if (behaviour.Equals(Behaviour.Strafe))
            {
                StrafeAI();
            }
            else if (behaviour.Equals(Behaviour.Stand))
            {
                StandAI();
            }
            else if (behaviour.Equals(Behaviour.KeepAway))
            {
                KeepAwayAI();
            }
            else if (behaviour.Equals(Behaviour.KeepAwayStrafe))
            {
                KeepAwayStrafeAI();
            }
        
    }
    private void CollideAI()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, currentSpeed * Time.deltaTime);
    }
    private void StrafeAI()
    {
        Vector3 strafeVector = new Vector3(playerDirection.y, -playerDirection.x, 0);
        Vector3 strafeDirection = transform.position + strafeVector;
        
        if (chaseToDistance < currentDistance)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position+ 0.1f*strafeDirection, currentSpeed * Time.deltaTime);
        }
        if ( chaseToDistance >= currentDistance)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, strafeDirection, currentSpeed * Time.deltaTime);
        }
    }
    private void StandAI()
    {
        if (chaseToDistance < currentDistance)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, currentSpeed * Time.deltaTime);
        }
    }
    private void KeepAwayAI()
    {
        Vector3 away = this.transform.position + (Vector3)(-playerDirection);
        if (chaseToDistance < currentDistance)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, currentSpeed * Time.deltaTime);
        }
        else if (chaseToDistance - 1 > currentDistance)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, away, (currentSpeed/2) * Time.deltaTime);
        }
    }
    private void KeepAwayStrafeAI()
    {
        Vector3 strafeVector = new Vector3(playerDirection.y, -playerDirection.x, 0);
        Vector3 strafeDirection = transform.position + strafeVector;
        Vector3 away = this.transform.position + (Vector3)(-playerDirection);
        if (chaseToDistance < currentDistance)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position + 0.1f * strafeDirection, currentSpeed * Time.deltaTime);
        }
        else if (chaseToDistance - 1 > currentDistance)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, away + 0.1f * strafeDirection, (currentSpeed/2) * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(this.transform.position, strafeDirection, currentSpeed * Time.deltaTime);
        }
    }
}
