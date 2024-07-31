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
	public LinePoint StartingPoint {get; set;}
	public int Direction {get; set;}
	public Dictionary<(int X, int Y), SortedSet<(int X, int y)>> PointList {get; set;}
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
		linePointList.Add(State.MakePoint(point));

		return BuildLine(point, linePointList);
	}

	public List<ForeignKeyLine> BuildLines()
	{
		foreach(var thing in PointList){Console.WriteLine(thing);}
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
				SetDirection();
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
		var endTable = Dto.Tables.Find(t => t.Id == fk);

		EndPoint.XPosition = endTable.TablePositions.TableStartX;
		EndPoint.YPosition = endTable.TablePositions.TableStartY + (endTable.TableSize / 2);
	}

	private void SetDirection()
	{
		// move right = 1
		// move left = 2
		// move up = 4
		// move down = 8

		var horizontalValue = StartingPoint.XPosition < EndPoint.XPosition ? 1 : 2;
		var verticalValue = StartingPoint.YPosition <= EndPoint.YPosition ? 8 : 4;
		Direction = horizontalValue + verticalValue;
	}
}


