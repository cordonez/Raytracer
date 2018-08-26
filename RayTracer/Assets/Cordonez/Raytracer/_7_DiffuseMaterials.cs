namespace Cordonez.Raytracer
{
	using System.Collections.Generic;
	using UnityEngine;

	public class _7_DiffuseMaterials : MonoBehaviour
	{
		public int Width;
		public int Height;
		public Material Material;
		[Range(1, 100)]
		public int SamplesPerPixel;
		public bool UpdateEveryFrame;

		private Texture2D m_texture2D;
		private readonly Vector3 m_bottomColor = Vector3.one;
		private readonly Vector3 m_topColor = new Vector3(0.5f, 0.7f, 1f);
		private readonly List<IHitable> m_worldElements = new List<IHitable>();
		private readonly MyCamera m_myCamera = new MyCamera();

		private void Start()
		{
			m_texture2D = new Texture2D(Width, Height);
			Material.mainTexture = m_texture2D;
			m_worldElements.Add(new Sphere(new Vector3(0, 0, -1), 0.5f));
			m_worldElements.Add(new Sphere(new Vector3(0, -100.5f, 1), 100));
			Draw();
		}

		private void Update()
		{
			if (UpdateEveryFrame)
			{
				Draw();
			}
		}

		private void Draw()
		{
			for (int i = 0; i < Width; i++)
			{
				for (int j = 0; j < Height; j++)
				{
					Vector3 sumColor = Vector3.zero;
					for (int sample = 0; sample < SamplesPerPixel; sample++)
					{
						float u = (i + Random.Range(0f, 1f)) / Width;
						float v = (j + Random.Range(0f, 1f)) / Height;
						MyRay ray = m_myCamera.GetRay(u, v);
						Color color = GetColor(ray);
						sumColor += new Vector3(color.r, color.g, color.b);
					}

					Vector3 averageColor = sumColor / SamplesPerPixel;
					Vector3 gammaCorrectedColor = new Vector3(Mathf.Sqrt(averageColor.x), Mathf.Sqrt(averageColor.y), Mathf.Sqrt(averageColor.z));
					m_texture2D.SetPixel(i, j, new Color(gammaCorrectedColor.x, gammaCorrectedColor.y, gammaCorrectedColor.z));
				}
			}

			m_texture2D.Apply();
		}

		private Color GetBackgroundColor(MyRay _ray)
		{
			Vector3 unitDirection = _ray.Direction;
			float t = 0.5f * unitDirection.y + 1f;
			Vector3 c = (1f - t) * m_bottomColor + t * m_topColor;
			return new Color(c.x, c.y, c.z);
		}

		private Color GetColor(MyRay _ray)
		{
			HitInfo hitInfo = new HitInfo();
			if (m_worldElements.Hit(_ray, 0.001f, float.MaxValue, hitInfo))
			{
				Vector3 target = hitInfo.HitPoint + hitInfo.Normal + RandomPointInUnitSphere();
				return 0.5f * GetColor(new MyRay(hitInfo.HitPoint, target - hitInfo.HitPoint));
			}

			return GetBackgroundColor(_ray);
		}

		private Vector3 RandomPointInUnitSphere()
		{
			Vector3 p;
			do
			{
				p = 2 * new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)) - Vector3.one;
			} while (p.sqrMagnitude >= 1);

			return p;
		}
	}
}