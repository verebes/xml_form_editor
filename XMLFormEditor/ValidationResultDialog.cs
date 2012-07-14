using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XMLFormEditor
{
    public partial class ValidationResultDialog : Form
    {
        public ValidationResultDialog()
        {
            InitializeComponent();
        }

        List<ValidationResult> _validationResults;
        public List<ValidationResult> ValidationResults
        {
            get { return _validationResults; }
            set {
                _validationResults = value;
                ProcessValidationResultList();
            }
        }

        public bool ShowWarnings
        {
            get {  return checkBox1.Checked; }
            set {  
                checkBox1.Checked = value;
                ProcessValidationResultList();
            }
        }

        private void ProcessValidationResultList()
        {
            listBox1.Items.Clear();
            foreach (ValidationResult valRes in _validationResults)
            {
                listBox1.Items.Add(valRes.FileName);
                foreach (string s in valRes.Errors)
                    listBox1.Items.Add(s);

                if (checkBox1.Checked)
                {
                    foreach (string s in valRes.Warnings)
                        listBox1.Items.Add(s);
                }

                labelErrorCount.Text = valRes.Errors.Count.ToString() + " errors were found";
                labelWarningCount.Text = valRes.Warnings.Count.ToString() + " warnings were found";

                listBox1.Items.Add("");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ProcessValidationResultList();
        }

        private void ValidationResultDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                checkBox1.Checked = false;
                ProcessValidationResultList();
            }


            if (e.KeyCode == Keys.F3)
            {
                checkBox1.Checked = true;
                ProcessValidationResultList();
            }

        }

    }
}