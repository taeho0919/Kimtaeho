using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("이동관련 세팅")]
    public float moveSpeed = 5f;
    public float sprintMultiplier = 1.5f;
    public float gravity = -9.81f;

    [Header("마우스 세팅")]
    public float mouseSensitivity = 100f;
    public Transform cameraTransform;

    [Header("스테미너 세팅")]
    public float maxStamina = 5f;
    public float staminaDecreaseRate = 1f;
    public float staminaRegenRate = 0.5f;

    [Header("스테미너 UI")]
    public Image staminaBar;

    [Header("페이드 인 아웃 세팅")]
    public FadeInOut fadeManager;
    public CanvasGroup canvasGroup;

    [Header("기타")]
    public bool objectOnOff;

    // 내부 세팅
    private float currentStamina;
    private bool isGameOver = false;
    private CharacterController controller;
    private float xRotation = 0f;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();

            Cursor.lockState = CursorLockMode.Locked;

        currentStamina = maxStamina;

        if (staminaBar != null)
        {
            staminaBar.fillAmount = 1f;
        }
            
    }

    void Update()
    {
        if (isGameOver) return;

        LookAround();
        Move();
        HandleStamina();
    }

    void LookAround()
    {
        if (!objectOnOff)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);
        }
    }

    void Move()
    {
        if (!objectOnOff)
        {
            isGrounded = controller.isGrounded;

            if (isGrounded && velocity.y < 0)
                velocity.y = -2f;

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector3 move = transform.right * h + transform.forward * v;

            float currentSpeed = moveSpeed;

            if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0f)
                currentSpeed *= sprintMultiplier;

            controller.Move(move * currentSpeed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }

    void HandleStamina()
    {
        if (!objectOnOff)
        {
            bool isMoving = (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0);
            bool isSprinting = Input.GetKey(KeyCode.LeftShift);
            bool isRunning = isSprinting && isMoving && isGrounded;

            if (isRunning)
            {
                currentStamina -= staminaDecreaseRate * Time.deltaTime;
            }
            else
            {
                currentStamina += staminaRegenRate * Time.deltaTime;
            }

            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);

            if (staminaBar != null)
                staminaBar.fillAmount = currentStamina / maxStamina;

            if (currentStamina <= 0f)
            {
                GameOver();
            }
        }
    }

    void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;

        fadeManager.fadeDuration = 1f;

        fadeManager.FadeOut(() =>
        {
            SceneManager.LoadScene("GameOverScene");
        });

        Debug.Log("게임 오버!");
    }

}