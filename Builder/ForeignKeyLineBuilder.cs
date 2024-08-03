namespace DbImageGen;

public class ForeignKeyLineBuilder
{
	public DbImageGenDto Dto {get; init;}

	public ILineBuilderState StartState {get; init;}
	public ILineBuilderState EndState {get; init;}
	public ILineBuilderState HorizontalState {get; init;}
	public ILineBuilderState VerticalState {get; init;}

	public ILineBuilderState State {get; set;}
	public LinePoint EndPoint {get; set;}
	public TableDto EndTable {get; set;}
	public LinePoint StartingPoint {get; set;}
	public int Direction {get; set;}
	public Dictionary<(int X, int Y, string PointType), SortedSet<(int X, int y, string PointType)>> PointList {get; set;}
	private int Margin = 50;

	public ForeignKeyLineBuilder(DbImageGenDto dto)
	{
		Dto = dto;
		StartState = new LineStartState(this);
		EndState = new LineEndState(this);
		HorizontalState = new LineHorizontalState(this);
		VerticalState = new LineVerticalState(this);
		State = StartState;
		EndPoint = new LinePoint();
		StartingPoint = new LinePoint();
		Direction = 0;
		PointList = new ForeignKeyPointBuilder(Margin/2, Dto.Tables).BuildPointList();
	}

	public List<LinePoint> BuildLine(LinePoint point, List<LinePoint> linePointList)
	{

		if (State == EndState)
		{
			return linePointList;
		}
		SetDirection(point);
		var nextPoint = State.MakePoint(point);
		linePointList.Add(nextPoint);

		return BuildLine(nextPoint, linePointList);
	}

	public List<ForeignKeyLine> BuildLines()
	{
		var Lines = new List<ForeignKeyLine>();
		foreach (var table in Dto.Tables)
		{
			if (table.ForeignKeys.Count == 0)
			{
				continue;
			}
			foreach(var fk in table.ForeignKeys)
			{
				var linePointList = new List<LinePoint>();
				StartingPoint = GetStartingPoint(table);
				FindEndPoint(fk);
				var line = BuildLine(StartingPoint, linePointList);
				var fkl = new ForeignKeyLine();
				fkl.LinePoints = line;
				Lines.Add(fkl);
			}
		}
		return Lines;
	}

	private LinePoint GetStartingPoint(TableDto table)
	{
		var xpos = table.TablePositions.TableStartX + table.TableWidth;
		var ypos = table.TablePositions.TableStartY + table.TableSize / 2;
		return new LinePoint
		{
			XPosition = xpos,
			YPosition = ypos
		};
	}

	private void FindEndPoint(int fk)
	{
		EndTable = Dto.Tables.Find(t => t.Id == fk);

		EndPoint.XPosition = EndTable.TablePositions.TableStartX;
		EndPoint.YPosition = EndTable.TablePositions.TableStartY + (EndTable.TableSize / 2);
	}

	private void SetDirection(LinePoint currentPoint)
	{
		// move right = 1
		// move left = 2
		// move up = 4
		// move down = 8
		
		if (State == HorizontalState || State == EndState)
		{
			return;
		}

		if (State == StartState)
		{
			var horizontalValue = currentPoint.XPosition < EndPoint.XPosition ? 1 : 2;
			var verticalValue = currentPoint.YPosition <= EndPoint.YPosition ? 8 : 4;
			Direction = horizontalValue + verticalValue;
			return;
		}

		var endTableTop = EndTable.TablePositions.TableStartY;
		var endTableBottom = EndTable.TablePositions.TableStartY + EndTable.TableSize;

		if (currentPoint.YPosition < endTableTop || (currentPoint.YPosition < endTableBottom && currentPoint.YPosition > endTableTop))
		{
			var horizontalValue = currentPoint.XPosition < EndPoint.XPosition ? 1 : 2;
			var verticalValue = 8;
			Direction = horizontalValue + verticalValue;
			return;
		}
		if (currentPoint.YPosition > endTableBottom)
		{
			var horizontalValue = currentPoint.XPosition < EndPoint.XPosition ? 1 : 2;
			var verticalValue = 4;
			Direction = horizontalValue + verticalValue;
			return;
		}
	}
}


