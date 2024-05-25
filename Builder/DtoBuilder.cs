using DbImageGen.Models;
namespace DbImageGen;

public class DtoBuilder
{
	private DbImageGenRequest Incoming { get; init; }
	private int Offset { get; set; }
	private int TableMarginX { get; set; }
	private int TableMarginY { get; set; }
	private int TablePadding { get; set; }

	public DtoBuilder(DbImageGenRequest incoming)
	{
		Offset = 45;
		TableMarginX = 50;
		TableMarginY = 10;
		TablePadding = 5; 
		Incoming = incoming; 
	} 
	public DbImageGenDto CreateDto() 
	{ 
		var tableList = new List<TableDto>(); 
		var tableDto = new TableDto(); 
		var fields = new List<FieldDto>(); 
		foreach (var f in Incoming.Table.First().fields) 
		{
			var fieldsDto = new FieldDto();
			fieldsDto.Offset = Offset;
			fieldsDto.FieldName = f;
			Offset += 20;
			fields.Add(fieldsDto);
		}
		tableDto.Fields = fields;
		tableDto.TableName = Incoming.Table.First().tableName;
		tableDto.TableSize = CalcTableLength();
		tableDto.TablePositions = CalcTablePositions();
		tableList.Add(tableDto);
		return new DbImageGenDto{ Tables = tableList};
	}

	public int CalcTableLength()
	{
		var fieldCount = Incoming.Table.First().fields.Count();
		return 20 + fieldCount * 20;
	}

	public TablePositions CalcTablePositions()
	{
		var positions = new TablePositions();
			    
		positions.TableStartX = TableMarginX;
	        positions.TableStartY = TableMarginY;
		positions.TableInsetX = TablePadding;
		return positions;
	}
}
