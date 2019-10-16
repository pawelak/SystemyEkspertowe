using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ExpertSystems
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string _filePath = "./../../tree.json";
        private Node _current;
        private IEnumerable<Node> _treeList;

        public MainWindow()
        {
            InitializeComponent();
            using (StreamReader sr = new StreamReader(_filePath, Encoding.GetEncoding("Windows-1250")))
            {
                var json = sr.ReadToEnd();
                _treeList = JsonConvert.DeserializeObject<List<Node>>(json);
                Console.WriteLine(_treeList.Count());
            }
            _current = _treeList.Where(x => x.Id.Equals("q1")).FirstOrDefault();
            CreatePage();
        }

        private void ResetBnt_Click(object sender, RoutedEventArgs e)
        {
            _current = _treeList.Where(x => x.Id.Equals("q1")).FirstOrDefault();
            CreatePage();
        }

        private void NextBnt_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == null) return;

            if (_current.Id.Equals("0"))
            {
                SetupQuestions();
            }
            else
            {
                switch (button.Content.ToString())
                {
                    case "Dalej":
                        SetupQuestions();
                        break;
                    case "Reset":

                        break;
                }
            }
        }

        private void SetupQuestions()
        {
            RadioButton selectedRB = null;
            foreach (var rbutton in this.AnswersPanel.Children)
            {
                if ((rbutton as RadioButton).IsChecked ?? false)
                {
                    selectedRB = rbutton as RadioButton;
                }
            }
            CreatePage(selectedRB?.Name);
        }

        private void CreatePage(string id = null)
        {
            _current = id == null ? _current : _treeList.FirstOrDefault(x => x.Id.Equals(id));
            this.AnswersPanel.Children.Clear();
            this.QuestionText.Text = _current.Question;

            if (_current.Reference?.Length > 0)
            {
                foreach (var n in _current.Reference)
                {
                    var tmp = _treeList.FirstOrDefault(x => x.Id.Equals(n));
                    this.AnswersPanel.Children.Add(new RadioButton
                    {
                        Margin = new Thickness(3),
                        Content = tmp?.Answer,
                        GroupName = "answers",
                        Name = tmp?.Id
                    });
                }
            }
        }
    }
}
