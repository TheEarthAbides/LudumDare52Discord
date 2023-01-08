using UnityEngine;

namespace GnomeGame
{
  public class Colors
  {
    private const float maxF = 255f;

    // public static readonly Color gnomeGreen = new Color(itof(113), itof(236), itof(27));

    public static readonly Color gnomeFadeOutPurple = new Color(itof(77), itof(87), itof(146), 0.8f);

    private static float itof(int i) { return i / maxF; }
  }
}