using UnityEngine;
using System;
public class Key : MonoBehaviour
{
    public int index;
    public bool active;
    public Color[] colors;
    public AudioClip noteSound;

    private HarmonyKeyboard keyboard;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private Color activeColor = new Color(0f, 255/255f, 14/255f);
    private Color normalColor = Color.white;



    public void Awake()
    {
        keyboard = FindObjectOfType<HarmonyKeyboard>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
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
        animator.SetBool("Pressed", active);
        animator.SetBool("Down", true);
        keyboard.OnKeyPressed(this);
    }
    public void OnMouseUp()
    {
        animator.SetBool("Down", false);
    }
}
