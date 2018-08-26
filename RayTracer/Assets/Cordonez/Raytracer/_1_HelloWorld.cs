namespace Cordonez.Raytracer
{
	using UnityEngine;

	public class _1_HelloWorld : MonoBehaviour
	{
		public int Width;
		public int Height;
		public Material Material;

		private Texture2D m_texture2D;

		private void Start()
		{
			m_texture2D = new Texture2D(Width, Height);
			Material.mainTexture = m_texture2D;
			Draw();
		}

		private void Draw()
		{
			for (int i = 0; i < Width; i++)
			{
				for (int j = 0; j < Height; j++)
				{
					m_texture2D.SetPixel(i, j, new Color((float) i / Width, (float) j / Height, 0.2f));
				}
			}

			m_texture2D.Apply();
		}
	}
}