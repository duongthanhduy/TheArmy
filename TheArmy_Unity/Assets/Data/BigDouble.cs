using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

[Serializable]
public struct BigDouble : IFormattable, IComparable, IComparable<BigDouble>, IEquatable<BigDouble>
{
    public int CompareTo(object obj)
    {
        throw new NotImplementedException();
    }

    public int CompareTo(BigDouble other)
    {
        throw new NotImplementedException();
    }

    public bool Equals(BigDouble other)
    {
        throw new NotImplementedException();
    }

    public string ToString(string format, IFormatProvider formatProvider)
    {
        throw new NotImplementedException();
    }
}
