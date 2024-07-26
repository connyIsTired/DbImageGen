namespace DbImageGen;

public class LineHorizontalState : ILineBuilderState
{

	ForeignKeyLineBuilder Fklb;

	public LineHorizontalState(ForeignKeyLineBuilder fklb)
	{
		Fklb = fklb;
	}
	public LinePoint MakePoint(LinePoint currentPoint)
	{
		var tablePositions = Fklb.Dto.Tables.Select(t => t).
			OrderBy(t => t.TablePositions.TableStartX).ThenBy(x => x.TablePositions.TableStartY);
		if (currentPoint.XPosition < Fklb.EndPoint.XPosition)
		{
			if (tablePositions.Any(tp => currentPoint.XPosition < tp.TablePositions.TableStartX && 
						tp.TablePositions.TableStartX < Fklb.EndPoint.YPosition)) 
			{
				if ( tablePositions.Any(tp => currentPoint.YPosition == (tp.TablePositions.TableStartY + 50)))
					{
						Fklb.State = Fklb.EndState;
						return Fklb.EndPoint;
					} else {
						Fklb.State = Fklb.VerticalState;
						return new LinePoint{XPosition = currentPoint.XPosition + 25, YPosition = currentPoint.YPosition};
				}
			}
		}
		Fklb.State = Fklb.EndState;
		return Fklb.EndPoint;
	}
}
// NEED TO RETHINK ALL OF THIS. THINK IN TERMS OF WHAT IS IN MY WAY AND HOW MUCH CAN BE ADDED TO X VALUE
