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
		return new LinePoint();
	}
}
