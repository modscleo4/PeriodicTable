using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace PeriodicTable.Model.Database
{
    /// <summary>
    /// Classe com funções SQLite
    /// </summary>
    public class Connection : IDisposable
    {
        /// <summary>
        /// Atributo interno que contém a ConnectionString com o banco de dados
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// Atributo interno da conexão com o banco
        /// </summary>
        private SQLiteConnection con;

        /// <summary>
        /// Cria um novo objeto Connection
        /// </summary>
        /// <param name="dataSource">Localização do .db</param>
        public Connection(string dataSource)
        {
            connectionString = $"Data Source={dataSource.Trim()};";
        }

        /// <summary>
        /// Abre a conexão
        /// </summary>
        public void Open()
        {
            try
            {
                con = new SQLiteConnection(connectionString);
                con.Open();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Abre a conexão de forma assíncrona
        /// </summary>
        public async Task OpenAsync()
        {
            try
            {
                con = new SQLiteConnection(connectionString);
                await con.OpenAsync();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Encerra a conexão
        /// </summary>
        public void Close()
        {
            con.Close();
            con.Dispose();
            con = null;
        }

        /// <summary>
        /// Executa um comando SQL sem retorno
        /// </summary>
        /// <param name="sql">O comando SQL</param>
        /// <returns>Retorna o número de linhas afetadas pela query</returns>
        public int Run(string sql)
        {
            try
            {
                Open();

                sql = sql.Trim();

                var cmd = new SQLiteCommand(sql, con);
                var r = cmd.ExecuteNonQuery();

                Close();

                return r;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Executa um comando SQL sem retorno (com parâmetros)
        /// </summary>
        /// <param name="sql">O comando SQL</param>
        /// <param name="parameters">Os parâmetros do comando</param>
        /// <returns>Retorna o número de linhas afetadas pela query</returns>
        public int Run(string sql, List<object> parameters)
        {
            try
            {
                Open();

                sql = sql.Trim();

                var cmd = new SQLiteCommand(sql, con);
                var i = 1;
                foreach (var parameter in parameters)
                {
                    cmd.Parameters.AddWithValue($"@{i++}", parameter);
                }
                var r = cmd.ExecuteNonQuery();

                Close();

                return r;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Executa um comando SQL sem retorno de forma assíncrona
        /// </summary>
        /// <param name="sql">O comando SQL</param>
        /// <returns>Retorna o número de linhas afetadas pela query</returns>
        public async Task<int> RunAsync(string sql)
        {
            try
            {
                await OpenAsync();

                sql = await Task.Run(() => sql.Trim());

                var cmd = await Task.Run(() => new SQLiteCommand(sql, con));
                var r = await cmd.ExecuteNonQueryAsync();

                Close();

                return r;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Executa um comando SQL sem retorno de forma assíncrona (com parâmetros)
        /// </summary>
        /// <param name="sql">O comando SQL</param>
        /// <returns>Retorna o número de linhas afetadas pela query</returns>
        public async Task<int> RunAsync(string sql, List<object> parameters)
        {
            try
            {
                await OpenAsync();

                sql = await Task.Run(() => sql.Trim());

                var cmd = await Task.Run(() => new SQLiteCommand(sql, con));
                var i = 1;
                foreach (var parameter in parameters)
                {
                    await Task.Run(() => cmd.Parameters.AddWithValue($"@{i++}", parameter));
                }
                var r = await cmd.ExecuteNonQueryAsync();

                Close();

                return r;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Executa um comando SQL com retorno de dados
        /// </summary>
        /// <param name="sql">O comando SQL</param>
        /// <returns>Retorna os dados selecionados</returns>
        public SQLiteDataReader Select(string sql)
        {
            try
            {
                Open();

                sql = sql.Trim();

                var cmd = new SQLiteCommand(sql, con);
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Executa um comando SQL com retorno de dados (com parâmetros)
        /// </summary>
        /// <param name="sql">O comando SQL</param>
        /// <param name="parameters">Os parâmetros do comando</param>
        /// <returns>Retorna os dados selecionados</returns>
        public SQLiteDataReader Select(string sql, List<object> parameters)
        {
            try
            {
                Open();

                sql = sql.Trim();

                var cmd = new SQLiteCommand(sql, con);
                var i = 1;
                foreach (var parameter in parameters)
                {
                    cmd.Parameters.AddWithValue($"@{i++}", parameter);
                }
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Executa um comando SQL com retorno de dados de forma assíncrona
        /// </summary>
        /// <param name="sql">O comando SQL</param>
        /// <returns>Retorna os dados selecionados</returns>
        public async Task<DbDataReader> SelectAsync(string sql)
        {
            try
            {
                await OpenAsync();

                sql = await Task.Run(() => sql.Trim());

                var cmd = await Task.Run(() => new SQLiteCommand(sql, con));
                return await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Executa um comando SQL com retorno de dados de forma assíncrona (com parâmetros)
        /// </summary>
        /// <param name="sql">O comando SQL</param>
        /// <param name="parameters">Os parâmetros do comando</param>
        /// <returns>Retorna os dados selecionados</returns>
        public async Task<DbDataReader> SelectAsync(string sql, List<object> parameters)
        {
            try
            {
                await OpenAsync();

                sql = await Task.Run(() => sql.Trim());

                var cmd = await Task.Run(() => new SQLiteCommand(sql, con));
                var i = 1;
                foreach (var parameter in parameters)
                {
                    await Task.Run(() => cmd.Parameters.AddWithValue($"@{i++}", parameter));
                }
                return await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Obtém um DataTable 
        /// </summary>
        /// <param name="sql">O comando SQL</param>
        /// <returns>Retorna um DataTable com os dados selecionados</returns>
        public DataTable SelectDataTable(string sql)
        {
            try
            {
                Open();

                sql = sql.Trim();

                var dt = new DataTable();
                var cmd = new SQLiteCommand(sql, con);
                var da = new SQLiteDataAdapter(cmd);
                da.Fill(dt);

                Close();
                return dt;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Obtém um DataTable (com parâmetros)
        /// </summary>
        /// <param name="sql">O comando SQL</param>
        /// <returns>Retorna um DataTable com os dados selecionados</returns>
        public DataTable SelectDataTable(string sql, List<object> parameters)
        {
            try
            {
                Open();

                sql = sql.Trim();

                var dt = new DataTable();
                var cmd = new SQLiteCommand(sql, con);
                var i = 1;
                foreach (var parameter in parameters)
                {
                    cmd.Parameters.AddWithValue($"@{i++}", parameter);
                }

                var da = new SQLiteDataAdapter(cmd);
                da.Fill(dt);

                Close();
                return dt;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Obtém um DataSet
        /// </summary>
        /// <param name="sql">O comando SQL</param>
        /// <returns>Retorna um DataSet com os dados</returns>
        public DataSet SelectDataSet(string sql)
        {
            try
            {
                Open();

                sql = sql.Trim();

                var ds = new DataSet();
                var cmd = new SQLiteCommand(sql, con);
                var da = new SQLiteDataAdapter(cmd);
                da.Fill(ds);

                Close();
                return ds;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Obtém um DataSet (com parâmetros)
        /// </summary>
        /// <param name="sql">O comando SQL</param>
        /// <returns>Retorna um DataSet com os dados</returns>
        public DataSet SelectDataSet(string sql, List<object> parameters)
        {
            try
            {
                Open();

                sql = sql.Trim();

                var ds = new DataSet();
                var cmd = new SQLiteCommand(sql, con);
                var i = 1;
                foreach (var parameter in parameters)
                {
                    cmd.Parameters.AddWithValue($"@{i++}", parameter);
                }

                var da = new SQLiteDataAdapter(cmd);
                da.Fill(ds);

                Close();
                return ds;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Obtém um DataSet (SQL dividido em argumentos)
        /// </summary>
        /// <param name="table">Nome da tabela</param>
        /// <param name="fields">Todos os campos (separados por vírgula)</param>
        /// <param name="where">Condição (opcional)</param>
        /// <param name="orderBy">Ordenação (opcional)</param>
        /// <returns>Retorna um DataSet com os dados</returns>
        public DataSet SelectDataSet(string table, string fields, string where = "", string orderBy = "")
        {
            try
            {
                Open();

                table = table.Trim();
                fields = fields.Trim();
                where = where.Trim();
                orderBy = orderBy.Trim();

                var ds = new DataSet();
                var sql = $"SELECT {fields} FROM {table} ";
                if (where != "")
                {
                    sql = $"{sql} WHERE {where} ";
                }

                if (orderBy != "")
                {
                    sql = $"{sql} ORDER BY {orderBy} ";
                }

                var cmd = new SQLiteCommand(sql, con);
                var da = new SQLiteDataAdapter(cmd);
                da.Fill(ds, table);

                Close();
                return ds;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Obtém os dados da conexão em string
        /// </summary>
        /// <returns>Retorna os dados da conexão</returns>
        override public string ToString()
        {
            return con.ConnectionString;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool __disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!__disposed)
            {
                if (disposing)
                {
                    Close();
                }

                __disposed = true;
            }
        }
    }
}
