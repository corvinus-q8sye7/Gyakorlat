﻿
using SantaFactory.Abstractions2;
using SantaFactory.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SantaFactory
{
    
    public partial class Form1 : Form
    {
        List<Toy> _toys = new List<Toy>();
        private IToyFactory _toyFactory;

        public IToyFactory ToyFactory
        {
            get { return _toyFactory; }
            set { _toyFactory = value; }
        }

        public Form1()
        {
            InitializeComponent();
            ToyFactory = new CarFactory();

            
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var toy = ToyFactory.CreateNew();
            _toys.Add(toy);
            mainPanel.Controls.Add(toy);
            toy.Left = -toy.Width;
            
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var lastPosition = 0;
            foreach (var item in _toys)
            {
                item.MoveToy();
                if (item.Left > lastPosition)
                    lastPosition = item.Left;
            }

            if (lastPosition >= 1000)
            {
               var oldestToy = _toys[0];
               mainPanel.Controls.Remove(oldestToy);
               _toys.Remove(oldestToy);
            }
        }
    }
}
