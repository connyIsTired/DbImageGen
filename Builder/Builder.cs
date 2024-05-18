using DbImageGen.Models;
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
		returnObj = returnObj + "<svg width=\"1000\" height=\"1000\" xmlns=\"http://www.w3.org/2000/svg\">";
	}

	public void BuildCloseSvgTag()
	{
		returnObj = returnObj + "</svg>";
	}

	public string BuildText(FieldDto input)
	{
	    var svgText = $"<text font-size=\"10pt\" x=\"55\" y=\"{input.offset}\" class=\"small\">{input.FieldName}</text>";
	    return svgText;
	}

	public void BuildAllText()
	{
		returnObj = returnObj + _incoming.Tables.First().Fields.Aggregate(string.Empty, (result, next) => result = result + BuildText(next));

	}

	public void BuildRect()
	{
		returnObj = returnObj + "<rect x=\"50\" y=\"10\" width=\"250\" height=\"250\" fill=\"none\" stroke=\"blue\"/>";
	}
	
	public string Build()
	{
		BuildParentSvg();
		BuildRect();
		BuildAllText();
		BuildCloseSvgTag();
		return returnObj;
	}

}
