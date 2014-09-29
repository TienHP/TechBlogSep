using UnityEngine;
using System.Collections;

public static class T2DUtils {

	private static bool DEBUG = false;

	//rebuilds the runtime datat each time the component is reloaded (action start/stop and script reload)
	public static bool DEBUG_REBUILD_ON_ENABLE { get { return DEBUG && true; } }
	//displays debug information in the inspector view of the components
	public static bool DEBUG_INSPECTOR { get {return DEBUG && false; } }
	//displays a curve producced by the generator last time it was executed
	public static bool DEBUG_GENERATOR_CURVE { get { return DEBUG && false; } }
	//displays control textures in the terrain component inspector
	public static bool DEBUG_CONTROL_TEXTURES { get { return DEBUG && false; } }
	//displays cursor related information at the current cursor position.
	public static bool DEBUG_CURSOR_INFO { get {return DEBUG && false; } }
	//displays points defining the curve stripe
	public static bool DEBUG_STRIPE_POINTS { get {return DEBUG && false; } }
	//displays points projected from the curve stripe
	public static bool DEBUG_SHOW_SUBOBJECTS { get {return DEBUG && false; } }
	//displays pints projected from the curve endpoints to the boundary
	public static bool DEBUG_BOUNDARY_PROJECTIONS { get {return DEBUG && false; } }
	//display wireframes or dont
	public static bool DEBUG_SHOW_WIREFRAME { get {return DEBUG && false; } }

	//return true if segment (a, b) intersect segment (c, d)
	public static bool SegmentsIntersect(Vector2 a, Vector2 b, Vector2 c, Vector2 d){
		Vector2 intersection;
		return SegmentsIntersect(a, b, c, d, out intersection);
	}//end method

	public static bool SegmentsIntersect(Vector2 a, Vector2 b, Vector2 c, Vector2 d, out Vector2 intersection){

		intersection = Vector2.zero;

		float cross = (a.x - b.x) * (c.y - d.y) - (a.y - b.y) * (c.x - d.x);
		if (Mathf.Abs(cross) < Mathf.Epsilon){
			return false; //near parallel
		}//end if

		//???
		//prevent from imprecision errors
		float delta1 = 0;
		if (Mathf.Abs(a.x - b.x) <= float.Epsilon || Mathf.Abs(a.y - b.y) <= float.Epsilon){
			delta1 = 0.01f;
		}//end if
		if (intersection.x < Mathf.Min (a.x, b.x) - delta1 || intersection.x > Mathf.Max(a.x, b.x) + delta1){
			return false;
		}//end if
		if (intersection.y < Mathf.Min (a.y, b.y) - delta1 || intersection.y > Mathf.Max(a.y, b.y) + delta1){
			return false;
		}//end if
		float delta2 = 0.0f;
		if (Mathf.Abs(c.x - d.x) <= float.Epsilon || Mathf.Abs(c.y - d.y) <= float.Epsilon){
			delta2 = 0.01f;
		}//end if
		if (intersection.x < Mathf.Min (c.x, d.x) - delta2 || intersection.x > Mathf.Max (c.x, d.x) + delta2){
			return false;
		}//end if
		if (intersection.y < Mathf.Min(c.y, d.y) - delta2 || intersection.y > Mathf.Max(c.y, d.y) + delta2){
			return false;
		}//end if

		return true;
	}//end method


}//end class
