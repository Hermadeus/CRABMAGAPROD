using UnityEngine;
using System;

public class InterfaceTypeAttribute : PropertyAttribute
{
	public Type type;

	public InterfaceTypeAttribute(Type type)
	{
		this.type = type;
	}
}
