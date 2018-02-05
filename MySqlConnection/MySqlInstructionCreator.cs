using DBMS2018.Table;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySqlConnection
{
    class MySqlInstructionCreator
    {
        #region "Fields"

        private TableInformation info;

        #endregion

        #region "Constructors"

        public MySqlInstructionCreator(TableInformation information)
        {
            this.info = information;
        }

        #endregion

        #region "Properties"



        #endregion

        #region "Methods"

        private string CreateInstruction()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("create table ").Append(info.Name).Append(" (");

            foreach(ColumnInformation column in info.Columns)
            {
                builder.Append(CreateColumnInstruction(column));
            }

            return builder.ToString();
        }

        private string CreateColumnInstruction(ColumnInformation column)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(column.Name).Append(" ");

            return builder.ToString();
        }

        private MySqlDbType GetType(Type type)
        {
            return MySqlDbType.Binary;
        }

        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        public override string ToString()
        {
            return CreateInstruction();
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
