using System;

[Serializable]
public class PolynomialData
{
	public float Base;

	public float GrowthRate;

	public float Exponent;

	public int Level;

	public BigDouble Value;

	public PolynomialOption PolynomialOption;

	public bool IsInteger;
}
