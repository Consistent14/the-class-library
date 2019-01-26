using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadHelper01
{
    public class DatabaseWorker
    {
        public readonly Database Database;

        public readonly ObjectId Linetype_Continuous;

        public DatabaseWorker(Database database)
        {
            this.Database = database;

            using (var host = database.TransactionManager.StartOpenCloseTransaction())
            {
                {
                    var linetypeTb = (LinetypeTable)host.GetObject(database.LinetypeTableId, OpenMode.ForRead);

                    if (linetypeTb == null) return;

                    if (linetypeTb.Has("Continuous"))
                    {
                        this.Linetype_Continuous = linetypeTb["Continuous"];
                    }
                }
            }
        }
    }
}
