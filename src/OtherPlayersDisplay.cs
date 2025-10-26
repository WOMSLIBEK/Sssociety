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

        //generate the sprites
        foreach ((GameManager.RockPaperScissors playerType, bool isAI) playerInfo in GameManager.gameManager.players)
        {
            Sprite2D gorgon = new Sprite2D();

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

    //FIX THIS THE oh who will even notice wtfd do something else goddamn 
    private void ResetCharacterPositions()
    {
        int playerIndex = gameboard.PlayerIndex;


        foreach (Sprite2D child in GetChildren())
        {
            child.Position = new Vector2(-5000, 0);
        }

        int whoopsAllIndex = 0;
        for (int i = playerIndex + 2; i < GameManager.gameManager.players.Count() + playerIndex + 1; i++)
        {
            int trueIndex = i;
            if (i >= GameManager.gameManager.players.Count())
            {
                trueIndex -= GameManager.gameManager.players.Count();
            }

            GetChild<Sprite2D>(trueIndex).Position = new Vector2(50 + whoopsAllIndex * 200, 100);
            whoopsAllIndex += 1;





        }

    }


    

}
