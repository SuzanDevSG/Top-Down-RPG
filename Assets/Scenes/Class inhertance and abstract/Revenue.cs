using System.Runtime.InteropServices;
using UnityEngine;

public class Revenue : Books
{
    protected override void Calculate()
    {
        income = price * sales;
    }
}
