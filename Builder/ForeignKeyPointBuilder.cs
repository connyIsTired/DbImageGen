namespace DbImageGen;

public class ForeignKeyPointBuilder
{
	private int Margin;
	private List<TableDto> Tables;
	private List<(int X, int Y)> PointList;

	public ForeignKeyPointBuilder(int margin, List<TableDto> tables)
	{
		Margin = margin;
		Tables = tables;
		PointList = new List<(int X, int Y)>();
	}

	public List<(int X, int Y)> BuildPointList()
	{
		foreach(var table in Tables)
		{
			(int X, int Y) TL = (
					table.TablePositions.TableStartX - Margin,
					table.TablePositions.TableStartY - Margin);
			(int X, int Y) TR = (
					table.TablePositions.TableStartX + Margin + table.TableWidth,
					table.TablePositions.TableStartY - Margin);
			(int X, int Y) BL = (
					table.TablePositions.TableStartX - Margin,
					table.TablePositions.TableStartY + Margin + table.TableSize);
			(int X, int Y) BR = (
					table.TablePositions.TableStartX + Margin + table.TableWidth,
					table.TablePositions.TableStartY + Margin + table.TableSize);

			if (!PointList.Contains(TL))
			{
				PointList.Add(TL);
			}
			if (!PointList.Contains(TR))
			{
				PointList.Add(TR);
			}
			if (!PointList.Contains(BL))
			{
				PointList.Add(BL);
			}
			if (!PointList.Contains(BR))
			{
				PointList.Add(BR);
			}
		}

		return PointList;
	}
}
