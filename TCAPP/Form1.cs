﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TCAPP
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            txtTimeFile.Text = openFileDialog1.FileName;
            processTimeFile();
        }

        private void processTimeFile()
        {
            Business.Employees = new List<Person>();

            string line;
            TimeEntry entry=null;

            try
            {
                using (StreamReader file = new StreamReader(txtTimeFile.Text))
                {
                    file.ReadLine();
                    while ((line = file.ReadLine()) != null)
                    {
                        entry = new TimeEntry(line);
                        Person employee = Business.Employees.Find(person => person.Id == entry.EmployeeNumber);
                        if (employee == null)
                        {
                            employee = new Person();
                            employee.Id = entry.EmployeeNumber;
                            employee.Name = entry.Name;
                            employee.AddTimeEntry(entry);
                            Business.Employees.Add(employee);
                        }
                        else
                        {
                            employee.AddTimeEntry(entry);
                        }
                    }

                    file.Close();
                }
            }
            catch (Exception ex)
            {
                txtOutput.Text = ex.Message + Environment.NewLine + entry==null?String.Empty:entry.RawEntryString;
            }

            int selectedInx = cbxIds.SelectedIndex;
            cbxIds.Items.Clear();
            foreach( Person person in Business.Employees)
            {
                cbxIds.Items.Add(person.Id);
            }
            cbxIds.SelectedIndex = selectedInx;

        }

        private void btnProcessFile_Click(object sender, EventArgs e)
        {
            processTimeFile();
        }

        private void btnPrintWk_Click(object sender, EventArgs e)
        {
            Person employee = Business.Employees.Find(person => person.Id == (int)cbxIds.SelectedItem);
            if (employee == null)
            {
                MessageBox.Show("Id: " + cbxIds.SelectedItem + " does not exist");
            }
            PayStub payStub = employee.GetWork((new DateTime(2012, 6, 10)), DateTime.Now);
            
            txtOutput.Text = payStub.Summary + Environment.NewLine;
            txtOutput.Text += "Total Hours: " + Math.Round(payStub.TotalHours, 2) + "\t$" + 
                payStub.GetPay( Business.PayRate[employee.Id - 1] );
        }
    }

}
