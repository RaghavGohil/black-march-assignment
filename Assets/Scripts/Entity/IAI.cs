using Game.Tile;
using System.Collections;
using System.Collections.Generic;

namespace Game.AI 
{
    public interface IAI 
    {
        public IEnumerator Move(Tile.Tile tile, List<Tile.Tile> moves);
    }
}