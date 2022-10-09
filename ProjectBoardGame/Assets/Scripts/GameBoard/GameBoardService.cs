using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardService : GenericSingleton<GameBoardService>
{
    private GameBoardContext m_currentGameBoard;

    private void GenerateGameBoard()
    {
        m_currentGameBoard = new GameBoardContext();
    }


}
