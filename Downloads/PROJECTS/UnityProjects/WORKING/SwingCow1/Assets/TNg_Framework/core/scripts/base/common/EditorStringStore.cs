using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TNg_Framework
{
		public class EditorStringStore : TNg_Singleton<EditorStringStore>
		{

				//use XML 

				private Dictionary<string, string> stringDict = new Dictionary<string, string> ();

				void Awake ()
				{
						Initialize (); 
				}//end method
	
				protected override void Initialize ()
				{
				}//end method

				public void FillTheDict ()
				{
						var t2d = XMLParser.LoadInstanceAsXml ("/TNg_Framework/xmls/terrain2d_strings.xml", typeof(terrain2d)) as terrain2d;	

						foreach (var editor_tool in t2d.editor_tools) {
								stringDict.Add (editor_tool.id, editor_tool.value);
						}//end foreach

				}//end method

				public Dictionary<string, string> GetStringDict ()
				{
						return stringDict;
				}//end method

				public List<string> GetFilterList (string filterString)
				{
						var l = from entry in stringDict where (entry.Key.Contains (filterString)) select entry.Value;
						return new List<string> (l);
				}//end method

				public string GetStringById (string stringid)
				{
						if (stringDict.ContainsKey (stringid)) {
								return stringDict [stringid];
						}
						return null;
				}//end method

				void OnDestroy ()
				{
						t2dDelegate.onInitEditorEvt -= FillTheDict;
				}///end method


				//or get it directly
				public static readonly string[] EDITOR_TOOLS = {
		"Nodes",
		"Brush",
		"Filling",
		"Textures",
		"Grass"
	};

				//details about editor tools
				public static readonly string[] EDITOR_TOOL_INFOS = {
		"Adjust nodes. Use to create and manipulate nodes",
		"Adjust brush. Change height of terrain",
		"Adjust filling. Pull the terrain",
		"Adjust textures. Draw textures on terrain",
		"Adjust grass. Place grass on the ground"
	};

				//no tool selected
				public const string NO_TOOL_SELECTED_STRING = "No tools selected.\nPress N to select Nodes tool" +
						"\nPress B to select Brush tool" +
						"\nPress F to select Filling tool" +
						"\nPress T to select Textures tool" +
						"\nPress G to select Grass tool";
				public const string LABEL_RESET_TERRAIN = "Reset Terrain";
				public const string LABEL_REBUILD_DATA = "Rebuild Data";

		}//end class
}//end namespace
