using System;
using System.Collections.Generic;
using System.Linq;

// 1. Interfejs IModular (z treści zadania)
public interface IModular
{
    public double Module();
}

// 1. Klasa ComplexNumber z ICloneable, IEquatable, IModular, IComparable
public class ComplexNumber : ICloneable, IEquatable<ComplexNumber>, IModular,
                             IComparable<ComplexNumber>, IComparable
{
    private double re;
    private double im;

    public double Re { get => re; set => re = value; }
    public double Im { get => im; set => im = value; }

    public ComplexNumber(double re, double im)
    {
        this.re = re; this.im = im;
    }

    public override string ToString()
    {
        string sign = im >= 0 ? "+" : "-";
        return $"{re} {sign} {Math.Abs(im)}i";
    }

    public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
        => new ComplexNumber(a.re + b.re, a.im + b.im);

    public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
        => new ComplexNumber(a.re - b.re, a.im - b.im);

    public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
        => new ComplexNumber(a.re * b.re - a.im * b.im, a.re * b.im + a.im * b.re);

    public static ComplexNumber operator -(ComplexNumber a)
        => new ComplexNumber(a.re, -a.im);

    public object Clone() => new ComplexNumber(re, im);

    public bool Equals(ComplexNumber other)
    {
        if (other == null) return false;
        return re == other.re && im == other.im;
    }

    public override bool Equals(object obj)
        => obj is ComplexNumber other && Equals(other);

    public override int GetHashCode()
        => HashCode.Combine(re, im);

    public static bool operator ==(ComplexNumber a, ComplexNumber b)
        => a?.Equals(b) ?? b is null;

    public static bool operator !=(ComplexNumber a, ComplexNumber b)
        => !(a == b);

    public double Module()
        => Math.Sqrt(re * re + im * im);

    // *** IComparable<ComplexNumber> – porównanie po module ***
    public int CompareTo(ComplexNumber other)
    {
        if (other == null) return 1;
        return this.Module().CompareTo(other.Module());
    }

    // *** IComparable (niemetryczny, „stary” interfejs) ***
    int IComparable.CompareTo(object obj)
    {
        if (obj == null) return 1;
        if (obj is ComplexNumber other)
            return CompareTo(other);
        throw new ArgumentException("Object is not a ComplexNumber");
    }
}
