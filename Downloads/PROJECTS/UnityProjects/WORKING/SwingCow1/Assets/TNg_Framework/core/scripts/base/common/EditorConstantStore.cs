using UnityEngine;
using System.Collections;

/// <summary>
/// class hold all constant use in t2d editor tool
/// </summary>
/// 
namespace TNg_Framework
{
		public class EditorConstantStore
		{

	#region General Settings

				//to check whether surface is intercrossing or not
				public static readonly bool CHECK_CURVE_INTERCROSSING = false;
	
	#endregion

	#region Handles

	#endregion

	#region Terrain

				//width/depth of the collision mesh
				public static readonly float COLLISION_MESH_Z_DEPTH = 10.0f;
				//number of splat textures per shader. When more textures are used more instances of the shader are created
				public static readonly int NUM_TEXTURES_PER_STRIPE_SHADER = 4;

				//maximum number of grass textures(limited by Grass.shader)
				public static readonly int MAX_GRASS_TEXTURES = 4;
				//influences the desity of grass
				public static readonly float GRASS_DENSITY_RATIO = 5.0f;
				//influences the amount of scattering of grass bushes
				public static readonly float GRASS_SCATTER_RATIO = 1.0f;
				//amount of the V coordinate to be considered as roots of the grass
				public static readonly float GRASS_ROOT_SIZE = .1f;

				//Names of the sub-object of the main game object holding the fill mesh data
				public static readonly string FILL_MESH_NAME = "_fill";
				//Names of the sub-object of the main game object holding the curve mesh data
				public static readonly string CURVE_MESH_NAME = "_curve";
				//Names of the sub-object of the main game object holding the grass mesh data
				public static readonly string GRASS_MESH_NAME = "_grass";
				//Names of the sub-object of the main game object holding the collider mesh data
				public static readonly string COLLIDER_MESH_NAME = "_collider";

				//initial size of the fill texture
				public static readonly float INIT_FILL_TEXTURE_WIDTH = 1;
				//initial size of the fill texture
				public static readonly float INIT_FILL_TEXTURE_HEIGHT = 1;
				//initial offset of the fill texture
				public static readonly float INIT_FILL_TEXTURE_OFFSEX_X = 0.0f;
				//initial offset of the fill texture
				public static readonly float INIT_FILL_TEXTURE_OFFSET_Y = 0.0f;
				//check wheter the curve is close or not
				public static readonly bool IS_CLOSED_CURVE = false;


	#endregion

	#region Terrain Scene

				//size of the gizmos in the upper right corner of scene view
				public static readonly float SCENE_GIZMOS_SIZE = 78.0f;
				//color of the curve
				public static readonly Color COLOR_CURVE_NORMAL = new Color (0.8f, 0.0f, 0.0f);
				//Color of the selected part of the curve
				public static readonly Color COLOR_CURVE_BRUSH_LINE = new Color (0.0f, 0.3f, 1.0f, 0.3f);
				//Color of the spheres displayed at the points of the curve
				public static readonly Color COLOR_CURVE_BRUSH_SPHERE = new Color (0.0f, 0.0f, 1.0f, 0.3f);
				//Color of the terrain boundary rectangle
				public static readonly Color COLOR_BOUNDARY_RECT = new Color (0.0f, 0.5f, 0.5f);
				//Color of the nodes cursor
				public static readonly Color COLOR_NODE_CURSOR = new Color (0.0f, 0.0f, 1.0f, 0.5f);
				//Color of the brush arrow
				public static readonly Color COLOR_BRUSH_ARROW = new Color (1.0f, 0.0f, 0.0f, 0.8f);

				//size of the curve brush lines
				public static readonly float SCALE_CURVE_BRUSH_LINE = 0.1f;
				//size of the curve brush spheres
				public static readonly float SCALE_CURVE_BRUSH_SPHERES = 0.2f;
				//scale of the handles of the terrain nodes
				public static readonly float SCALE_NODE_HANDLES = 2.5f;
				//size of the cursor while editting nodes
				public static readonly float SCALE_NODES_CURSOR = 0.2f;
				//size of the cursor while editting nodes
				public static readonly float SCALE_NODES_CURVE_SPHERE = 0.1f;
				//size of the arrow displaying the direction of the brush
				public static readonly float SCALE_BRUSH_ARROW = 0.2f;

				//strength of the height brush
				public static readonly float BRUSH_HEIGHT_RATIO = 0.05f;
				//strength of the grassh brush
				public static readonly float BRUSH_GRASS_RATIO = 0.001f;
				//how fast the size of the bursh changes when the mouse moves
				public static readonly float BRUSH_SIZE_INC_RATIO = 40.0f;
				//how fast the angle of the brush changes when the mouse moves
				public static readonly float BRUSH_ANGLE_INC_RATIO = 150.0f;
				//how fast the selected texture changes when the mouse moves
				public static readonly float BRUSH_OPACITY_INC_RATIO = 2.0f;
				//distance the mouse must travel for the brush to be applied again
				public static readonly float BRUSH_APPLY_STEP_RATIO = 0.3f;

				//minimum size of the brush
				public static readonly int BRUSH_SIZE_MIN = 1;
				//maximum size of the brush
				public static readonly int BRUSH_SIZE_MAX = 50;
				//initial size of the brush
				public static readonly int INIT_BRUSH_SIZE = 15;
				//minimum opacity of the brush
				public static readonly int BRUSH_OPACITY_MIN = 0;
				//maximum opacity of the brush
				public static readonly int BRUSH_OPACITY_MAX = 100;
				//initial opacity of the brush
				public static readonly int INIT_BRUSH_OPACITY = 50;
				//Initial angle of the brush
				public static readonly float INIT_BRUSH_ANGLE = 0;
	#endregion

	#region Terrain Inspector

				//minimum size of a tile of the fill texture
				public static readonly float FILL_TEXTURE_SIZE_MIN = float.Epsilon;
				//maxiimum size of a tile of the fill texture
				public static readonly float FILL_TEXTURE_SIZE_MAX = 20.0f;
				//maximum offset of a tile of the fill texture
				public static readonly float FILL_TEXTURE_OFFSET_MAX = 20.0f;

				//minimum width of a texture
				public static readonly float TEXTURE_WIDTH_MIN = 0.1f;
				//maximum width of a texture
				public static readonly float TEXTURE_WIDTH_MAX = 10.0f;
				//minimum height of a texture
				public static readonly float TEXTURE_HEIGHT_MIN = 0.1f;
				//maximum height of a texture
				public static readonly float TEXTURE_HEIGHT_MAX = 5.0f;

				//initial random grass scatter ratio
				public static readonly float INIT_GRASS_SCATTER_RATIO = 0.0f;
				//minimum random grass scatter ratio
				public static readonly float GRASS_SCATTER_RATIO_MIN = 0.0f;
				//maximum random grass scatter ratio
				public static readonly float GRASS_SCATTER_RATIO_MAX = 1.0f;
				//initial speed of grass waving
				public static readonly float INIT_GRASS_WAVE_SPEED = 1.0f;
				//minimum speed of grasss waving
				public static readonly float GRASS_WAVE_SPEED_MIN = .1f;
				//maximum speed of grass waving
				public static readonly float GRASS_WAVE_SPEED_MAX = 2.0f;
				//minimum size of grass
				public static readonly float GRASS_SIZE_MIN = .1f;
				//maximum size of grass
				public static readonly float GRASS_SIZE_MAX = 10.0f;
				//minimum size randomness of grass
				public static readonly float GRASS_SIZE_RANDOMNESS_MIN = .0f;
				//maximum size randomness of grass
				public static readonly float GRASS_SIZE_RANDOMNESS_MAX = 10.0f;
				//minimum wave amplitude of grass
				public static readonly float GRASS_WAVE_AMPLITUDE_MIN = 0.0f;
				//maximum wave amplitude of grass
				public static readonly float GRASS_WAVE_AMPLITUDE_MAX = 1.0f;

				//size of the control texture displayed in the inspector
				public static readonly float CONTROL_TEXTURE_SIZE = 50.0f;


	#endregion

		}//end class
}//end namespace
