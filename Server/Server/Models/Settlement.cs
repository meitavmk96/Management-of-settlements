using static DBservices;

namespace Server.Models
{
    public class Settlement
    {
        //fileds
        int id;
        string name;

        //properties
        public string Name { get => name; set => name = value; }
        public int Id { get => id; set => id = value; }


        public Settlement(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Settlement() { }

        public List<Settlement> Read()
        {
            DBservices dbs = new DBservices();
            return dbs.Read_Settlements();
        }

        public InsertResult Insert(out int id)
        {
            DBservices dbs = new DBservices();
            return dbs.Insert_Settlement(this, out id);
        }

        public InsertResult Update()
        {
            DBservices dbs = new DBservices();
            return dbs.Update_Settlement(this);
        }

        public int Delete(int id)
        {
            DBservices dbs = new DBservices();
            List<Settlement> SettlementsList = dbs.Read_Settlements();

            foreach (Settlement settlement in SettlementsList)
            {
                if (settlement.Id == id)
                    return dbs.Delete_Settlement(id);
            }
            return (0);
        }
    }
}
