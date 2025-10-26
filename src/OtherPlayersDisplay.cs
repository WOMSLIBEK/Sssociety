using Godot;
using System;
using System.Linq;

public partial class OtherPlayersDisplay : Node
{
    Gameboard gameboard;

    public override void _Ready()
    {
        gameboard = GetParent<Gameboard>();
        if (gameboard == null)
        {
            GD.PrintErr("The other player display must be a child of the gameboard");
            QueueFree();
        }

        gameboard.Connect(Gameboard.SignalName.NextTurn, Callable.From(ResetCharacterPositions));

        PackedScene packedGorgon = GD.Load<PackedScene>("res://scene_objects/GorgonOtherPlayer.tscn");
        //generate the sprites
        foreach ((GameManager.RockPaperScissors playerType, bool isAI) playerInfo in GameManager.gameManager.players)
        {
            Sprite2D gorgon = packedGorgon.Instantiate<Sprite2D>();

            if (playerInfo.playerType == GameManager.RockPaperScissors.Bite)
            {
                gorgon.Texture = ResourceLoader.Load<Texture2D>("res://assets/images/characters/Gorgon3.png");
            }
            if (playerInfo.playerType == GameManager.RockPaperScissors.Hiss)
            {
                gorgon.Texture = ResourceLoader.Load<Texture2D>("res://assets/images/characters/Gorgon2.png");

            }
            if (playerInfo.playerType == GameManager.RockPaperScissors.Constrict)
            {
                gorgon.Texture = ResourceLoader.Load<Texture2D>("res://assets/images/characters/Gorgon1.png");
            }

            AddChild(gorgon);
        }

        ResetCharacterPositions();
    }

    Vector2 lastScreenSize = Vector2.Zero;
    public override void _Process(double delta)
    {
        if(GetViewport().GetVisibleRect().Size != lastScreenSize)
        {
            lastScreenSize = GetViewport().GetVisibleRect().Size;
            ResetCharacterPositions();
        }
    }

    //FIX THIS THE oh who will even notice wtfd do something else goddamn 
    private void ResetCharacterPositions()
    {
        int playerIndex = gameboard.PlayerIndex;


        foreach (Sprite2D child in GetChildren())
        {
            child.Position = new Vector2(-5000, 0);
        }

        int whoopsAllIndex = 0;
        for (int i = playerIndex + 1; i < GameManager.gameManager.players.Count() + playerIndex; i++)
        {
            int trueIndex = i;
            if (i >= GameManager.gameManager.players.Count())
            {
                trueIndex -= GameManager.gameManager.players.Count();
            }


            
            float step = GetViewport().GetVisibleRect().Size.X / GameManager.gameManager.players.Count();
            whoopsAllIndex += 1;
            float sinScale = GetViewport().GetVisibleRect().Size.Y / 3;
            GetChild<Sprite2D>(trueIndex).Position = new Vector2(step * whoopsAllIndex,
            GetChild<Sprite2D>(trueIndex).Texture.GetSize().Y +
            sinScale - sinScale*Mathf.Sin(3.14f*whoopsAllIndex/GameManager.gameManager.players.Count()));
            





        }

    }


    

}
