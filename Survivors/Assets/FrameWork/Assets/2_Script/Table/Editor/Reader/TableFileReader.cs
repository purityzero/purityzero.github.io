using System.Collections.Generic;

public abstract class TableFileReader
{
    protected string m_fileDirectory;
    //protected int m_rowIdx_type = 0;
    protected int m_rowIdx_name = 0;
    protected int m_rowIdx_data = 1;

    public string fileDirectory { get { return m_fileDirectory; } set { m_fileDirectory = value; } }
    //public int rowIdx_type { get { return m_rowIdx_type; } set { m_rowIdx_type = value; } }
    public int rowIdx_name { get { return m_rowIdx_name; } set { m_rowIdx_name = value; } }
    public int rowIdx_data { get { return m_rowIdx_data; } set { m_rowIdx_data = value; } }

    public abstract void Clear();
    public abstract void AddFile(string _fileName, string _sheetName);
    public abstract void LoadFile();
    public abstract List<Dictionary<string, string>> GetData(string _filename, string _sheetName);
    public abstract Dictionary<string, string> GetTypeData(string _fileName, string _sheetName);
}