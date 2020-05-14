using GameLib.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLib
{
    public interface IDeck
    {
        void Shuffle();
        List<Card> Deal(int cardCount);
    }
}
