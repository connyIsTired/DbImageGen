namespace DbImageGen;

public class ForeignKeyLineBuilder
{
	DbImageGenDto Dto;

	public ILineBuilderState StartState {get; init;}
	public ILineBuilderState EndState {get; init;}
	public ILineBuilderState HorizontalState {get; init;}
	public ILineBuilderState VerticalState {get; init;}

	public ILineBuilderState State {get; set;}

	public ForeignKeyLineBuilder(DbImageGenDto dto)
	{
		Dto = dto;
		StartState = new LineStartState(this);
		EndState = new LineEndState(this);
		HorizontalState = new LineHorizontalState(this);
		VerticalState = new LineVerticalState(this);
		State = StartState;
	}

	public List<LinePoint> BuildLine(LinePoint point, List<LinePoint> linePointList)
	{

		if (State == EndState)
		{
			linePointList.Add(State.MakePoint(point));
			return linePointList;
		}
		linePointList.Add(State.MakePoint(point));

		return BuildLine(point, linePointList);
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
				var startingPoint = GetStartingPoint(table);
				var line = BuildLine(startingPoint, linePointList);
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
}


