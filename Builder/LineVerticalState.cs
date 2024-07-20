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

	}
}
