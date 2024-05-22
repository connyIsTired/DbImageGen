namespace DbImageGen;

public class Builder
{
	private string ReturnObj = string.Empty;
	private DbImageGenDto Incoming;
	public Builder(DbImageGenDto incoming)
	{
		Incoming = incoming;
	}

	public void BuildParentSvg()
	{
		ReturnObj += "<svg width=\"1000\" height=\"1000\" xmlns=\"http://www.w3.org/2000/svg\">";
	}

	public void BuildOpenSvgTag()
	{
		ReturnObj += "<svg x=\"50\">";
	}

	public void BuildCloseSvgTag()
	{
		 ReturnObj += "</svg>";
	}

	public string BuildText(FieldDto input)
	{
	    var svgText = $"<text font-size=\"10pt\" x=\"55\" y=\"{input.Offset}\" class=\"small\">{input.FieldName}</text>";
	    return svgText;
	}

	public void BuildLine()
	{
		ReturnObj += "<line x1=\"0\" x2=\"250\" y1=\"30\" y2=\"30\" stroke=\"black\"/>";
	}

	public void BuildTitle()
	{

		BuildOpenSvgTag();
		ReturnObj += $"<text font-size=\"10pt\" x=\"125\" y=\"25\" class=\"small\" text-anchor=\"middle\">{Incoming.Tables.First().TableName}</text>";
		BuildLine();
		BuildCloseSvgTag();
	}

	public void BuildAllText()
	{
		ReturnObj += Incoming.Tables.First().Fields.Aggregate(string.Empty, (result, next) => result = result + BuildText(next));

	}

	public void BuildRect()
	{
		ReturnObj += $"<rect x=\"50\" y=\"10\" width=\"250\" height=\"{Incoming.Tables.First().TableSize}\" fill=\"none\" stroke=\"blue\"/>";
	}
	
	public string Build()
	{
		BuildParentSvg();
		BuildRect();
		BuildTitle();
		BuildAllText();
		BuildCloseSvgTag();
		return ReturnObj;
	}

}
