using DbImageGen.Models;
namespace DbImageGen;

public class DtoBuilder
{
	private DbImageGenRequest Incoming { get; init; }
	private int Offset { get; set; }
	private int TableMarginX { get; set; }
	private int TableMarginY { get; set; }
	private int TablePadding { get; set; }
	private List<TableDto> tableList { get; set; }

	public DtoBuilder(DbImageGenRequest incoming)
	{
		TableMarginX = 50;
		TableMarginY = 10;
		TablePadding = 5; 
		Incoming = incoming; 
		tableList = new List<TableDto>();
	} 
	public DbImageGenDto CreateDto() 
	{ 
		foreach (var table in Incoming.Table)
		{
			var tableDto = new TableDto(); 
			var fields = new List<FieldDto>(); 
			var offset = 45;
			foreach (var f in table.fields) 
			{
				var fieldsDto = new FieldDto();
				fieldsDto.Offset = offset;
				fieldsDto.FieldName = f;
				offset += 20;
				fields.Add(fieldsDto);
			}
			tableDto.Fields = fields;
			tableDto.TableName = table.tableName;
			tableDto.TableSize = CalcTableLength(table);
			tableList.Add(tableDto);
		}
		CalcTablePositions();
		return new DbImageGenDto{ Tables = tableList};
	}

	public int CalcTableLength(Table table)
	{
		var fieldCount = table.fields.Count();
		return 30 + fieldCount * 20;
	}

	public void CalcTablePositions()
	{
		assignTableXPositions();
	}

	public List<int> CalcTableXPositions()
	{
		var result = new List<int>{50, 350, 650}; 
		return result;
	}

	public void assignTableXPositions()
	{
		var positions = CalcTableXPositions();
		for (var i = 0; i < tableList.Count; i ++)
		{
			var positionIndex = i % positions.Count;
			tableList[i].TablePositions.TableStartX = positions[positionIndex];
			tableList[i].TablePositions.TableInsetX = positions[positionIndex] + TablePadding;
		}
	}
}
