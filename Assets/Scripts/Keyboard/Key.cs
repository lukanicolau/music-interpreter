using UnityEngine;
using System;
public class Key : MonoBehaviour
{
    Keyboard keyboard;
    SpriteRenderer spriteRenderer;

    public int index;
    public bool active;
    public KeyColor keyColor;

    public AudioClip noteSound;
    private Color activeColor = new Color(0f, 255/255f, 14/255f);
    private Color normalColor = Color.white;
    public Color[] colors;


    public void Awake()
    {
        keyboard = FindObjectOfType<Keyboard>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (name.Contains("white"))
        {
            keyColor = Key.KeyColor.WHITE;
        } else
        {
            keyColor = Key.KeyColor.BLACK;
        }
    }

    public void Start()
    {
        noteSound = keyboard.soundPreset.notes[index];
        colors = new Color[2];
        colors[0] = normalColor;
        colors[1] = activeColor;
}

    public void OnMouseDown()
    {
        active = !active;
        spriteRenderer.color = colors[Convert.ToInt32(active)];
        keyboard.OnKeyPressed(this);
    }
    
    public enum KeyColor
    {
        WHITE,
        BLACK
    }
}
