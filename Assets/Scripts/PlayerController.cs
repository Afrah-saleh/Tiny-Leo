using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameManager gameManger;
    Rigidbody rb;
    public float moveSpeed = 5.0f;

    public int maxHealth = 1;

     [SerializeField] int currentHealth;
    public float JumpForce = 7.0f;

    public float damageBufferTime = 0.25f;

    float damageBufferTimer;

    private Camera mainCamera;

    public int CollectedCoins;
     bool isAlive = true;
     bool canMove = true;
    private void Awake(){
        gameManger = FindAnyObjectByType<GameManager>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        currentHealth = maxHealth;
        damageBufferTimer = damageBufferTime;
         gameManger.UpdateHealthText(currentHealth,maxHealth);
    }
    void Update()
    {
        if(isAlive == false || canMove == false){
            return;
        }
        if(damageBufferTimer > 0){
            damageBufferTimer -= Time.deltaTime;

        }
     Movement();
     Jump();
    }
    public void Movement(){
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

         Vector3 cameraForward = mainCamera.transform.forward;
         Vector3 cameraRight = mainCamera.transform.right;

         cameraForward.y= 0;
         cameraRight.y= 0;

        Vector3 moveDirection = cameraForward.normalized * verticalInput + cameraRight.normalized * horizontalInput;

        if(moveDirection != Vector3.zero){
            //change Rotation
            transform.forward = moveDirection;

            //move the charcter
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }  
        }

        public void Jump(){
            if(Input.GetButtonDown("Jump")){
                rb.AddForce(Vector3.up * JumpForce , ForceMode.Impulse);
            }
        }
        public void TakeDamage(Vector3 damageDirection , int damage){
            damageDirection.Normalize();
            rb.AddForce(-damageDirection * 2, ForceMode.Impulse);      
             currentHealth -= damage;

             if(currentHealth <= 0){
                currentHealth = 0;
                isAlive = false;
                gameManger.OpenTryAgainMenu();
             }
             damageBufferTimer = damageBufferTime;
        }
         void OnCollisionEnter(Collision other) {
            if(other.gameObject.CompareTag("Damage") && damageBufferTimer <= 0){
                    Vector3 damageDirection = other.transform.position - transform.position;
                    int damage = other.gameObject.GetComponent<TakeDamage>().damage;
                    
                    TakeDamage(damageDirection , damage);
                
                   gameManger.UpdateHealthText(currentHealth,maxHealth);
            }
        }

        private void OnTriggerEnter(Collider other) {
             if(other.gameObject.CompareTag("Coin")){
                    int coinValue = other.GetComponent<Coins>().value;
                   CollectedCoins += coinValue;
                   gameManger.UpdateCoinText(CollectedCoins);
                   Destroy(other.gameObject);
            }
            if(other.gameObject.CompareTag("Won")){
                canMove = false;
                gameManger.OpenYouWonMenu();
            }
        }
}
