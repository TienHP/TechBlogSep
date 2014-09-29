using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TNg_Framework
{
		[CustomEditor (typeof(Terrain2D))]
		public class Terrain2DEditor : Editor
		{

				private Terrain2DSceneEditor mSceneEditor;

	#region Initialization

				public Terrain2DEditor ()
				{
						mSceneEditor = new Terrain2DSceneEditor (this);
				}//end constructor

	#endregion

	#region Properties

				public Terrain2D Terrain { get { return this.target as Terrain2D; } }

	#endregion

	#region Events

				//let editor hadnle event in the scene view
				void OnSceneGUI ()
				{

						mSceneEditor.OnSceneGUI ();
						bool mouseEvent = Event.current.type == EventType.MouseMove || Event.current.type == EventType.MouseDrag;

						if (mouseEvent) {
								HandleUtility.Repaint ();

						}//end if

				}//end method


				/// <summary>
				/// 0 is Nodes
				/// 1 is Brush
				/// 2 is Filling
				/// 3 is Textures
				/// 4 is Grass
				/// </summary>
				int selected = -1;

				//imnplement custom inspector
				public override void OnInspectorGUI ()
				{
						//Debug.Log("call with time: ");
						GUILayout.Label ("This is a label in a custom editor");
						if (EditorStringStore.EDITOR_TOOLS != null && EditorStringStore.EDITOR_TOOLS.Length > 0) {
								GUILayout.BeginHorizontal ();
								string[] toolOptions = EditorStringStore.EDITOR_TOOLS;
								selected = GUILayout.Toolbar (selected, toolOptions, GUILayout.MinWidth (0));
								GUILayout.EndHorizontal ();
						}//end if

						//draw tool info
						DrawToolInfo ();

						switch (selected) {
						case 0:
								DrawNodesTool ();
								break;
						case 1:
								DrawBrushTool ();
								break;
						case 2:
								DrawFillingTool ();
								break;
						case 3:
								DrawTexturesTool ();
								break;
						case 4:
								DrawGrassTool ();
								break;
						}//end switch

						EditorGUILayout.Separator ();
						EditorGUIUtility.LookLikeControls ();
				}//end method
	
	#endregion


	#region Tools

				private void DrawToolInfo ()
				{
						if (selected == -1) {
								EditorGUILayout.BeginVertical ();
								GUILayout.Label (EditorStringStore.NO_TOOL_SELECTED_STRING);
								EditorGUILayout.EndVertical ();
						}//end if
		else {
								EditorGUILayout.BeginVertical ();
								GUILayout.Label (EditorStringStore.EDITOR_TOOL_INFOS [selected]);
								EditorGUILayout.EndVertical ();
						}//end else
				}//end method

				private void DrawNodesTool ()
				{
						EditorGUILayout.BeginHorizontal ();

						if (GUILayout.Button (EditorStringStore.LABEL_RESET_TERRAIN, GUILayout.ExpandWidth (false))) {
								Terrain.Reset ();
						}//end if

						if (GUILayout.Button (EditorStringStore.LABEL_REBUILD_DATA, GUILayout.ExpandWidth (false))) {
								Terrain.FixCurve ();
								Terrain.FixBoundary (); 
						}//end if

				}//end method

				private void DrawBrushTool ()
				{

				}//end method

				private void DrawFillingTool ()
				{

				}//end method

				private void DrawTexturesTool ()
				{

				}//end method

				private void DrawGrassTool ()
				{

				}//end method

	#endregion
		}//end class
}//end namespace