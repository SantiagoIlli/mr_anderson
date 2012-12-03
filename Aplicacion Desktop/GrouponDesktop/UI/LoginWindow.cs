﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GrouponDesktop.User;
using GrouponDesktop.DataAdapter;

namespace GrouponDesktop
{
    public partial class LoginWindow : Form
    {
        Menu menu;
        User.User usuarioActivo;
        HomeUsuarios homeUsr;
        DataAccess.SPManager spManager = new GrouponDesktop.DataAccess.SPManager();
        Login login;

        
        public LoginWindow()
        {
            InitializeComponent();
            this.homeUsr = new HomeUsuarios();
            this.usuarioActivo = new User.User();
           
        }

       
        

        private void button1_Click(object sender, EventArgs e)
        {

            login = new Login();
            login.UserName = TxtBox_userName.Text;
            login.Password = TxtBox_password.Text;
            try
            {
                login.validateUser();
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.ToString());
                return;
            }

            this.usuarioActivo = homeUsr.getUsuario(login);

            MessageBox.Show("Bienvenid@ "+usuarioActivo.DatosLogin.UserName+"!");

            Menu menu = this.crearMenuWindow(usuarioActivo.Rol);
            menu.ShowDialog(this);
            
        }

        private Menu crearMenuWindow(Rol.Rol unRol)
        {
            Menu menu = new Menu(this);
            int i = 0;
            int j = 0;
            foreach(String func in unRol.getFuncionalidades()){
                Button boton = new Button();
                boton.Text = func;
                boton.AutoSize = true;
                boton.Location = new System.Drawing.Point(20+j,20+i);
                boton.Click += new EventHandler(this.buttonClicked);
                menu.Controls.Add(boton);
                i = i + 35;
                if (i > 35 * 5)
                {
                    i = 0;
                    j = j + 110;
                }
                
            }
            this.menu = menu;
            return menu;
        }

        private void buttonClicked(Object sender, EventArgs e)
        {
            Button boton = (Button)sender;
            this.menu.do_f(boton.Text);
        }

        private void lnkRegister_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegistroWindow w = new RegistroWindow();
            w.Show();
        }

        #region Properties

        public Login _Login
        {
            get { return login; }
            set { login = value; }
        }

        public User.User UsuarioActivo
        {
            get { return usuarioActivo; }
            set { usuarioActivo = value; }
        }

        #endregion
    }
}