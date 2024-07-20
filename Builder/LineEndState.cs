namespace DbImageGen;

public class LineEndState : ILineBuilderState
{

	ForeignKeyLineBuilder Fklb;

	public LineEndState(ForeignKeyLineBuilder fklb)
	{
		Fklb = fklb;
	}
	public LinePoint MakePoint(LinePoint currentPoint)
	{

	}
}
