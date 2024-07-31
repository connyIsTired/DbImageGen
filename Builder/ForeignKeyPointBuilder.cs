namespace DbImageGen;

public class ForeignKeyPointBuilder
{
	private int Margin;
	private List<TableDto> Tables;
	private Dictionary<(int X, int Y), SortedSet<(int X, int Y)>> PointList;

	public ForeignKeyPointBuilder(int margin, List<TableDto> tables)
	{
		Margin = margin;
		Tables = tables;
		PointList = new Dictionary<(int X, int Y), SortedSet<(int X, int Y)>>();
	}

	public Dictionary<(int X, int Y), SortedSet<(int X, int Y)>> BuildPointList()
	{
		foreach(var table in Tables)
		{
			(int X, int Y) TopLeft = (
					table.TablePositions.TableStartX - Margin,
					table.TablePositions.TableStartY - Margin);
			(int X, int Y) TopRight = (
					table.TablePositions.TableStartX + Margin + table.TableWidth,
					table.TablePositions.TableStartY - Margin);
			(int X, int Y) BottomLeft = (
					table.TablePositions.TableStartX - Margin,
					table.TablePositions.TableStartY + Margin + table.TableSize);
			(int X, int Y) BottomRight = (
					table.TablePositions.TableStartX + Margin + table.TableWidth,
					table.TablePositions.TableStartY + Margin + table.TableSize);
			(int X, int Y) StartingPoint = (
					table.TablePositions.TableStartX + table.TableWidth,
					table.TablePositions.TableStartY + (table.TableSize/2));
			(int X, int Y) EndingPoint = (
					table.TablePositions.TableStartX,
					table.TablePositions.TableStartY + (table.TableSize/2));
			(int X, int Y) StartingTerminal = (
					table.TablePositions.TableStartX + table.TableWidth + Margin,
					table.TablePositions.TableStartY + (table.TableSize/2));
			(int X, int Y) EndingTerminal = (
					table.TablePositions.TableStartX - Margin,
					table.TablePositions.TableStartY + (table.TableSize/2));

			AddPointToDict(TopLeft, new SortedSet<(int X, int Y)>{TopRight, BottomLeft, StartingTerminal});
			AddPointToDict(TopRight, new SortedSet<(int X, int Y)>{TopLeft, BottomRight, EndingTerminal});
			AddPointToDict(BottomLeft, new SortedSet<(int X, int Y)>{TopLeft, BottomRight, StartingTerminal});
			AddPointToDict(BottomRight, new SortedSet<(int X, int Y)>{TopRight, BottomLeft, EndingTerminal});
			AddPointToDict(StartingPoint, new SortedSet<(int X, int Y)>{StartingTerminal});
			AddPointToDict(StartingTerminal, new SortedSet<(int X, int Y)>{StartingPoint, TopRight, BottomRight});
			AddPointToDict(EndingTerminal, new SortedSet<(int X, int Y)>{EndingPoint, TopLeft, BottomLeft});
			AddPointToDict(EndingPoint, new SortedSet<(int X, int Y)>{EndingTerminal});
		}
		return PointList;
	}

	private void AddPointToDict((int X, int Y) node, SortedSet<(int X, int Y)> edges)
	{
			if (!PointList.TryAdd(node, edges))
			{
				PointList[node].UnionWith(edges);
			}
	}
}
