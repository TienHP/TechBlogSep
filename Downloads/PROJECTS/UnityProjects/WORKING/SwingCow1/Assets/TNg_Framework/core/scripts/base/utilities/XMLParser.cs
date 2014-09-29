using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Text;

namespace TNg_Framework
{
		public class XMLParser
		{

				public static void Parse (string filename)
				{
						Debug.Log ("hizz");
						var obj = LoadInstanceAsXml (filename, typeof(terrain2d)) as terrain2d;
						Debug.Log (obj.editor_tools [1].value);

//		var obj1 = new terrain2d();
//		obj1.editor_tools = new terrain2d.editor_tools_info[10];
//		obj1.editor_tools[0].value = "value 0";
//		obj1.editor_tools[1].value = "value 1";
//		obj1.editor_tools[2].value = "value 2";
//		obj1.editor_tools[3].value = "value 3";
//		obj1.editor_tools[4].value = "value 4";
//		obj1.editor_tools[5].value = "value 5";
//		obj1.editor_tools[6].value = "value 6";
//		obj1.editor_tools[7].value = "value 7";
//		obj1.editor_tools[8].value = "value 8";
//		obj1.editor_tools[9].value = "value 9";
//
//		SaveInstanceAsXml("/TNg_Framework/xmls/terrain2d_strings.xml", typeof(terrain2d), obj1);
				}//end method

				public static object LoadInstanceAsXml (string fileName, System.Type type)
				{
						var content = LoadXML (fileName);
						if (content != null && content.Length != 0) {
								return DeserializeObject (type, content);
						}//end if
						return null;
				}//end method


				public static void SaveInstanceAsXml (string fileName, System.Type type, object instance)
				{

						string xml = SerializeObject (type, instance);
						SaveXML (fileName, xml);
				}//end method


				private static void SaveXML (string fileName, string xml)
				{
						StreamWriter writer;
						FileInfo fileInfo = new FileInfo (Application.dataPath + fileName);

						if (!fileInfo.Exists) {
								writer = fileInfo.CreateText ();
						}//end if
		else {
								fileInfo.Delete ();
								writer = fileInfo.CreateText ();
						}//end else
						writer.Write (xml);
						writer.Close ();
				}//end method

				private static string LoadXML (string fileName)
				{
						string path = Application.dataPath + fileName;
		
						string content = File.ReadAllText (path);
		
						//Debug.Log(content);

						return content;
				}//end method 

				public static string SerializeObject (System.Type type, object instance)
				{

						string xmlizedString = null;
						MemoryStream memoryStream = new MemoryStream ();
						XmlSerializer xs = new XmlSerializer (type);
						XmlTextWriter xmlTextWriter = new XmlTextWriter (memoryStream, Encoding.UTF8);
						xs.Serialize (xmlTextWriter, instance);
						memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
						xmlizedString = UTF8ByteArrayToString (memoryStream.ToArray ());
						return xmlizedString;
				}//end method

				public static object DeserializeObject (System.Type type, string content)
				{
						XmlSerializer xs = new XmlSerializer (type);
						if (content != null && content.Length != 0) {
								MemoryStream memoryStream = new MemoryStream (StringToUTF8ByteArray (content));
								return xs.Deserialize (memoryStream);
						}//end if
						return null;
				}//end method


				private static byte[] StringToUTF8ByteArray (string pXmlString)
				{
						UTF8Encoding encoding = new UTF8Encoding ();
						byte[] byteArr = encoding.GetBytes (pXmlString);
						return byteArr;
				}//end method

				private static string UTF8ByteArrayToString (byte[] byteArr)
				{
						UTF8Encoding encoding = new UTF8Encoding ();
						string str = encoding.GetString (byteArr);
						return str;
				}//end method
		}//end class
}//end namespace
