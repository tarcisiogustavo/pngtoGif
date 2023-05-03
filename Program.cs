using System;
using System.IO;
using ImageMagick;
using System.Windows.Forms;

namespace PNGtoGIF
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string inputFolder = "";
            string outputFolder = "";

            // Selecionar pasta de entrada
            var inputFolderDialog = new FolderBrowserDialog();
            inputFolderDialog.Description = "Selecione a pasta que contém as imagens PNG a serem convertidas.";
            if (inputFolderDialog.ShowDialog() == DialogResult.OK)
            {
                inputFolder = inputFolderDialog.SelectedPath;
            }
            else
            {
                Console.WriteLine("Nenhuma pasta de entrada selecionada. O programa será encerrado.");
                return;
            }

            if (!Directory.Exists(inputFolder))
            {
                Console.WriteLine($"A pasta de entrada {inputFolder} não existe.");
                return;
            }

            // Selecionar pasta de saída
            var outputFolderDialog = new FolderBrowserDialog();
            outputFolderDialog.Description = "Selecione a pasta de saída para salvar as imagens GIF.";
            if (outputFolderDialog.ShowDialog() == DialogResult.OK)
            {
                outputFolder = outputFolderDialog.SelectedPath;
            }
            else
            {
                Console.WriteLine("Nenhuma pasta de saída selecionada. O programa será encerrado.");
                return;
            }

            Directory.CreateDirectory(outputFolder);

            var files = Directory.GetFiles(inputFolder, "*.png");

            foreach (var file in files)
            {
                Console.WriteLine($"Convertendo {file}...");

                using (var image = new MagickImage(file))
                {
                    var outputFile = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(file) + ".gif");
                    image.Write(outputFile);
                }
            }

            Console.WriteLine("Conversão concluída!");
        }
    }
}
