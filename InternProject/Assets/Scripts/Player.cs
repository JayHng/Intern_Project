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
    public int maxHP = 5;

    // Start is called before the first frame update
    void Start()
    {
        //This code belongs to Sebastian Lague
        controller = GetComponent<Controller2D>();

        //This code belongs to me
        currentHP = maxHP;    
    }
    void Update()
    {
        if (currentHP <= 0)
        {
            Death();
        }
    }
    //This code belongs to me
    public void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlayerDamage(int dam){
        currentHP -= dam;
    }
    public void Knockback(Vector3 knockDir){
        velocity = new Vector3(0,0,0);
        velocity = new Vector3(knockDir.x * -15, controller.jumpVelocity-2, 0);
    } 
}
