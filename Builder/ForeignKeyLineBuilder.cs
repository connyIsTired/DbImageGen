namespace DbImageGen;

public class ForeignKeyLineBuilder
{
	DbImageGenDto Dto;

	ILineBuilderState StartState;
	ILineBuilderState EndState;
	ILineBuilderState HorizontalState;
	ILineBuilderState VerticalState;

	ILineBuilderState State;

	public ForeignKeyLineBuilder(DbImageGenDto dto)
	{
		Dto = dto;
		StartState = new LineStartState(this);
		EndState = new LineEndState(this);
		HorizontalState = new LineHorizontalState(this);
		VerticalState = new LineVerticalState(this);
		State = StartState;
	}

	public List<LinePoint> BuildLine(LinePoint point)
	{
		var LinePointList = new List<LinePoint>();

		if (State == EndState)
		{
			return LinePointList;
		}
		LinePointList.Add(State.MakePoint(point));

		return BuildLine(point);
	}

	public List<ForeignKeyLine> BuildLines()
	{
		var Lines = new List<ForeignKeyLine>();
		foreach (var table in Dto.Tables)
		{
			foreach(var fk in table.ForeignKeys)
			{
				var startingPoint = GetStartingPoint(table);
				var line = BuildLine(startingPoint);
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
		var ypos = table.TablePositions.TableStartY + table.TableSize;
		return new LinePoint
		{
			XPosition = xpos,
			YPosition = ypos
		};
	}
}


