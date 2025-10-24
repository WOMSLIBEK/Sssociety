using Godot;
using System;

public partial class Card : Sprite2D
{
    public int cardType = -1;

    //this is the resting position of the card and where it snaps back to if moved
    private Vector2 cardPosition; 

    public Vector2 CardPosition
    {
        get { return cardPosition; }
        set
        {
            cardPosition = value;
            Position = cardPosition;
        }
    }
    
    //this is to determine what cards are best for being picked up by the player
    public float Selectability = 0;


    public Card (int cardType)
    {
        this.cardType = cardType;
    }

    public override void _Ready()
    {
        Texture = ResourceLoader.Load<Texture2D>("res://assets/images/icon.svg");

    }

    public override void _Process(double delta)
    {
        Rect2 box = new Rect2(GlobalPosition - .5f * Texture.GetSize(), Texture.GetSize());
        if (box.HasPoint(GetGlobalMousePosition()))
        {
            Vector2 thingy = new Vector2(GetGlobalMousePosition().X, GlobalPosition.Y);
            float closeness = 1 - (thingy.DistanceTo(GlobalPosition) / Texture.GetSize().X);

            Scale = Vector2.One + .5f * new Vector2(
                closeness,
                closeness);

            Selectability = closeness;
            ZIndex = (int)Mathf.Floor(closeness * 6);
        }
        else
        {
            Scale = Vector2.One;
            Selectability = 0;
            ZIndex = 0;
        }

    }

    public void ResetPosition()
    {
        Position = cardPosition;
    }
    

}
