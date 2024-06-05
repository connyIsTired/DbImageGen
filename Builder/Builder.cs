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

	public void BuildOpenSvgTag(TableDto table)
	{
		ReturnObj += $"<svg x=\"{table.TablePositions.TableStartX}\">";
	}

	public void BuildCloseSvgTag()
	{
		 ReturnObj += "</svg>";
	}

	public string BuildText(TableDto table, FieldDto input)
	{
	    var svgText = $"<text font-size=\"10pt\" x=\"{table.TablePositions.TableInsetX}\" y=\"{input.Offset}\" class=\"small\">{input.FieldName}</text>";
	    return svgText;
	}

	public void BuildLine()
	{
		ReturnObj += "<line x1=\"0\" x2=\"250\" y1=\"30\" y2=\"30\" stroke=\"black\"/>";
	}

	public void BuildTitle(TableDto table)
	{

		BuildOpenSvgTag(table);
		ReturnObj += $"<text font-size=\"10pt\" x=\"125\" y=\"25\" class=\"small\" text-anchor=\"middle\">{table.TableName}</text>";
		BuildLine();
		BuildCloseSvgTag();
	}

	public void BuildAllText(TableDto table)
	{
		ReturnObj += table.Fields.Aggregate(string.Empty, (result, next) => result = result + BuildText(table, next));

	}

	public void BuildRect(TableDto table)
	{
		ReturnObj += $"<rect x=\"{table.TablePositions.TableStartX}\" y=\"{table.TablePositions.TableStartY}\" width=\"250\" height=\"{table.TableSize}\" fill=\"none\" stroke=\"blue\"/>";
	}

	public void BuildTable(TableDto table)
	{
		BuildRect(table);
		BuildTitle(table);
		BuildAllText(table);
	}

	public void BuildTables()
	{
		foreach (var table in Incoming.Tables)
		{
			BuildTable(table);
		}
	}
	
	public string Build()
	{
		BuildParentSvg();
		BuildTables();
		BuildCloseSvgTag();
		return ReturnObj;
	}
}
