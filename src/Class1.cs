﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro_Alunos
{
    internal class Estudante
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public Estudante()
        {
            this.Name = string.Empty;
        }
    }
}
