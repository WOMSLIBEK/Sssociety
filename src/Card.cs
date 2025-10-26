using Godot;
using System;

public partial class Card : Sprite2D
{
    public GameManager.RockPaperScissors cardType;

    //tthis stops certain processes
    bool usable = true;

    //this is the resting position of the card and where it snaps back to if moved
    private Vector2 cardStaticPosition; 

    public Vector2 CardStaticPosition
    {
        get { return cardStaticPosition; }
        set
        {
            cardStaticPosition = value;
            Position = cardStaticPosition;
        }
    }
    
    //this is to determine what cards are best for being picked up by the player
    public float Selectability = 0;


    public Card (GameManager.RockPaperScissors cardType)
    {
        this.cardType = cardType;
        if (cardType == GameManager.RockPaperScissors.Bite)
        {
            Texture = ResourceLoader.Load<Texture2D>("res://assets/images/cards/card_bite.png");
        }

        if (cardType == GameManager.RockPaperScissors.Hiss)
        {
            Texture = ResourceLoader.Load<Texture2D>("res://assets/images/cards/card_hiss.png");
        }

        if (cardType == GameManager.RockPaperScissors.Constrict)
        {
            Texture = ResourceLoader.Load<Texture2D>("res://assets/images/cards/card_constrict.png");
        }


        

    }

    public override void _Ready()
    {

    }

    public override void _Process(double delta)
    {
        if (usable)
        {
            HighlightCard();

        }


    }

    private void HighlightCard()
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
    public void ActivateSpecialAbility()
    {
        
    }

    
    public void DeactivateCard()
    {
        usable = false;
        Selectability = 0;
        
    }

    public void ResetPosition()
    {
        Position = cardStaticPosition;
        
    }
    

}
