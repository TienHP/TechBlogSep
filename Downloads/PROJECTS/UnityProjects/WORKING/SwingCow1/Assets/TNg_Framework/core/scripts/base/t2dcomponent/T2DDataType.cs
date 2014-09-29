using UnityEngine;
using System.Collections;

[System.Serializable]
public class T2DCurveNode
{

		//position in the local space of terrain
		public Vector2 position;
		//index into the array of the curve textures 
		public int texture;
		//amount of grass at this node [0, 1]
		public float grassRatio;

		public T2DCurveNode (Vector2 _position)
		{
				position = _position;
				texture = 0;
				grassRatio = 0;		
		}//end ctor

		//copies data from another node
		public void Copy (T2DCurveNode other)
		{
				position = other.position;
				texture = other.texture;
				grassRatio = other.grassRatio;
		}//end method

		public override bool Equals (object obj)
		{
				if (!(obj is T2DCurveNode))
						return false;
				return this == (T2DCurveNode)obj;
		}//end method

}//end class

[System.Serializable]
public class T2DCurveTexture
{

		//texture data
		public Texture texture;
		//size of the local game object space
		public Vector2 size;
		//whether texture is aligned to the surface curve or not
		public bool fixedAngle;
		//fade amount when switch textures
		public float fadeThreshold;


		public T2DCurveTexture (Texture _texture)
		{
				texture = _texture;
				size = new Vector2 (1, 1);
				fixedAngle = false;
				fadeThreshold = 0.3f;
		}//end ctor

		public T2DCurveTexture (T2DCurveTexture other)
		{
				texture = other.texture;
				size = other.size;
				fixedAngle = other.fixedAngle;
				fadeThreshold = other.fadeThreshold;
		}//end ctor

}//end class

[System.Serializable]
public class T2DGrassTexture
{

		//texture data
		public Texture texture;
		//size in world coordinates
		public Vector2 size;
		//randomness of the size in world units
		public Vector2 sizeRandomness;
		//influences how much the grass waves
		public float waveAmplitude;

		public T2DGrassTexture (Texture _texture)
		{
				texture = _texture;
				size = new Vector2 (1, 1);
				sizeRandomness = new Vector2 (.5f, .5f); 
				waveAmplitude = .5f;

		}//end ctor

		public T2DGrassTexture (T2DGrassTexture other)
		{
				texture = other.texture;
				size = other.size;
				sizeRandomness = other.sizeRandomness;
				waveAmplitude = other.waveAmplitude;
		}//end ctor	
		
}//end class

//de fines a peak the user wishes to have in the generated terrain.
[System.Serializable]
public class T2DGeneratorPeak
{
		//position of the peak in local coordinates of the game objects
		public Vector2 position;
		
		public T2DGeneratorPeak (Vector2 _position)
		{	
				position = _position;
		}//end ctor

}//end class
