namespace DbImageGen;

public class Builder
{
	private string returnObj = string.Empty;
	private DbImageGenDto _incoming;
	public Builder(DbImageGenDto incoming)
	{
		_incoming = incoming;
	}

	public void BuildParentSvg()
	{
		returnObj += "<svg width=\"1000\" height=\"1000\" xmlns=\"http://www.w3.org/2000/svg\">";
	}

	public void BuildOpenSvgTag()
	{
		returnObj += "<svg x=\"50\">";
	}

	public void BuildCloseSvgTag()
	{
		 returnObj += "</svg>";
	}

	public string BuildText(FieldDto input)
	{
	    var svgText = $"<text font-size=\"10pt\" x=\"55\" y=\"{input.offset}\" class=\"small\">{input.FieldName}</text>";
	    return svgText;
	}

	public void BuildLine()
	{
		returnObj += "<line x1=\"0\" x2=\"250\" y1=\"30\" y2=\"30\" stroke=\"black\"/>";
	}

	public void BuildTitle()
	{

		BuildOpenSvgTag();
		returnObj += $"<text font-size=\"10pt\" x=\"125\" y=\"25\" class=\"small\" text-anchor=\"middle\">{_incoming.Tables.First().TableName}</text>";
		BuildLine();
		BuildCloseSvgTag();
	}

	public void BuildAllText()
	{
		returnObj += _incoming.Tables.First().Fields.Aggregate(string.Empty, (result, next) => result = result + BuildText(next));

	}

	public void BuildRect()
	{
		returnObj += "<rect x=\"50\" y=\"10\" width=\"250\" height=\"250\" fill=\"none\" stroke=\"blue\"/>";
	}
	
	public string Build()
	{
		BuildParentSvg();
		BuildRect();
		BuildTitle();
		BuildAllText();
		BuildCloseSvgTag();
		return returnObj;
	}

}
