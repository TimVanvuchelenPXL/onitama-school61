﻿using System.ComponentModel;
using Onitama.Core.MoveCardAggregate.Contracts;

namespace Onitama.Core.TableAggregate;

public class NewTablePreferences : TablePreferences
{
    /// <summary>
    /// Number of players that can take part in the game.
    /// The default value is 2.
    /// </summary>
    [DefaultValue(2)]
    public int NumberOfPlayers { get; set; } = 2;

    /// <summary>
    /// Size of the player mat (= number of rows or columns).
    /// The default value is 5.
    /// </summary>
    [DefaultValue(5)]
    public int PlayerMatSize { get; set; } = 5;

    /// <summary>
    /// Set of move cards to use.
    /// The default value is the original set of 16 cards.
    /// </summary>
    [DefaultValue(MoveCardSet.Original)]
    public MoveCardSet MoveCardSet { get; set; } = MoveCardSet.Original;


    /// <summary>
    /// Game type
    /// The default value is classic.
    /// </summary>
    [DefaultValue("competitive")]
    public string TableType { get; set; } = "competitive";
}