using UnityEngine;

public class HexCell : MonoBehaviour {
	public HexCoordinates coordinates;
	public Color color;
	
	[SerializeField]
	HexCell[] neighbors;

	public HexCell GetNeighbor(HexDirection direction)
    {
		return neighbors[(int)direction];
    }
	public void SetNeighbor(HexCell cell, HexDirection direction)
    {
        neighbors[(int)direction] = cell;
		cell.neighbors[(int)direction.Opposite()] = this;
	}
}