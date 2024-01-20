using System;
using System.Windows.Forms;

namespace AnimalsManageApp
{
    /// <summary>
    /// AnimalRecord class.
    /// </summary>
    internal sealed class AnimalRecord
    {
        /// <summary>
        /// Gets the tag.
        /// </summary>
        /// <value>
        /// The tag.
        /// </value>
        public string Tag { get; }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimalRecord"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="url">The URL.</param>
        public AnimalRecord(string tag, string url)
        {
            Tag = tag;
            Url = url;
        }


        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{Tag},{Url}";
        }

        /// <summary>
        /// Froms the text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static AnimalRecord FromText(string text)
        {
            string[] s = text.Split(',');
            return new AnimalRecord(s[0], s[1]);
        }
    }

    /// <summary>
    /// Program class.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
