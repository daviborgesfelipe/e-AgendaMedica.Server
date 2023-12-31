﻿using e_AgendaMedica.Dominio.ModuloAtividade;
using e_AgendaMedica.WebApi.ViewModels.ModuloMedico;

namespace e_AgendaMedica.WebApi.ViewModels.ModuloAtividade
{
    public class VisualizarAtividadeViewModel
    {
        public string Paciente { get; set; }
        public DateTime Data { get; set; }
        public string HorarioTermino { get; set; }
        public string HorarioInicio { get; set; }
        public TipoAtividadeEnum TipoAtividade { get; set; }

        public List<ListarMedicoViewModel> ListaMedicos { get; set; }
    }
}
