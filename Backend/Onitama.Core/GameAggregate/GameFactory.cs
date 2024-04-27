﻿using System.Diagnostics;
using System.Drawing;
using Onitama.Core.GameAggregate.Contracts;
using Onitama.Core.MoveCardAggregate.Contracts;
using Onitama.Core.PlayerAggregate.Contracts;
using Onitama.Core.PlayMatAggregate;
using Onitama.Core.PlayMatAggregate.Contracts;
using Onitama.Core.SchoolAggregate;
using Onitama.Core.SchoolAggregate.Contracts;
using Onitama.Core.TableAggregate.Contracts;
using Onitama.Core.Util;
using static Onitama.Core.TableAggregate.Table;

namespace Onitama.Core.GameAggregate;

internal class GameFactory : IGameFactory
{
    private IMoveCardRepository _moveCardRepository;
    private static Random _random = new Random();
    public GameFactory(IMoveCardRepository moveCardRepository)
    {
       this._moveCardRepository = moveCardRepository;
    }

    public IGame CreateNewForTable(ITable table)
    {
        Color[] colors = table.SeatedPlayers.Select(p => p.Color).ToArray();
        
        IMoveCard[] moveCards;
        moveCards = _moveCardRepository.LoadSet(table.Preferences.MoveCardSet, colors);
        moveCards = moveCards.OrderBy(card => _random.Next()).ToArray();
        //throw new Exception(table.Preferences.MoveCardSet.ToString());
        var playMat = new PlayMat(5);
        table.SeatedPlayers[0].MoveCards.Add(moveCards[0]);
        table.SeatedPlayers[0].MoveCards.Add(moveCards[1]);
        table.SeatedPlayers[1].MoveCards.Add(moveCards[2]);
        table.SeatedPlayers[1].MoveCards.Add(moveCards[3]);
        var players = new IPlayer[table.SeatedPlayers.Count];
        table.SeatedPlayers.CopyTo(players, 0);
        Game game = new Game(Guid.NewGuid(), playMat, players, moveCards.ElementAt(moveCards.Length - 1));
        foreach (var player in players)
        {
            player.School = new School();
            player.School.AllPawns = new Pawn[5];
            player.School.Students = new Pawn[4];
            player.School.AllPawns[0] = new Pawn(Guid.NewGuid(), player.Id, SchoolAggregate.Contracts.PawnType.Student);
            player.School.AllPawns[1] = new Pawn(Guid.NewGuid(), player.Id, SchoolAggregate.Contracts.PawnType.Student);
            player.School.AllPawns[2] = new Pawn(Guid.NewGuid(), player.Id, SchoolAggregate.Contracts.PawnType.Master);
            player.School.AllPawns[3] = new Pawn(Guid.NewGuid(), player.Id, SchoolAggregate.Contracts.PawnType.Student);
            player.School.AllPawns[4] = new Pawn(Guid.NewGuid(), player.Id, SchoolAggregate.Contracts.PawnType.Student);
            player.School.Master = player.School.AllPawns[2];
            player.School.Students[0] = player.School.AllPawns[0];
            player.School.Students[1] = player.School.AllPawns[1];
            player.School.Students[2] = player.School.AllPawns[2];
            player.School.Students[3] = player.School.AllPawns[3];

            game.PlayMat.PositionSchoolOfPlayer(player);
        }
        
        return game;
    }
}