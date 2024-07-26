using DbImageGen.Models;
namespace DbImageGen;

public class DtoBuilder
{
	private DbImageGenRequest Incoming { get; init; }
	private int Offset { get; set; }
	private int TableMarginX { get; set; }
	private int TableMarginY { get; set; }
	private int TablePadding { get; set; }
	private int TableWidth { get; set; }
	private List<TableDto> tableList { get; set; }
	private List<ForeignKeyLine> foreignKeys { get; set; }

	public DtoBuilder(DbImageGenRequest incoming)
	{
		TableMarginX = 50;
		TableMarginY = 50;
		TablePadding = 5; 
		TableWidth = 250;
		Incoming = incoming; 
		tableList = new List<TableDto>();
		foreignKeys = new List<ForeignKeyLine>();
	} 
	public DbImageGenDto CreateDto() 
	{ 
		foreach (var table in Incoming.Table)
		{
			var tableDto = new TableDto(); 
			var fields = new List<FieldDto>(); 
			var offset = 40;
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
			tableDto.TableWidth = TableWidth;
			tableDto.Id = table.id;
			tableDto.ForeignKeys = table.foreignKeys;
			tableList.Add(tableDto);
		}
		CalcTablePositions();
		return new DbImageGenDto
		{ 
			Tables = tableList,
			ForeignKeys = foreignKeys

		};
	}

	public int CalcTableLength(Table table)
	{
		var fieldCount = table.fields.Count();
		return 30 + fieldCount * 20;
	}

	public void CalcTablePositions()
	{
		assignTableXPositions();
		assignTableYPositions();
	}

	public List<int> CalcTableXPositions()
	{
		var result = new List<int>(); 
		var tablesPerRow = CalTablesPerRow();
		int position = TableMarginX;
		while (tablesPerRow !=0)
		{
			result.Add(position);
			position += TableWidth + TableMarginX;
			tablesPerRow--;
		}

		return result;
	}

	public void assignTableYPositions()
	{
		for (var i = 0; i < tableList.Count; i ++)
		{
			var indexAmountToStepBack = i < CalTablesPerRow() ? 0 : i - CalTablesPerRow();
			tableList[i].TablePositions.TableStartY = i < CalTablesPerRow() ? TableMarginY : tableList[indexAmountToStepBack].TableSize + tableList[indexAmountToStepBack].TablePositions.TableStartY + 50;
		}
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

	public int CalTablesPerRow()
	{
		return (1000-TableMarginX) / (TableWidth + TableMarginX);
	}
}
