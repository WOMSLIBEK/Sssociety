using Godot;
using System;

public partial class CardStackMaintainer : Sprite2D
{

    Vector2 lastScreenSize = Vector2.Zero;
    public override void _Process(double delta)
    {
        if(GetViewport().GetVisibleRect().Size != lastScreenSize)
        {
            Position = GetViewport().GetVisibleRect().Size / 2;
            lastScreenSize = GetViewport().GetVisibleRect().Size;
            foreach(Card child in GetChildren())
            {
                
            }
        }
    }

}
