﻿using System;
using System.IO;
using System.Windows.Forms;

namespace Tutorial5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            LoadContents(textBox1.Text);
        }

        /// <summary>
        /// Loads all directories via path in a Treeview
        /// </summary>
        /// <param name="path">path of directory to load</param>
        /// <param name="isNodeClick">Checks to see if this is a Treeview Node double click</param>
        private void LoadContents(string path, bool isNodeClick = false)
        {
            // Check if the path is null or empty OR if it is an actual file. If so, return
            if (string.IsNullOrEmpty(path) || File.Exists(path))
                return;

            treeView1.Nodes.Clear();

            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            try
            {
                foreach (DirectoryInfo dirInfo in directoryInfo?.GetDirectories())
                {
                    TreeNode node = new TreeNode();
                    node.Text = dirInfo.FullName;
                    node.ImageIndex = 1;
                    node.SelectedImageIndex = 1;

                    if (isNodeClick)
                    {
                        foreach (FileInfo file in dirInfo?.GetFiles())
                        {
                            TreeNode childNode = new TreeNode();
                            childNode.Text = file.FullName;
                            childNode.ImageIndex = 0;
                            childNode.SelectedImageIndex = 0;
                            node.Nodes.Add(childNode);
                        }
                    }
                    treeView1.Nodes.Add(node);
                }

                foreach (FileInfo fileInfo in directoryInfo?.GetFiles())
                {
                    TreeNode fileNode = new TreeNode();
                    fileNode.Text = fileInfo.FullName;
                    fileNode.ImageIndex = 0;
                    fileNode.SelectedImageIndex = 0;
                    treeView1.Nodes.Add(fileNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            textBox1.Text = path;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadContents(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(textBox1.Text);
            LoadContents(directoryInfo?.Parent?.FullName);
        }

        private void treeview_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            LoadContents(e.Node.Text, true);
        }
    }
}
