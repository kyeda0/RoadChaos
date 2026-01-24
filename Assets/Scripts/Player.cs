using System;
using UnityEngine;
public class Player: Transport
{
    private float currentLane = 0;
    private float distantLane = 2;
    private float targetRotation;
    public float tiltAmount;
    public float smoothSpeed;
    [SerializeField] private float smoothAngle;
    public float minLane;
    public float maxLane;
    private bool isCrush = false;
    [SerializeField] private float timeForInvincibility;
    public int health;
    public bool isMoveChange = false;
    public event Action OnGameOverEvent;
    public bool isPossibleToMove = true;
    [SerializeField] private Animator animatorPlayer;
    private HealthUI healthUI;


    private void Start()
    {
        minLane = -1;
        maxLane = 1;
        animatorPlayer.GetComponent<Animator>();
    }
    private void Update()
    {
        if (isCrush == true)
        {
            if (timeForInvincibility <= 0)
            {
                boxCollider2D.isTrigger = false;
                animatorPlayer.SetBool("IsCrush",false);
                isCrush = false;

            }
            else
            {
                timeForInvincibility -= Time.deltaTime;
            }
        }
        ControlMove();
    }


    public void ClampLane()
    {
        currentLane = Mathf.Clamp(currentLane, minLane, maxLane);
    }
    private void ChangeLine(int direction)
    {
        currentLane = Mathf.Clamp(currentLane + direction, minLane, maxLane);
        targetRotation = direction * tiltAmount;
    }
    protected override void Move()
    {
        var targetPos = new Vector2(currentLane * distantLane,  rigidbody2d.position.y);
        var newPos = Vector2.Lerp(rigidbody2d.position,targetPos,smoothSpeed * Time.fixedDeltaTime);

        var newRotation = Mathf.LerpAngle(transform.rotation.eulerAngles.z,-targetRotation, smoothAngle * Time.fixedDeltaTime);

        transform.rotation = Quaternion.Euler(0,0,newRotation);

        rigidbody2d.MovePosition(newPos);

        if(Mathf.Abs(newPos.x - targetPos.x) < 0.5f)
        {
            targetRotation = 0f;
        }
    }

    private void ControlMove()
    {
        if (Input.GetMouseButtonDown(0) && isPossibleToMove == true )
        {
            Vector3 tochPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(tochPos.x < transform.position.x && isMoveChange == false)
            {
                ChangeLine(-1);
               
            }
            else if(tochPos.x > transform.position.x && isMoveChange == false)
            {
                ChangeLine(1);
            }
            else if (tochPos.x < transform.position.x && isMoveChange == true)
            {
                ChangeLine(1);
               
            }
            else if (tochPos.x > transform.position.x && isMoveChange == true)
            {
                ChangeLine(-1);
               
            }
        }

      
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("EnemyCar"))
        {
            health--;
            if(health <= 0)
            {
                OnGameOverEvent?.Invoke();
                GameObject.Find("PanelForHealth").GetComponent<HealthUI>().DeleteHealth();
            }
            else
            {
                animatorPlayer.Play("CrushAnimation");
                isCrush = true;
                boxCollider2D.isTrigger = true;
                timeForInvincibility = 1f;
                GameObject.Find("PanelForHealth").GetComponent<HealthUI>().DeleteHealth();
            }

        }
    }
}


