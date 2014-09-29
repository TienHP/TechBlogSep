using UnityEngine;
using System.Collections;
using UnityEditor;

namespace TNg_Framework
{
		public class Terrain2DSceneEditor
		{

				//plane to detect mouse cursor position
				private static Plane sXyPlane;

				//inspector editor
				private Terrain2DEditor mInspector;

				//current world position of the cursor
				private Vector3 mCursorPos;

				//position of the cursor when a tool was actived
				private Vector3 mInitCursorPos;

				//store drag state
				private bool mMouseDragged;

				//store press state
				private bool mMousePressed;

				//store shift press state
				private bool mShiftPressed;

				//store ctr press state
				private bool mControlPressed;

				//store shift and ctr press state
				private bool mShiftAndControlPressed;

				//initial value of the brush size before a a tool was acticved
				private int mInitBrushSize;

				//initial value of the brush opacity before a tool was actived
				private int mInitBrushOpacity;

				//initial value of the brush angle before a tool was actived
				private float mInitBrushAngle;

				//initial index of the selected textures to choice
				private int mInitTextureIndex;

				//last position brush was applied to
				private Vector3 mLastAppliedPosition;

				//will terrain be rebuild when mouse is released
				private bool mRebuildDataOnMouseUp;

				//size of the current brush in the world units
				private static int sBrushSize = EditorConstantStore.INIT_BRUSH_SIZE;
				//defines how hard will be the brush applied
				private static int sBrushOpacity = EditorConstantStore.INIT_BRUSH_OPACITY;
				//defines the rotation of the brush( if needed)
				private static float sBrushAngle = EditorConstantStore.INIT_BRUSH_ANGLE;

				private Terrain2D Terrain {
						get {
								return (Terrain2D)mInspector.target;
						}
				}//end prop


				public Terrain2DSceneEditor (Terrain2DEditor inspector)
				{
						mInspector = inspector;
						mCursorPos = Vector3.zero;
						mMouseDragged = false;
						mShiftPressed = false;
						mControlPressed = false;
						mShiftAndControlPressed = false;
						mRebuildDataOnMouseUp = false; 
				}//end constructor

				//draw on scene editor
				public void OnSceneGUI ()
				{

						//create scene gui on editor
						//GUILayout.Label("Create on scene");
						if (!PrepareSceneView ())
								return;

				}//end method

				//make sure the scene view ready for the GUI of the editor. Returns false if something went wrong and the GUI
				//can't be drawn
				private bool PrepareSceneView ()
				{
						Terrain.EditorReference = mInspector;

						//disable wireframe mode
						EditorUtility.SetSelectedWireframeHidden (Terrain.curveMesh.renderer, !T2DUtils.DEBUG_SHOW_WIREFRAME);
						EditorUtility.SetSelectedWireframeHidden (Terrain.grassMesh.renderer, !T2DUtils.DEBUG_SHOW_WIREFRAME);
						EditorUtility.SetSelectedWireframeHidden (Terrain.curveMesh.renderer, !T2DUtils.DEBUG_SHOW_WIREFRAME);

						//checks
						if (T2DEditorUtils.IsAnySceneToolSelected ()) {

						}
		
						return false;
				}//end method

				//draws the curve polygonal chain.
				private void DrawCurve ()
				{

						if (Terrain.terrainCurve.Count < 2)
								return;
						Handles.color = new Color (EditorConstantStore.COLOR_CURVE_NORMAL.r, EditorConstantStore.COLOR_CURVE_NORMAL.g, EditorConstantStore.COLOR_CURVE_NORMAL.b, 1);

						//curve outline
						Vector2 lastPoint = Vector2.zero;
						bool first = true;
						foreach (var node in Terrain.terrainCurve) {
								if (first) {
										first = false;
								}//end if
			else {
								}//end else
						}//end foreach

				}//end method

		}//end class
}//end namespace
