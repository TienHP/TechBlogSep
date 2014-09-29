using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TNg_Framework
{
		public class T2DTerrainCurveMesh : T2DTerrainMesh
		{

	#region Properties
				//control textures for shaders. 
				public List<Texture2D> controlTextures;
				//verticles of the stripe mesh
				public List<Vector3> stripeVerticles;

				public T2DTerrainCurveMesh (Terrain2D terrain) : base (terrain)
				{
						controlTextures = new List<Texture2D> ();
						stripeVerticles = new List<Vector3> ();
				}//end ctor

				public MeshRenderer renderer {
						get {
								EnsureMeshComponentsExist ();
								return transform.FindChild (EditorConstantStore.FILL_MESH_NAME).GetComponent<MeshRenderer> ();
						}
				}//end prop

	#endregion

	#region Material

				public void RebuildMaterial ()
				{
						EnsureMeshComponentsExist ();
						MeshRenderer renderer = transform.FindChild (EditorConstantStore.CURVE_MESH_NAME).GetComponent<MeshRenderer> ();
						Material[] materials = renderer.sharedMaterials;
						if (materials != null) {
								foreach (Material mat in materials) {
										Object.DestroyImmediate (mat, true);
								}//end foreach
						}//end if

				}//end method

	#endregion

				/// <summary>
				/// ???
				/// </summary>
				/// <returns>The materials needed count.</returns>
				private int GetMaterialsNeededCount ()
				{
						int materialCount = curveTextures.Count / EditorConstantStore.NUM_TEXTURES_PER_STRIPE_SHADER;
						if (curveTextures.Count % EditorConstantStore.NUM_TEXTURES_PER_STRIPE_SHADER != 0) {
								materialCount++;
						}
						return materialCount;
				}//end method

				//update the control textures of the shaders
				public void UpdateControlTextures ()
				{
						UpdateControlTextures (false);
				}//end method

				public void UpdateControlTextures (bool forceRecreate)
				{
						while (controlTextures.Count > GetMaterialsNeededCount()) {

						}//end while
				}//end method

				public T2DCurveTexture GetDefaultCurveTexture ()
				{
						T2DCurveTexture result = new T2DCurveTexture (Resources.Load ("defaultCurveTexture", typeof(Texture)) as Texture);
						return result;
				}//end method

				private void EnsureTextureInited ()
				{
						if (curveTextures.Count == 0) {
								curveTextures.Clear ();
								curveTextures.Add (GetDefaultCurveTexture ());
								foreach (Texture texture in controlTextures)
										Object.DestroyImmediate (texture, true);
								controlTextures.Clear ();
						}//end if
				}//end method

				private Texture2D CreateControlTexture (Color color)
				{
						int size = GetControlTextureSize ();
						Texture2D texture = new Texture2D (size, 1, TextureFormat.ARGB32, false);
						texture.filterMode = FilterMode.Bilinear;
						texture.wrapMode = TextureWrapMode.Clamp;
						texture.anisoLevel = 1;

						Color[] colors = new Color[size];
						for (int i = 0; i < size; i++) {
								colors [i] = color;
						}//end for

						texture.SetPixels (colors);
						texture.Apply ();

						return texture;
				}//end method

				private int GetControlTextureSize ()
				{
						int textureSize = Mathf.NextPowerOfTwo (TerrainCurve.Count);
						if (textureSize == 0)
								textureSize = 1;
						return textureSize;
				}//end method

				protected void EnsureMeshComponentsExist ()
				{
						EnsureMeshObjectsExist ();

						GameObject meshObject;

						meshObject = transform.FindChild (EditorConstantStore.FILL_MESH_NAME).gameObject;
						EnsureMeshFilterExists (meshObject);
						EnsureMeshRendererExists (meshObject);
						EnsureScriptsAttached (meshObject);		
				
						meshObject = transform.FindChild (EditorConstantStore.CURVE_MESH_NAME).gameObject;
						EnsureMeshFilterExists (meshObject);
						EnsureMeshRendererExists (meshObject);
						EnsureScriptsAttached (meshObject);	

						meshObject = transform.FindChild (EditorConstantStore.GRASS_MESH_NAME).gameObject;
						EnsureMeshFilterExists (meshObject);
						EnsureMeshRendererExists (meshObject);
						EnsureScriptsAttached (meshObject);

						meshObject = transform.FindChild (EditorConstantStore.COLLIDER_MESH_NAME).gameObject;
						EnsureMeshFilterExists (meshObject);
						EnsureScriptsAttached (meshObject);
				}//end method

				protected void EnsureMeshObjectsExist ()
				{

						if (transform.FindChild (EditorConstantStore.FILL_MESH_NAME) == null) {
								GameObject go = new GameObject (EditorConstantStore.FILL_MESH_NAME);
								go.transform.parent = transform;
						}//end if

						if (transform.FindChild (EditorConstantStore.CURVE_MESH_NAME) == null) {
								GameObject go = new GameObject (EditorConstantStore.CURVE_MESH_NAME);
								go.transform.parent = transform;
						}//end if

						if (transform.FindChild (EditorConstantStore.GRASS_MESH_NAME) == null) {
								GameObject go = new GameObject (EditorConstantStore.GRASS_MESH_NAME);
								go.transform.parent = transform;
						}//end if

						if (transform.FindChild (EditorConstantStore.COLLIDER_MESH_NAME) == null) {
								GameObject go = new GameObject (EditorConstantStore.COLLIDER_MESH_NAME);
								go.transform.parent = transform;
						}//end if
				}//end method

				protected void EnsureMeshFilterExists (GameObject meshObject)
				{
						if (meshObject.GetComponent<MeshFilter> () == null) {
								Mesh mesh = new Mesh ();
								mesh.name = meshObject.name;
								meshObject.AddComponent<MeshFilter> ().mesh = mesh;
						}//end if
		else if (meshObject.GetComponent<MeshFilter> ().sharedMesh == null) {
								Mesh mesh = new Mesh ();
								mesh.name = meshObject.name;
								meshObject.GetComponent<MeshFilter> ().mesh = mesh;
						}//end else

				}//end method

				protected void EnsureMeshRendererExists (GameObject meshObject)
				{
						if (meshObject.GetComponent<MeshCollider> () == null) {

								Mesh mesh = new Mesh ();
								mesh.name = meshObject.name;
								meshObject.AddComponent<MeshCollider> ().sharedMesh = mesh;
						}//end if
		else if (meshObject.GetComponent<MeshCollider> ().sharedMesh) {
								Mesh mesh = new Mesh ();
								mesh.name = meshObject.name;
								meshObject.GetComponent<MeshCollider> ().sharedMesh = mesh;
						}//end else
				}//end method

				protected void EnsureMeshColliderExists (GameObject meshObject)
				{
						if (meshObject.GetComponent<MeshCollider> () == null) {
								Mesh mesh = new Mesh ();
								mesh.name = meshObject.name;
								meshObject.AddComponent<MeshCollider> ().sharedMesh = mesh;
						}//end if
		else if (meshObject.GetComponent<MeshCollider> ().sharedMesh == null) {
								Mesh mesh = new Mesh ();
								mesh.name = meshObject.name;
								meshObject.GetComponent<MeshCollider> ().sharedMesh = mesh;
						}//end else
				}//end method

				public void DeleteAllSubobjects ()
				{
						EnsureMeshObjectsExist (); 
				}//end method

				public void DestroyMesh ()
				{

				}
		}//end class
}//end namespace
