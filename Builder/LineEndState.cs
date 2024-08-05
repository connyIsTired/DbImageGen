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
		Fklb.State = Fklb.StartState;
		return new LinePoint{XPosition=currentPoint.XPosition + 50, YPosition=currentPoint.YPosition};
	}
}
