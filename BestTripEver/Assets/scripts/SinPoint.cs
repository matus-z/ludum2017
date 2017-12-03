using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ----------------------------------------------------------------
public class SinPoint
{
  public int X = 0;
  public int Y = 0;

  public ESin sin = ESin.Lust;

  // ----------------------------------------------------------------
  public SinPoint(int x, int y, ESin s)
  {
    X = x;
    Y = y;
    sin = s;
  }
}
