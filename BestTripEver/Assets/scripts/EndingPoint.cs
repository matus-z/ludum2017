using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ----------------------------------------------------------------
public class EndingPoint
{
    public int X = 0;
    public int Y = 0;

    public int mapInfoIndex = 0;

    // ----------------------------------------------------------------
    public EndingPoint(int x, int y, int m)
    {
        X = x;
        Y = y;
        mapInfoIndex = m;
    }
}
