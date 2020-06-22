using UnityEngine;
using UnityEngine.SceneManagement;

//This code belongs to Sebastian Lague
[RequireComponent (typeof(Controller2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private Controller2D controller;
    [SerializeField] private Vector3 velocity;
    //This code belongs to me
    public int currentHP;
    [SerializeField] private int maxHP = 5;
    [SerializeField] private GameObject deadChunkParticle, bloodParticle; 
    private GameManager gm;
    public Vector2 respawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        //This code belongs to Sebastian Lague
        controller = GetComponent<Controller2D>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        //This code belongs to me
        currentHP = maxHP;    
        respawnPosition = this.transform.position;
    }
    void Update()
    {
        
    }
    //This code belongs to me
    public void Death()
    {
        Instantiate(deadChunkParticle, transform.position, deadChunkParticle.transform.rotation);
        Instantiate(bloodParticle, transform.position, bloodParticle.transform.rotation);
        gm.Respawn();
        Destroy(gameObject);
        if(PlayerPrefs.GetInt("highScore") < gm.points){
            PlayerPrefs.SetInt("highScore", gm.points);
        }
    }

    public void DecreasePlayerHP(int dam){
        currentHP -= dam;
        gameObject.GetComponent<Animation>().Play("redflash");
        if (currentHP <= 0)
        {
            Death();
        }
    }
    public void Knockback(Vector3 knockDir){
        velocity = new Vector3(0,0,0);
        velocity = new Vector3(knockDir.x * -15, controller.jumpVelocity-2, 0);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Death")
        {
            gm.Respawn();
        }
    }
}
