﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GrouponDesktop.Rol;

namespace GrouponDesktop
{
    public partial class ModificacionRol : Form
    {
        string selectedRolName;
        List<String> funcionalidadesRol;
        List<String> funcionalidadesDisponibles;
        HomeRoles home;

        public ModificacionRol(String nombre)
        {
            InitializeComponent();
            this.selectedRolName = nombre;
            this.home = new HomeRoles();
            
            Rol.Rol adminRol = home.getRol("Administrador General");
            this.funcionalidadesDisponibles = adminRol.getFuncionalidades();
            this.fill();
            
        }

        public void fill()
        {
            Rol.Rol rol = home.getRol(this.selectedRolName);
            this.funcionalidadesRol = rol.getFuncionalidades();

            this.textBox1.Text = this.selectedRolName;
            this.listBox1.DataSource = funcionalidadesRol;
            this.listBox2.DataSource = funcionalidadesDisponibles;

            this.checkBox1.Checked = rol.estaHabilitado();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (String f in funcionalidadesRol)
            {
                if (f.Equals(this.listBox2.SelectedValue.ToString())) return;
            }
            this.home.addFuncionalidad(this.selectedRolName,this.listBox2.SelectedValue.ToString());
            
            this.fill();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (funcionalidadesRol.Count() == 1) return;
            
            this.home.deleteFuncionalidad(this.selectedRolName, this.listBox1.SelectedValue.ToString());
            this.fill();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.home.modificarNombre(selectedRolName, this.textBox1.Text);
            selectedRolName = this.textBox1.Text;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.home.modificarStatus(this.selectedRolName,this.checkBox1.Checked);
        }
    }
}
