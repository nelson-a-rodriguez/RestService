﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WEBSUMA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RepositorioSuma rep = new RepositorioSuma();
            //rep.ConsultarInteres();
            //rep.ConsultarDireccion();
            //rep.ConsultarCanal();
            rep.FindAfiliacionSuma("V-14566318");
        }
    }
}
