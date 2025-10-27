using Godot;
using System;

public partial class SpriteSpawner : Node2D
{
    [Export]
    String spritePath;

    [Export]
    float proportionalPosition=0;

    Texture2D spriteTexture;

    public override void _Ready()
    {
        spriteTexture = ResourceLoader.Load<Texture2D>(spritePath);

    }

    double menuCounter = 0;
    Vector2 lastScreenSize = Vector2.Zero;
    public override void _Process(double delta)
    {
        FallingSprite newSprite = new FallingSprite();
        newSprite.Texture = spriteTexture;
        AddChild(newSprite);

        if (GetViewport().GetVisibleRect().Size != lastScreenSize)
        {
            lastScreenSize = GetViewport().GetVisibleRect().Size;
            Position = new Vector2(GetViewport().GetVisibleRect().Size.X * proportionalPosition, -50);

        }

        menuCounter += delta;
        if(menuCounter > 7)
        {
            GetTree().ChangeSceneToFile("res://scenes/main_menu.tscn");
        }

    }
    
    

}
