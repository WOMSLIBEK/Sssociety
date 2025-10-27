using Godot;
using System;

public partial class EggPile : Node2D
{

    public override void _Ready()
    {
        GenerateEggs(4);
    }

    private void GenerateEggs(int numberOfEggs)
    {
        Random rand = new Random();
        for (int i = 0; i < numberOfEggs; i++)
        {
            Sprite2D newEgg = new Sprite2D();
            newEgg.Texture = ResourceLoader.Load<Texture2D>("res://assets/images/UI/Egg.png");
            newEgg.Scale = new Vector2(2, 2);
            newEgg.Position += new Vector2(rand.Next(-40, 40), rand.Next(-40, 40));
            AddChild(newEgg);
        }
    }

    double eggTimer = 0;
    bool eggAnimation = false;
    Vector2[] eggVelocities;
    public void PlayEggAnimation()
    {
        Random rand = new Random();
        eggVelocities = new Vector2[GetChildCount()];
        for (int i = 0; i < GetChildCount(); i++)
        {
            eggVelocities[i] = new Vector2(rand.Next(-200, 200), rand.Next(-200, 200));
        }
        eggAnimation = true;
        
    }

    Vector2 lastScreenSize = Vector2.Zero;
    public override void _Process(double delta)
    {
        if (GetViewport().GetVisibleRect().Size != lastScreenSize)
        {
            Position = GetViewport().GetVisibleRect().Size / 2 + new Vector2(-150, -40);
            lastScreenSize = GetViewport().GetVisibleRect().Size;
        }

        if(eggTimer > 2)
        {
            eggTimer = 0;
            eggAnimation = false;
            foreach (Node child in GetChildren())
            {
                child.QueueFree();
            }
            GenerateEggs(4);
        }

        if (eggAnimation)
        {
            for (int i = 0; i < GetChildCount(); i++)
            {
                GetChild<Sprite2D>(i).Position += eggVelocities[i] * (float)delta;
                eggVelocities[i].Y += 600.0f * (float)delta;
            }
            eggTimer += delta;


        }
        

    }
}
