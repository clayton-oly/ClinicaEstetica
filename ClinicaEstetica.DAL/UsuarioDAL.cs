﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ClinicaEstetica.DTO;

namespace ClinicaEstetica.DAL
{
    public class UsuarioDAL : Conexao
    {
        public UsuarioDTO Autenticar(string email, string senha)
        {
            try
            {
                Conectar();
                command = new SqlCommand("SELECT * FROM Usuario WHERE Email = @Email AND Senha = @Senha;", connection);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Senha", senha);
                dataReader = command.ExecuteReader();

                UsuarioDTO usuario = null;
                if (dataReader.Read())
                {
                    usuario = new UsuarioDTO();
                    usuario.IdTipoUsuario = int.Parse(dataReader["IdTipoUsuario"].ToString());
                    usuario.Nome = dataReader["Nome"].ToString();
                    usuario.Email = dataReader["Email"].ToString();
                    usuario.Senha = dataReader["Senha"].ToString();
                    usuario.Status = bool.Parse(dataReader["Status"].ToString());
                }
                return usuario;

            }
            catch (Exception error)
            {

                throw new Exception($"Erro: {error.Message}");
            }
            finally
            {
                Desconectar();
            }
        }

        public List<UsuarioDTO> ListarTodos()
        {
            List<UsuarioDTO> usuarios = new List<UsuarioDTO>();

            try
            {
                Conectar();
                string sql = "SELECT * FROM Usuario ORDER BY Nome";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usuarios.Add(new UsuarioDTO()
                        {
                            IdUsuario = (int)reader["IdUsuario"],
                            Nome = reader["Nome"].ToString(),
                            Email = reader["Email"].ToString(),
                            Senha = reader["Senha"].ToString(),
                            Status = (bool)reader["Senha"],
                            IdTipoUsuario = (int)reader["IdTipoUsuario"]
                        });
                    }
                }

            }
            catch (Exception error)
            {
                throw new Exception($"Erro: {error.Message}");
            }

            return usuarios;
        }

        public List<TipoUsuarioDTO> GetTipos()
        {
            try
            {
                Conectar();
                string sql = "SELECT * FROM TipoUsuario;";
                command = new SqlCommand(sql, connection);
                dataReader = command.ExecuteReader();
                List<TipoUsuarioDTO> lista = new List<TipoUsuarioDTO>();
                while (dataReader.Read())
                {
                    TipoUsuarioDTO tipoUsuario = new TipoUsuarioDTO();
                    tipoUsuario.IdTipoUsuario = Convert.ToInt32(dataReader["IdTipoUsuario"]);
                    tipoUsuario.Nome = dataReader["Nome"].ToString();
                    lista.Add(tipoUsuario);
                }

                return lista;
            }
            catch (Exception error)
            {

                throw new Exception($"Erro: {error.Message}");
            }
            finally
            {
                Desconectar();
            }
        }
    }
}
