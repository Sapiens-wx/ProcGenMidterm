using System;
using UnityEngine;

public class RBot : Tile
{
    public RBotTerminal terminal;
    public SpriteRenderer spr;
    public Rigidbody2D rb;
    [TextArea] [SerializeField] string initialCode;
    [HideInInspector]public Tile interactor;
    void Start() {
        terminal.gameObject.SetActive(false);
        terminal.code.text=initialCode;
        terminal.RunCode();
    }
	public override void pickUp(Tile tilePickingUsUp) {
        if (interactor == null) { // start interaction
            terminal.gameObject.SetActive(true);
            interactor=tilePickingUsUp;
        } else { // end interaction
        }
    }
}