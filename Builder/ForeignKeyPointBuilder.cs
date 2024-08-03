namespace DbImageGen;

public class ForeignKeyPointBuilder
{
	private int Margin;
	private List<TableDto> Tables;
	private Dictionary<(int X, int Y, string PointType), SortedSet<(int X, int Y, string PointType)>> PointList;

	public ForeignKeyPointBuilder(int margin, List<TableDto> tables)
	{
		Margin = margin;
		Tables = tables;
		PointList = new Dictionary<(int X, int Y, string PointType), SortedSet<(int X, int Y, string PointType)>>();
	}

	public Dictionary<(int X, int Y, string PointType), SortedSet<(int X, int Y, string PointType)>> BuildPointList()
	{
		foreach(var table in Tables)
		{
			(int X, int Y, string PointType) TopLeft = (
					table.TablePositions.TableStartX - Margin,
					table.TablePositions.TableStartY - Margin,
					"E");
			(int X, int Y, string PointType) TopRight = (
					table.TablePositions.TableStartX + Margin + table.TableWidth,
					table.TablePositions.TableStartY - Margin,
					"E");
			(int X, int Y, string PointType) BottomLeft = (
					table.TablePositions.TableStartX - Margin,
					table.TablePositions.TableStartY + Margin + table.TableSize,
					"E");
			(int X, int Y, string PointType) BottomRight = (
					table.TablePositions.TableStartX + Margin + table.TableWidth,
					table.TablePositions.TableStartY + Margin + table.TableSize,
					"E");
			(int X, int Y, string PointType) StartingPoint = (
					table.TablePositions.TableStartX + table.TableWidth,
					table.TablePositions.TableStartY + (table.TableSize/2
						),"E");
			(int X, int Y, string PointType) EndingPoint = (
					table.TablePositions.TableStartX,
					table.TablePositions.TableStartY + (table.TableSize/2
						),"E");
			(int X, int Y, string PointType) StartingTerminal = (
					table.TablePositions.TableStartX + table.TableWidth + Margin,
					table.TablePositions.TableStartY + (table.TableSize/2
						),"T");
			(int X, int Y, string PointType) EndingTerminal = (
					table.TablePositions.TableStartX - Margin,
					table.TablePositions.TableStartY + (table.TableSize/2
						),"T");

			AddPointToDict(TopLeft, new SortedSet<(int X, int Y, string PointType)>{TopRight, BottomLeft, StartingTerminal});
			AddPointToDict(TopRight, new SortedSet<(int X, int Y, string PointType)>{TopLeft, BottomRight, EndingTerminal});
			AddPointToDict(BottomLeft, new SortedSet<(int X, int Y, string PointType)>{TopLeft, BottomRight, StartingTerminal});
			AddPointToDict(BottomRight, new SortedSet<(int X, int Y, string PointType)>{TopRight, BottomLeft, EndingTerminal});
			AddPointToDict(StartingPoint, new SortedSet<(int X, int Y, string PointType)>{StartingTerminal});
			AddPointToDict(StartingTerminal, new SortedSet<(int X, int Y, string PointType)>{StartingPoint, TopRight, BottomRight});
			AddPointToDict(EndingTerminal, new SortedSet<(int X, int Y, string PointType)>{EndingPoint, TopLeft, BottomLeft});
			AddPointToDict(EndingPoint, new SortedSet<(int X, int Y, string PointType)>{EndingTerminal});
		}
		return PointList;
	}

	private void AddPointToDict((int X, int Y, string PointType) node, SortedSet<(int X, int , string PointTypeY)> edges)
	{
			if (!PointList.TryAdd(node, edges))
			{
				PointList[node].UnionWith(edges);
			}
	}
}
