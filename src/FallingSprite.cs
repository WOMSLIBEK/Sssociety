using Godot;
using System;

public partial class FallingSprite : Sprite2D
{
    Vector2 u;

    public FallingSprite()
    {
        Random rand = new Random();
        u = new Vector2(rand.Next(-200, 200), rand.Next(-200, 200));

    }

    double deathTimer = 0;
    public override void _Process(double delta)
    {
        Position += u * (float)delta;
        u.Y += 600.0f * (float)delta;

        deathTimer += delta;
        if(deathTimer > 4)
        {
            QueueFree();
        }

    }

}
