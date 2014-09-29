using System;

[AttributeUsage(AttributeTargets.Class)]
public sealed class InfoAttribute : Attribute
{
	public string category;
	public string description;
	public string url;
}

public static class InfoAttributeExtensions{
#if UNITY_EDITOR
	public static string GetCategory(this Type type){
		object[] attributes=type.GetCustomAttributes(true);
		foreach(object attribute in attributes){
			if(attribute is InfoAttribute){
				return (attribute as InfoAttribute).category;
			}
		}
		return string.Empty;
	}

	public static string GetDescription(this Type type){
		object[] attributes=type.GetCustomAttributes(true);
		foreach(object attribute in attributes){
			if(attribute is InfoAttribute){
				return (attribute as InfoAttribute).description;
			}
		}
		return string.Empty;
	}

	public static string GetInfoUrl(this Type type){
		object[] attributes=type.GetCustomAttributes(true);
		foreach(object attribute in attributes){
			if(attribute is InfoAttribute){
				return (attribute as InfoAttribute).url;
			}
		}
		return string.Empty;
	}
#endif
}