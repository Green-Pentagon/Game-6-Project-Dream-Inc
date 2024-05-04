using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBehaviourScript : MonoBehaviour
{
    //movement
    private Rigidbody2D rb;
    private float speed = 35.0f;

    //player state
    private float timeAlive = 0.0f;
    private bool alive = true;
    private bool won = false;
    private TextMeshProUGUI EndofGameText;
    public GameObject EndOfGameCard;

    //primary fire
    private AudioSource fireSound;
    private bool fire = false;
    private KeyCode fireKey = KeyCode.Mouse0;

    //money
    public TextMeshProUGUI moneyReadout;
    public TextMeshProUGUI moneyMultiplierReadout;
    private int bossDeskAwake = 1;
    private float currentCash = 1000;
    private float cashMultiplier = 0.0f;
    private Color defaultColour = Color.white;
    private Color positiveColour = Color.green;
    private Color negativeColour = new Color(0.906f, 0.033f, 0.0f);

    //employee object references
    private GameObject employee;
    private EmployeeBehaviourScript employeeScript;

    IEnumerator MoneyReadoutColourFade(bool wasGain)
    {
        if (bossDeskAwake == 1)
        {
            if (wasGain)
            {
                moneyReadout.color = positiveColour;
            }
            else
            {
                moneyReadout.color = negativeColour;
            }
            yield return new WaitForSeconds(1.0f);
            moneyReadout.color = defaultColour;
        }
        else
        {
            yield return null;
        }
        
    }

    public float GetCurrentCash()
    {
        return currentCash;
    }

    public void SubtractCash(int Amout)
    {
        if (currentCash > Amout)
        {
            currentCash -= Amout;
            StartCoroutine(MoneyReadoutColourFade(false));
        }
    }

    public void AlterCashMultiplier(float factor)
    {
        cashMultiplier += factor;
    }

    public void BossDeskState(bool isAwake)
    {
        if (isAwake)
        {
            bossDeskAwake = 1;
            moneyReadout.color = defaultColour;
        }
        else
        {
            bossDeskAwake = 0;
            moneyReadout.color = negativeColour;
        }
    }

    private void UpdateCashReadout()
    {
        
        if (cashMultiplier <= 0.0f)
        {
            moneyMultiplierReadout.color = negativeColour;
        }
        else
        {
            moneyMultiplierReadout.color = positiveColour;
        }
        moneyReadout.text = currentCash.ToString();
        moneyMultiplierReadout.text = "x" + cashMultiplier.ToString();
    }

    private void GameOver()
    {
        alive = false;
        timeAlive = Mathf.RoundToInt(timeAlive * 100.0f) / 100.0f;
        if (!won)
        {
            EndofGameText.color = negativeColour;
            EndofGameText.text = "Game Over!\nYour dream business lasted for " + timeAlive.ToString() + " seconds.";
        }
        else
        {
            EndofGameText.color = positiveColour;
            EndofGameText.text = "Game Over!\nYour dream business succeeded in " + timeAlive.ToString() + " seconds!";
        }
        
        EndOfGameCard.SetActive(true);
        
    }


    // Start is called before the first frame update
    void Start()
    {
        EndofGameText = EndOfGameCard.transform.GetChild(0).ConvertTo<TMPro.TextMeshProUGUI>();
        EndOfGameCard.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        fireSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            float xMovement = Input.GetAxis("Horizontal") * speed;
            float yMovement = Input.GetAxis("Vertical") * speed;
            rb.velocity = new Vector2(xMovement, yMovement);

            if (Input.GetKeyDown(fireKey) && !fire)
            {
                fire = true;
                fireSound.Play();
            }
            if (Input.GetKeyUp(fireKey) && fire)
            {
                fire = false;
            }

            UpdateCashReadout();
        }
    }


    private void FixedUpdate()
    {
        if (currentCash >= 20000.0f)
        {
            won = true;
            GameOver();
        }
        else if (currentCash > 0.0f)
        {
            currentCash += (cashMultiplier * bossDeskAwake);
            timeAlive += Time.fixedDeltaTime;
        }
        else if (alive)
        {
            GameOver();
        }
    }
        

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Employee")
        {
            employee = collision.gameObject;
            employeeScript = employee.GetComponent<EmployeeBehaviourScript>();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (fire)
        {
            if (collision.gameObject != employee)
            {
                employee = collision.gameObject;
                employeeScript = employee.GetComponent<EmployeeBehaviourScript>();
            }
            employeeScript.WakeUp();
        }
        
    }
}
