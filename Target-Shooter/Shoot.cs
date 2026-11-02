using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Shoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    private InputActionMap actionMap;
    private PlayerInput input;
    private InputAction shoot;
    public float force = 1f;
    public Camera camera;
    public float shotCooldown = 0f;
    private float shotCooldownTimer = 0f;

    private int penSkill = 0; //Penetration skill, number means how many targets the ball can penetrate through
    private float reducedShotCooldown = 0; //Reduced shot cooldown skill, number affects by how much its reduced
    private int bonusChance = 0; //Skill for bonus score on random chance. Chance of bonus applying is equal to this var


    private void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        actionMap = input.actions.FindActionMap("Player");
        actionMap.Enable();
        shoot = actionMap.FindAction("Attack");
    }

    private void OnDisable()
    {
        force = 0f;
        actionMap.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        penSkill = PlayerPrefs.GetInt("penSkill");
        reducedShotCooldown = PlayerPrefs.GetFloat("reducedShotCooldown");
        bonusChance = PlayerPrefs.GetInt("bonusChance");
        
    }


    // Update is called once per frame
    void Update()
    {
        if (shotCooldownTimer > 0f)
        {
            shotCooldownTimer -= Time.deltaTime;
        }
        else if (shoot.IsPressed() && Time.timeScale == 1f)
        {
            Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            Vector3 shootPoint;
            if (Physics.Raycast(ray, out hit))
            {
                shootPoint = hit.point;
            }
            else
            {
                shootPoint = ray.GetPoint(100);
            }

            Vector3 shootDirection = (shootPoint - transform.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(shootDirection * force, ForceMode.Impulse);
            shotCooldownTimer = shotCooldown - reducedShotCooldown;
        }
    }

    //void shootBullet(GameObject bullet)
    //{
    //    Vector3 shot = new Vector3()
    //}
}
