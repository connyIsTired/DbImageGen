namespace DbImageGen;

public class LineStartState : ILineBuilderState
{
	ForeignKeyLineBuilder Fklb;

	public LineStartState(ForeignKeyLineBuilder fklb)
	{
		Fklb = fklb;
	}
	public LinePoint MakePoint(LinePoint currentPoint)
	{

	}
}
