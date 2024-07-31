namespace DbImageGen;

public class LineVerticalState : ILineBuilderState
{

	ForeignKeyLineBuilder Fklb;

	public LineVerticalState(ForeignKeyLineBuilder fklb)
	{
		Fklb = fklb;
	}
	public LinePoint MakePoint(LinePoint currentPoint)
	{
		Fklb.State = Fklb.EndState;
		return Fklb.EndPoint;
		// our work flow should be:
		// 1) can we travel to end point terminal point?
		// 2) if not, lets travel to a y that is just before, either below or above our enpoint terminal point
		//
		// remember to use those enums baby!
	}
}
