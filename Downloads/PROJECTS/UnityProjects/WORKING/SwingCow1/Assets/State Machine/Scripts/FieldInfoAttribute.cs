using System;
using System.Reflection;

[AttributeUsage(AttributeTargets.Field)]
public sealed class FieldInfoAttribute : Attribute
{
	public bool bindedCanBeConstant=true;
	public bool requiredField = true;
	public bool canBeConstant = true;
	public string nullLabel = "None";
	public string tooltip = string.Empty;
	public object defaultValue= null;
	public string dirtyField=string.Empty;
}

public static class FieldInfoExtension{
#if UNITY_EDITOR
	public static string GetDirty(this FieldInfo info){
		object[] attributes=info.GetCustomAttributes(true);
		foreach(object attribute in attributes){
			if(attribute is FieldInfoAttribute){
				return (attribute as FieldInfoAttribute).dirtyField;
			}
		}
		return string.Empty;
	}

	public static string GetToolTip(this FieldInfo info){
		object[] attributes=info.GetCustomAttributes(true);
		foreach(object attribute in attributes){
			if(attribute is FieldInfoAttribute){
				return (attribute as FieldInfoAttribute).tooltip;
			}
		}
		return string.Empty;
	}

	public static object GetDefaultValue(this FieldInfo info){
		object[] attributes=info.GetCustomAttributes(true);
		foreach(object attribute in attributes){
			if(attribute is FieldInfoAttribute){
				return (attribute as FieldInfoAttribute).defaultValue;
			}
		}
		return null;
	}

	public static string GetNullLabel(this FieldInfo info){
		object[] attributes=info.GetCustomAttributes(true);
		foreach(object attribute in attributes){
			if(attribute is FieldInfoAttribute){
				return (attribute as FieldInfoAttribute).nullLabel;
			}
		}
		return string.Empty;
	}

	public static bool IsFieldRequired(this FieldInfo info){
		object[] attributes=info.GetCustomAttributes(true);
		foreach(object attribute in attributes){
			if(attribute is FieldInfoAttribute){
				return (attribute as FieldInfoAttribute).requiredField;
			}
		}
		return false;
	}

	public static bool CanBeConstant(this FieldInfo info){
		object[] attributes=info.GetCustomAttributes(true);
		foreach(object attribute in attributes){
			if(attribute is FieldInfoAttribute){
				return (attribute as FieldInfoAttribute).canBeConstant;
			}
		}
		return true;
	}

	public static bool BindedCanBeConstant(this FieldInfo info){
		object[] attributes=info.GetCustomAttributes(true);
		foreach(object attribute in attributes){
			if(attribute is FieldInfoAttribute){
				return (attribute as FieldInfoAttribute).bindedCanBeConstant;
			}
		}
		return true;
	}
#endif
}