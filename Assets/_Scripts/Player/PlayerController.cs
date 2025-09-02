using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public GameObject player;
    public Transform camTarget;

    #region References
    private CharacterController cc;
    private Animator anim;
    private PlayerInteract interact;
    private Vector3 moveInput;
    #endregion

    #region Boolean
    public bool IsRunning;
    public bool IsEnding;
    #endregion

    #region Movement Values
    public float walkSpeed = 4.0f;
    public float runSpeed = 10.0f;
    public float jumpPower = 6.0f;
    public float gravityScale = 2f;
    public float mouseSensitivity = 100f;
    private float xRotation;
    #endregion

    public Transform firePoint;
    public Weapon activeWeapon = null;
    public List<Weapon> allWeapon = new List<Weapon>();
    public int currentWeapon;
    public GameObject muzzleFlash;

    void Awake()
    {
        instance = this;

        cc = GetComponent<CharacterController>();
        interact = GetComponent<PlayerInteract>();

        anim = GetComponent<Animator>();
    }

    void Start()
    {
        UpdateAmmo();
        UpdateAmmo_Meta();
    }

    void Update()
    {
        if (!UIController.instance.pauseScreen.activeInHierarchy)
        {
            if (!IsEnding)
            {
                #region Moving Input
                float yStore = moveInput.y;
                Vector3 moveV = transform.forward * Input.GetAxis("Vertical");
                Vector3 moveH = transform.right * Input.GetAxis("Horizontal");
                moveInput = (moveH + moveV).normalized;

                #region Walk & Run
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    IsRunning = true;
                    if (PlayerHealthController.instance.currentStamina != 0)
                    {
                        moveInput *= runSpeed;
                    }
                    else
                    {
                        moveInput *= walkSpeed;
                    }
                }
                else
                {
                    IsRunning = false;
                    moveInput *= walkSpeed;
                }

                moveInput.y = yStore;
                moveInput.y += Physics.gravity.y * gravityScale * Time.deltaTime;
                #endregion

                #region Jump
                if (cc.isGrounded)
                {
                    moveInput.y = -1f;
                    moveInput.y += Physics.gravity.y * gravityScale * Time.deltaTime;
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        moveInput.y = jumpPower;
                        AudioManager.instance.PlaySFX("Jump");
                    }
                }
                #endregion

                cc.Move(moveInput * Time.deltaTime);
                #endregion

                #region Mouse Movement (Camera Rotation)

                float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
                Vector2 mouseInput = new Vector2(mouseX, mouseY);
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);
                xRotation -= mouseInput.y;
                xRotation = Mathf.Clamp(xRotation, -80f, 80f);
                camTarget.localEulerAngles = new Vector3(xRotation, 0f, 0f);

                #endregion
            }

            #region IsEnding
            if (IsEnding)
            {
                moveInput = Vector3.zero;
                moveInput.y += Physics.gravity.y * gravityScale * Time.deltaTime;
                cc.Move(moveInput);
            }
            #endregion

            #region Animation

            anim.SetFloat("Speed", Mathf.Abs(moveInput.x) + Mathf.Abs(moveInput.z));

            #endregion

            if (allWeapon != null) // if has weapon in list
            {
                // switch weapon
                if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
                {
                    SwitchWeapon();
                    AudioManager.instance.PlaySFX("Swap");
                }

                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    ActivateWeapon();
                }

                if (activeWeapon.gameObject.activeInHierarchy)
                {
                    #region Attack (Left Click)
                    if (Input.GetMouseButtonDown(0) && activeWeapon.attackCounter <= 0) // left click & single shot
                    {
                        RaycastHit hit;
                        if (Physics.Raycast(camTarget.position, camTarget.forward, out hit, 500f))
                        {
                            firePoint.LookAt(hit.point);
                        }
                        else
                        {
                            firePoint.LookAt(camTarget.position + (camTarget.forward * 30f));
                        }
                        FireShot();
                    }
                    #endregion
                }
            }
        }
    }

    public void FireShot()
    {
        if (activeWeapon.currentAmmo > 0)
        {
            activeWeapon.currentAmmo--;
            Instantiate(activeWeapon.bullet, firePoint.position, firePoint.rotation);
            activeWeapon.attackCounter = activeWeapon.attackRate; // reset value
            if (muzzleFlash)
            {
                Instantiate(muzzleFlash, firePoint.position, firePoint.rotation);
            }
            UpdateAmmo();
            UpdateAmmo_Meta();
            activeWeapon.AttackAnim();
            AudioManager.instance.PlaySFX("PistolFire");
        }
        else
        {
            activeWeapon.reloadCounter = activeWeapon.reloadRate;
            activeWeapon.ReloadAmmo();
        }

    }

    public void UpdateAmmo()
    {
        if (allWeapon.Count >= 0)
        {
            UIController.instance.ammoText.text = activeWeapon.currentAmmo.ToString();
            UIController.instance.bulletAmmo.text = activeWeapon.currentAmmo.ToString();
        }
    }

    public void UpdateAmmo_Meta()
    {
        if (allWeapon.Count >= 0)
        {
            GameObject[] ammoAmount = UIController.instance.ammoNum;
            for (int i = 0; i < ammoAmount.Length; i++)
            {
                ammoAmount[ammoAmount.Length - activeWeapon.currentAmmo].SetActive(false);
            }
        }
    }

    public void AddWeapon(Weapon weapon)
    {
        allWeapon.Add(weapon);
        UIController.instance.weaponImage.gameObject.SetActive(true);
        activeWeapon = allWeapon[0];
        activeWeapon.gameObject.SetActive(true);
        UIController.instance.weaponImage.sprite = activeWeapon.weaponSprite;
        UIController.instance.bulletUI.SetActive(true);
        UpdateAmmo();
        UpdateAmmo_Meta();
    }

    void SwitchWeapon()
    {
        activeWeapon.gameObject.SetActive(false);
        currentWeapon++;
        if (currentWeapon >= allWeapon.Count)
        {
            currentWeapon = 0;
        }
        activeWeapon = allWeapon[currentWeapon];
        activeWeapon.gameObject.SetActive(true);
        UIController.instance.weaponImage.sprite = activeWeapon.weaponSprite;
        UpdateAmmo();
        UpdateAmmo_Meta();
    }

    void ActivateWeapon()
    {
        if (activeWeapon.gameObject.activeInHierarchy)
        {
            activeWeapon.gameObject.SetActive(false);
        }
        else
        {
            activeWeapon.gameObject.SetActive(true);
        }

    }

}