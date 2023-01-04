using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * TableCreate_Game
 * 
 */
public class TableCreate_Game : TableCreate
{
    public override Dictionary<System.Type, ITable> CreateTableList()
    {
        Dictionary<System.Type, ITable> _list = new Dictionary<System.Type, ITable>();
        AddTable<HeroTable>(_list);
        AddTable<ExpTable>(_list);
        AddTable<DropExpTable>(_list);
        AddTable<MonsterTable>(_list);
        AddTable<SkillTable>(_list);
        AddTable<StageTable>(_list);
        return _list;
    }

    public override Dictionary<string, Dictionary<string, List<string>>> CreateEditorTableList()
    {
        Dictionary<string, Dictionary<string, List<string>>> _list = new Dictionary<string, Dictionary<string, List<string>>>();
        AddEditorTable(_list, "HeroTable", "HeroTable.xlsx", "HeroTable");
        AddEditorTable(_list, "ExpTable", "ExpTable.xlsx", "ExpTable");
        AddEditorTable(_list, "DropExpTable", "DropExpTable.xlsx", "DropExpTable");
        AddEditorTable(_list, "MonsterTable", "MonsterTable.xlsx", "MonsterTable");
        AddEditorTable(_list, "SkillTable", "SkillTable.xlsx", "SkillTable");
        AddEditorTable(_list, "StageTable", "StageTable.xlsx", "StageTable");
        return _list;
    }
}
