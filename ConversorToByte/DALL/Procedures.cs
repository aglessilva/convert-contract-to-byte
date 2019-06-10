using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversorToByte.DALL
{
    public enum Procedures
    {
        /// <summary>
        /// Procedure que grava os arquivos PDF. ZIP Binalizados no banco de dados
        /// </summary>
        SP_POST_FILE_SAFE = 0,
        
        /// <summary>
        /// Atualiza Permissão de acesso de usuarios ao sistema
        /// </summary>
        SP_UDT_USERS = 1,

        /// <summary>
        /// Adiciona um novo usuario ao sistema
        /// </summary>
        SP_POST_USERS = 2,

        /// <summary>
        /// Retorna uma lista de usuários do sistema
        /// </summary>
        SP_GET_USERS = 3,

        /// <summary>
        /// Retorna o caminho fisico dos arquivos
        /// </summary>
        SP_GET_PATH_COMPANY = 4,


        /// <summary>
        /// Retorna uma lista de registro
        /// </summary>
        SP_GET_FILES = 5,

        /// <summary>
        /// retorna um allista de empresas fornecedoreas de contratos
        /// </summary>
        SP_GET_COMPANY = 6,

        /// <summary>
        /// verifica se o usuario logado pode acessar o sistema
        /// </summary>
        SP_CHK_USER = 7,

        /// <summary>
        /// checa e o usuario atual tem permissão de acesso ao sistema
        /// </summary>
        SP_CHK_PERMICAO = 8
    }
}
