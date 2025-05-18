using System.Collections.Generic;

public class Data
{
    public bool[] IsPurchased = new bool[30];
    public bool[] IsWin = new bool[30];
    public List<int> Gallery;

    public Data()
    {
        IsPurchased[0] = true;
        Gallery = new();
    }
}