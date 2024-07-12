namespace DbImageGen;

public class Table
{
	public string tableName {get; init;} = string.Empty;
	public List<string> fields {get; init;} = new List<string>();
	public int id {get; init;} 
	public List<int> foreignKeys {get; init;} = new List<int>();
}
