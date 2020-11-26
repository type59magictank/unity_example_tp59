using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_control : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] spritestext = new Sprite[3];
    SpriteRenderer sprite_Renderer;


    void Start()
    {
        sprite_Renderer = gameObject.GetComponent<SpriteRenderer>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        sprite_Renderer.sprite = spritestext[1];
    }

    private void OnMouseDown()
    {
        sprite_Renderer.sprite = spritestext[2];
        SceneManager.LoadScene("scene1");
    }

    private void OnMouseUp()
    {
        sprite_Renderer.sprite = spritestext[0];
    }

    private void OnMouseExit()
    {
        sprite_Renderer.sprite = spritestext[0];
    }

}
