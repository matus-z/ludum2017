using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ----------------------------------------------------------------
public class EndingPoint
{
    public int mapInfoIndex = 0;

    public PositionOnGrid Pos;

    // ----------------------------------------------------------------
    public EndingPoint(int x, int y, int m)
    {
        Pos = new PositionOnGrid(x, y);
        mapInfoIndex = m;
    }
}
