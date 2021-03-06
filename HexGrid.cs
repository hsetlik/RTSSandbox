using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour {

	public int width = 6;
	public int height = 6;

	public HexCell cellPrefab;
	public Text cellLabelPrefab;

	public Color defaultColor = Color.white;
	public Color touchedColor = Color.cyan;

	HexCell[] cells;

	Canvas gridCanvas;
	HexMesh hexMesh;

	void Awake () {
		gridCanvas = GetComponentInChildren<Canvas>();
		hexMesh = GetComponentInChildren<HexMesh>();

		cells = new HexCell[height * width];

		for (int z = 0, i = 0; z < height; z++) {
			for (int x = 0; x < width; x++) {
				CreateCell(x, z, i);
				i += 1;
			}
		}
	}

	void Start () {
		hexMesh.Triangulate(cells);
	}

	void CreateCell (int x, int z, int i) {
		Vector3 position;
		position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
		position.y = 0f;
		position.z = z * (HexMetrics.outerRadius * 1.5f);

		HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
		cell.transform.SetParent(transform, false);
		cell.transform.localPosition = position;
		cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
		cell.color = defaultColor;
		/*
		if (x > 0)
        {
			cell.SetNeighbor(cells[i - 1], HexDirection.W);
		}
			
		if(z > 0)
		{
			if((z & 1) == 0)
            {
				cell.SetNeighbor(cells[i - width], HexDirection.SE);
				if (x > 0)
				{
					cell.SetNeighbor(cells[i - width - 1], HexDirection.SW);
				}
			}
			else
			{
				cell.SetNeighbor(cells[i - width], HexDirection.SW);
				if(x < width - 1)
                {
					cell.SetNeighbor(cells[i - width + 1], HexDirection.SE);
                }
			}

			
		}
		*/
		Text label = Instantiate<Text>(cellLabelPrefab);
		label.rectTransform.SetParent(gridCanvas.transform, false);
		label.rectTransform.anchoredPosition =
			new Vector2(position.x, position.z);
		label.text = cell.coordinates.ToStringOnSeparateLines();
	}

	public void ColorCell(Vector3 position, Color color)
	{
		position = transform.InverseTransformPoint(position);
		HexCoordinates coordinates = HexCoordinates.FromPosition(position);
		Debug.Log("touched at " + coordinates.ToString());
		int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
		HexCell cell = cells[index];
		cell.color = color;
		hexMesh.Triangulate(cells);
	}
}