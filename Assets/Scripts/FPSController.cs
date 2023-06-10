using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 2f;
    public float runSpeed = 4f;
    public float jumpPower = 3f;
    public float gravity = 10f;


    public float lookSpeed = 2f;
    public float lookXLimit = 45f;


    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;


    CharacterController characterController;

    public int maxHealth = 100;
    public int maxStamina = 100;
    public float staminaDrainRate = 80f;


    [HideInInspector]
    public int currentHealth;
    [HideInInspector]
    public int currentStamina;
    [HideInInspector]
    public int currentScore = 0;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentHealth = maxHealth;
        currentStamina = maxStamina;
    }

    void Update()
    {

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        #region Handles Movment
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && currentStamina > 0;
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        // Drena stamina se o jogador estiver correndo
        if (isRunning && currentStamina > 0)
        {
            currentStamina -= (int)(staminaDrainRate * Time.deltaTime);

        }
        else if (!isRunning && currentStamina < maxStamina)
        {
            // Recupera stamina se o jogador não estiver correndo
            currentStamina += (int)(staminaDrainRate * Time.deltaTime);
        }

        // Garante que a stamina nunca saia dos limites
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);


        #endregion

        #region Handles Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        #endregion

        #region Handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        #endregion
    }

    public void TakeDamage(int damage)
    {
        // Deduz a vida do jogador
        currentHealth -= damage;

        // Garante que a vida nunca saia dos limites
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Verifica se o jogador morreu
        if (currentHealth <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            // O jogador morreu, carrega a cena do menu
            SceneManager.LoadScene("MenuScene");
        }
    }

    public void IncreaseScore(int score)
    {
        currentScore += score;
    }

}
