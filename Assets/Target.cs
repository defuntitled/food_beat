using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRigitbody;
    private GameManager gameManager;
    private SoundController sfx;

    [SerializeField] private float forceMin = 14;
    [SerializeField] private float forceMax = 16;
    [SerializeField] private float torqueRange = 10;
    [SerializeField] private float xRange = 4;
    [SerializeField] private float ySpawnPos = -6;
    [SerializeField] private float destroyHeight = -50;
    [SerializeField] private int givingScore;
    [SerializeField] private ParticleSystem effect;
    
    
    void Start()
    {
        
        sfx = GameObject.Find("Sound").GetComponent<SoundController>();
        gameManager = GameObject.Find("GameMaster").GetComponent<GameManager>();
        targetRigitbody = GetComponent<Rigidbody>();
        targetRigitbody.AddForce(RandomForce(), ForceMode.Impulse);
        targetRigitbody.AddTorque(RandomTorque(), RandomTorque(), RandomTorque());
        transform.position = RandomPosition();
    }

    private Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos,0);
    }

    private float RandomTorque()
    {
        return Random.Range(-torqueRange, torqueRange);
    }

    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(forceMin, forceMax);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= destroyHeight) 
        {
            Destroy(gameObject);
            if (gameObject.CompareTag("Good"))
            { 
                gameManager.Fali(); 
            }

        }
        
    }

  

    private void OnMouseEnter()
    {
        bool mouseButttonPushed = Input.GetMouseButton(0);
        if ((!gameManager.gameOver) && mouseButttonPushed) 
        {
            Debug.Log(Input.GetMouseButton(0));
            
            gameManager.ImplicateScore(givingScore);
            Destroy(gameObject);
        }
    }
   

    private void OnDestroy()
    {
        if (transform.position.y > destroyHeight)
        {
            if (gameObject.CompareTag("Bad")) sfx.playBadSFX();
            else sfx.playGoodSFX();
            Instantiate(effect, transform.position, transform.rotation);;
        }
    }
}
