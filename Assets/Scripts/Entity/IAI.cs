using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.AI 
{
    public interface IAI 
    {
        public IEnumerator MoveAI(List<Tile.Tile> moves);
        public void HandleNoMovesLeft();
    }
}