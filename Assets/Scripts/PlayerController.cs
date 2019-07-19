using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private int count;
    private int lives;
    public AudioSource musicSource;
    public AudioSource background;
    public float speed;
    public float jumpForce;
    public Text countText;
    public Text winText;
    public Text loseText;
    public Text livesText;
    public AudioClip backgroundmusic;
    public AudioClip victory;
    Animator anim;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        lives = 3;
        winText.text = "";
        loseText.text = "";
        SetCountText ();
        SetLivesText ();
        background.clip = backgroundmusic;
        background.Play();
        anim = GetComponent<Animator>();
        

    }

    void Update()
    {
        if (Input.GetKeyDown (KeyCode.LeftArrow))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp (KeyCode.LeftArrow))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown (KeyCode.RightArrow))
        {
            anim.SetInteger("State", 1);
        }
        if(Input.GetKeyUp (KeyCode.RightArrow))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown (KeyCode.UpArrow))
        {
            anim.SetInteger("State", 3);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            anim.SetInteger("State", 0);
        }
    
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        Vector3 chracterScale = transform.localScale;
        if (Input.GetAxis("Horizontal") < 0)
        {
            chracterScale.x = -.5f;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            chracterScale.x = .5f;
        }
        transform.localScale = chracterScale;
    }

    void FixedUpdate()
    {
     
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, 0);

        rb2d.AddForce(movement * speed);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground") {

            if(Input.GetKey(KeyCode.UpArrow))
            {
                rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText ();
            SetLivesText ();
          
        }
        else
        {
            other.gameObject.CompareTag("Enemy");
            other.gameObject.SetActive(false);
            lives = lives - 1;
            SetCountText();
            SetLivesText();
        }
        if (count == 4)
        {
            transform.position = new Vector2(33.0f, 0.0f);

        }
    }
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 8)
        {
            winText.text = "You win!";
            musicSource.clip = victory;
            musicSource.Play();
            background.Stop();

        }

    }
    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives == 0)
        {
            loseText.text = "Game over!";
        }
    }
}
