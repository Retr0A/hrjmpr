using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacter : MonoBehaviour
{
    [Header("Camera Look / Movement")]
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    [Header("Player Stats")]
    public int health = 100;
    public int stamina = 100;
    public int hunger = 100;

    [Header("Terrain Editing")]
    public LayerMask groundLayer;

    [Header("Inventory System")]
    public List<InventoryPack> inventory = new List<InventoryPack>();

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift) && stamina > 0;
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        StartCoroutine(GetHungry());

        if (isRunning)
        {
            StartCoroutine(SmoothlyRemoveStamina());
        }

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, Mathf.Infinity, groundLayer))
            {
                if (hit.transform.gameObject.GetComponent<Tile>())
                {
                    hit.transform.localScale += new Vector3(0, 0.2f, 0);
                }
            }
        }
        if (Input.GetMouseButton(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, Mathf.Infinity, groundLayer))
            {
                if (hit.transform.localScale.y >= 0.1f && hit.transform.gameObject.GetComponent<Tile>())
                {
                    hit.transform.localScale -= new Vector3(0, 0.2f, 0);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, Mathf.Infinity, groundLayer))
            {
                    Debug.Log(hit.transform.gameObject.name);
                if (hit.transform.gameObject.GetComponent<InventoryItemPrefab>())
                {
                    hit.transform.gameObject.GetComponent<InventoryItemPrefab>().PickUp(this);
                }
            }
        }
    }

    IEnumerator SmoothlyRemoveStamina()
    {
        RemoveStamina(1);
        yield return new WaitForSeconds(2);
    }

    IEnumerator GetHungry()
    {
        RemoveHunger(1);
        yield return new WaitForSeconds(10);
    }

    /// <summary>
    /// Removes health from the player
    /// </summary>
    /// <param name="healthToRemove">Health to remove from the player</param>
    /// <returns>Current health</returns>
    public int RemoveHealth(int healthToRemove)
    {
        health -= healthToRemove;
        return health;
    }

    /// <summary>
    /// Removes stamina from the player
    /// </summary>
    /// <param name="staminaToRemove">Stamina to remove from the player</param>
    /// <returns>Current stamina</returns>
    public int RemoveStamina(int staminaToRemove)
    {
        stamina -= staminaToRemove;
        return stamina;
    }

    /// <summary>
    /// Removes hunger from the player
    /// </summary>
    /// <param name="hungerToRemove">Hunger to remove from the player</param>
    /// <returns>Current hunger</returns>
    public int RemoveHunger(int hungerToRemove)
    {
        hunger -= hunger;
        return hungerToRemove;
    }

    /// <summary>
    /// Add item to inventory
    /// </summary>
    /// <param name="item">Inventory Item to add</param>
    /// <param name="quantity">Quantity to add</param>
    /// <returns>The new added inventory item</returns>
    public InventoryPack AddInventoryItem(InventoryItem item, int quantity)
    {
        InventoryPack i = new InventoryPack(item, quantity);

        inventory.Add(i);
        return i;
    }

    /// <summary>
    /// Remove item from inventory
    /// </summary>
    /// <param name="index">The index of the item to be removed</param>
    public void RemoveInventoryItem(int index)
    {
        inventory.RemoveAt(index);
    }
}

[System.Serializable]
public class InventoryPack
{
    public InventoryItem item;
    public int quantity;

    public InventoryPack(InventoryItem item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }
}