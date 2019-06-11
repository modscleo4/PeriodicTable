using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
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
        private SqliteConnection con;

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
                con = new SqliteConnection(connectionString);
                con.Open();
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

                var cmd = new SqliteCommand(sql, con);
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

                var cmd = new SqliteCommand(sql, con);
                var i = 1;
                foreach (var parameter in parameters)
                {
                    cmd.Parameters.AddWithValue($"@{i++}", parameter ?? DBNull.Value);
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
        public Task<int> RunAsync(string sql)
        {
            var tcs = new TaskCompletionSource<int>();
            Task.Run(() =>
            {
                var r = Run(sql);
                tcs.SetResult(r);
            });

            return tcs.Task;
        }

        /// <summary>
        /// Executa um comando SQL sem retorno de forma assíncrona (com parâmetros)
        /// </summary>
        /// <param name="sql">O comando SQL</param>
        /// <returns>Retorna o número de linhas afetadas pela query</returns>
        public Task<int> RunAsync(string sql, List<object> parameters)
        {
            var tcs = new TaskCompletionSource<int>();
            Task.Run(() =>
            {
                var r = Run(sql, parameters);
                tcs.SetResult(r);
            });

            return tcs.Task;
        }

        /// <summary>
        /// Executa um comando SQL com retorno de dados
        /// </summary>
        /// <param name="sql">O comando SQL</param>
        /// <returns>Retorna os dados selecionados</returns>
        public SqliteDataReader Select(string sql)
        {
            try
            {
                Open();

                sql = sql.Trim();

                var cmd = new SqliteCommand(sql, con);
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
        public SqliteDataReader Select(string sql, List<object> parameters)
        {
            try
            {
                Open();

                sql = sql.Trim();

                var cmd = new SqliteCommand(sql, con);
                var i = 1;
                foreach (var parameter in parameters)
                {
                    cmd.Parameters.AddWithValue($"@{i++}", parameter ?? DBNull.Value);
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
        public Task<SqliteDataReader> SelectAsync(string sql)
        {
            var tcs = new TaskCompletionSource<SqliteDataReader>();
            Task.Run(() =>
            {
                var r = Select(sql);
                tcs.SetResult(r);
            });

            return tcs.Task;
        }

        /// <summary>
        /// Executa um comando SQL com retorno de dados de forma assíncrona (com parâmetros)
        /// </summary>
        /// <param name="sql">O comando SQL</param>
        /// <param name="parameters">Os parâmetros do comando</param>
        /// <returns>Retorna os dados selecionados</returns>
        public Task<SqliteDataReader> SelectAsync(string sql, List<object> parameters)
        {
            var tcs = new TaskCompletionSource<SqliteDataReader>();
            Task.Run(() =>
            {
                var r = Select(sql, parameters);
                tcs.SetResult(r);
            });

            return tcs.Task;
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
